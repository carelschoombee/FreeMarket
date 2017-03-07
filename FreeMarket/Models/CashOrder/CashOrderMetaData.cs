using FreeMarket.FreeMarketDataSetTableAdapters;
using Microsoft.AspNet.Identity;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace FreeMarket.Models
{
    [MetadataType(typeof(CashOrderMetaData))]
    public partial class CashOrder
    {
        [DisplayName("Name")]
        public string CustomerName { get; set; }

        [DisplayName("Email")]
        public string CustomerEmail { get; set; }

        [DisplayName("Phone")]
        public string CustomerPhone { get; set; }

        [DisplayName("Address")]
        public string CustomerDeliveryAddress { get; set; }

        [DisplayName("Client Vat Number")]
        public string ClientVatNumber { get; set; }

        public static Dictionary<Stream, string> GetReport(string reportType, int orderNumber, string bankAccountType)
        {
            Stream stream = new MemoryStream();
            Dictionary<Stream, string> outCollection = new Dictionary<Stream, string>();

            try
            {
                GetCashOrderReportTableAdapter ta = new GetCashOrderReportTableAdapter();
                FreeMarketDataSet ds = new FreeMarketDataSet();

                ds.GetCashOrderReport.Clear();
                ds.EnforceConstraints = false;

                ta.Fill(ds.GetCashOrderReport, orderNumber, bankAccountType);

                ReportDataSource rds = new ReportDataSource();
                rds.Name = "DataSet2";
                rds.Value = ds.GetCashOrderReport;

                ReportViewer rv = new Microsoft.Reporting.WebForms.ReportViewer();
                rv.ProcessingMode = ProcessingMode.Local;

                switch (reportType)
                {
                    case "CashOrderInvoice":
                        rv.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/Reports/Report7.rdlc");
                        break;

                    default:
                        return new Dictionary<Stream, string>();
                }

                rv.LocalReport.DataSources.Add(rds);
                rv.LocalReport.EnableHyperlinks = true;
                rv.LocalReport.Refresh();

                byte[] streamBytes = null;
                string mimeType = "";
                string encoding = "";
                string filenameExtension = "";
                string[] streamids = null;
                Warning[] warnings = null;

                streamBytes = rv.LocalReport.Render("PDF", null, out mimeType, out encoding, out filenameExtension, out streamids, out warnings);

                stream = new MemoryStream(streamBytes);

                outCollection.Add(stream, mimeType);
            }
            catch (Exception e)
            {
                ExceptionLogging.LogException(e);
            }

            return outCollection;
        }

        public static async void SendInvoice(int orderNumber, string bankAccountType)
        {
            using (FreeMarketEntities db = new FreeMarketEntities())
            {
                CashOrder order = db.CashOrders.Find(orderNumber);

                if (order == null)
                    return;

                CashCustomer customer = db.CashCustomers.Find(order.CashCustomerId);

                if (customer == null)
                    return;

                Support supportInfo = db.Supports.FirstOrDefault();

                Dictionary<Stream, string> invoice = CashOrder.GetReport(ReportType.CashOrderInvoice.ToString(), orderNumber, bankAccountType);

                IdentityMessage iMessage = new IdentityMessage();
                iMessage.Destination = customer.Email;

                string message1 = CreateConfirmationMessageCustomer();

                iMessage.Body = string.Format((message1), customer.Name, supportInfo.MainContactName, supportInfo.Landline, supportInfo.Cellphone, supportInfo.Email);
                iMessage.Subject = string.Format("Schoombee and Son Order");

                EmailService email = new EmailService();

                await email.SendAsync(iMessage, invoice.FirstOrDefault().Key);
            }
        }

        private static string CreateConfirmationMessageCustomer()
        {
            using (FreeMarketEntities db = new FreeMarketEntities())
            {
                string line1 = db.SiteConfigurations
                    .Where(c => c.Key == "OrderConfirmationEmailLine1")
                    .Select(c => c.Value)
                    .FirstOrDefault();

                string line2 = db.SiteConfigurations
                    .Where(c => c.Key == "OrderConfirmationEmailLine2")
                    .Select(c => c.Value)
                    .FirstOrDefault();

                string line3 = db.SiteConfigurations
                    .Where(c => c.Key == "OrderConfirmationEmailLine3")
                    .Select(c => c.Value)
                    .FirstOrDefault();

                string line4 = db.SiteConfigurations
                    .Where(c => c.Key == "OrderConfirmationEmailLine4")
                    .Select(c => c.Value)
                    .FirstOrDefault();

                string line5 = db.SiteConfigurations
                    .Where(c => c.Key == "OrderConfirmationEmailLine5")
                    .Select(c => c.Value)
                    .FirstOrDefault();

                return line1 + line2 + line3 + line4 + line5;
            }
        }

        public async static Task<FreeMarketObject> CreateNewCashOrder(CashOrderViewModel model)
        {
            FreeMarketObject result = new FreeMarketObject { Result = FreeMarketResult.NoResult, Argument = null, Message = null };

            using (FreeMarketEntities db = new FreeMarketEntities())
            {
                CashCustomer customer = db.CashCustomers.Find(model.Order.CashCustomerId);

                if (customer == null)
                {
                    customer = new CashCustomer
                    {
                        DeliveryAddress = model.Order.CustomerDeliveryAddress,
                        Email = model.Order.CustomerEmail,
                        Name = model.Order.CustomerName,
                        PhoneNumber = model.Order.CustomerPhone,
                        ClientVatNumber = model.Order.ClientVatNumber
                    };

                    db.CashCustomers.Add(customer);
                    db.SaveChanges();
                }
                else
                {
                    customer.DeliveryAddress = model.Order.CustomerDeliveryAddress;
                    customer.Email = model.Order.CustomerEmail;
                    customer.Name = model.Order.CustomerName;
                    customer.PhoneNumber = model.Order.CustomerPhone;
                    customer.ClientVatNumber = model.Order.ClientVatNumber;

                    db.Entry(customer).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }

                string status;
                if (model.Order.Delivered == true && model.Order.PaymentReceived == true)
                    status = "Completed";
                else
                    status = "In Progress";

                CashOrder order = new CashOrder
                {
                    CashCustomerId = customer.Id,
                    DatePlaced = DateTime.Now,
                    Status = status,
                    Total = model.Order.ShippingTotal,
                    Delivered = model.Order.Delivered,
                    PaymentReceived = model.Order.PaymentReceived,
                    BankTransfer = model.Order.BankTransfer,
                    CashTransaction = model.Order.CashTransaction,
                    InvoiceSent = model.Order.InvoiceSent,
                    ShippingTotal = model.Order.ShippingTotal,
                    ClientOrderNumber = model.Order.ClientOrderNumber
                };

                db.CashOrders.Add(order);
                db.SaveChanges();

                foreach (Product p in model.Products.Products)
                {
                    if (p.CashQuantity > 0)
                    {
                        decimal price = decimal.Parse(p.SelectedPrice);
                        CashOrderDetail detail = new CashOrderDetail
                        {
                            CashOrderId = order.OrderId,
                            ProductNumber = p.ProductNumber,
                            SupplierNumber = p.SupplierNumber,
                            Quantity = p.CashQuantity,
                            Price = price,
                            OrderItemTotal = price * p.CashQuantity,
                            CustodianNumber = model.SelectedCustodian
                        };

                        db.CashOrderDetails.Add(detail);
                        db.SaveChanges();

                        order.Total += detail.OrderItemTotal;

                        db.Entry(order).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();

                        ProductCustodian custodian = db.ProductCustodians
                            .Where(c => c.CustodianNumber == model.SelectedCustodian &&
                                                c.ProductNumber == p.ProductNumber &&
                                                c.SupplierNumber == p.SupplierNumber)
                                                .FirstOrDefault();

                        custodian.QuantityOnHand -= p.CashQuantity;

                        db.Entry(custodian).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                    }
                }

                db.SaveChanges();

                if (order.InvoiceSent == true && !string.IsNullOrEmpty(customer.Email))
                    SendInvoice(order.OrderId, model.SelectedBankAcountOption);

                if (customer != null && order != null && db.CashOrderDetails.Any(c => c.CashOrderId == order.OrderId))
                    result.Result = FreeMarketResult.Success;
                else
                    result.Result = FreeMarketResult.Failure;
            }

            return result;
        }

        public async static Task<FreeMarketObject> ModifyOrder(CashOrderViewModel model)
        {
            FreeMarketObject result = new FreeMarketObject { Result = FreeMarketResult.NoResult, Argument = null, Message = null };

            using (FreeMarketEntities db = new FreeMarketEntities())
            {
                CashCustomer customer = db.CashCustomers.Find(model.Order.CashCustomerId);

                customer.DeliveryAddress = model.Order.CustomerDeliveryAddress;
                customer.Email = model.Order.CustomerEmail;
                customer.Name = model.Order.CustomerName;
                customer.PhoneNumber = model.Order.CustomerPhone;
                customer.ClientVatNumber = model.Order.ClientVatNumber;

                db.Entry(customer).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

                CashOrder order = db.CashOrders.Find(model.Order.OrderId);

                List<GetCashOrderDetails_Result> orderDetails = db.GetCashOrderDetails(order.OrderId).ToList();

                foreach (Product p in model.Products.Products)
                {
                    if (p.CashQuantity > 0)
                    {
                        decimal price = decimal.Parse(p.SelectedPrice);

                        if (orderDetails.Any(c => c.ProductNumber == p.ProductNumber && c.SupplierNumber == p.SupplierNumber))
                        {
                            CashOrderDetail existingDetail = db.CashOrderDetails
                                .Where(c => c.CashOrderId == order.OrderId && c.ProductNumber == p.ProductNumber && c.SupplierNumber == p.SupplierNumber)
                                .FirstOrDefault();
                            existingDetail.Price = price;
                            existingDetail.OrderItemTotal = price * p.CashQuantity;

                            if (existingDetail.Quantity > p.CashQuantity)
                            {
                                int stock = existingDetail.Quantity - p.CashQuantity;
                                AddStockToCustodian(order.OrderId, p.ProductNumber, p.SupplierNumber, model.SelectedCustodian, stock);
                            }
                            else
                            {
                                int stock = p.CashQuantity - existingDetail.Quantity;
                                RemoveStockFromCustodian(order.OrderId, p.ProductNumber, p.SupplierNumber, model.SelectedCustodian, stock);
                            }

                            existingDetail.Quantity = p.CashQuantity;

                            db.Entry(existingDetail).State = System.Data.Entity.EntityState.Modified;
                            db.SaveChanges();
                        }
                        else
                        {
                            CashOrderDetail detail = new CashOrderDetail
                            {
                                CashOrderId = order.OrderId,
                                ProductNumber = p.ProductNumber,
                                SupplierNumber = p.SupplierNumber,
                                Quantity = p.CashQuantity,
                                Price = price,
                                OrderItemTotal = price * p.CashQuantity,
                                CustodianNumber = model.SelectedCustodian,
                            };

                            db.CashOrderDetails.Add(detail);
                            db.SaveChanges();

                            RemoveStockFromCustodian(order.OrderId, p.ProductNumber, p.SupplierNumber, model.SelectedCustodian, p.CashQuantity);
                        }
                    }
                    else
                    {
                        CashOrderDetail toRemove = db.CashOrderDetails
                            .Where(c => c.CashOrderId == order.OrderId && c.ProductNumber == p.ProductNumber && c.SupplierNumber == p.SupplierNumber)
                            .FirstOrDefault();

                        if (toRemove != null)
                        {
                            db.CashOrderDetails.Remove(toRemove);

                            AddStockToCustodian(order.OrderId, p.ProductNumber, p.SupplierNumber, model.SelectedCustodian, toRemove.Quantity);
                        }
                    }
                }

                db.SaveChanges();

                List<GetCashOrderDetails_Result> details = db.GetCashOrderDetails(order.OrderId).ToList();
                order.Total = details.Sum(c => c.OrderItemTotal) + model.Order.ShippingTotal;
                order.DatePlaced = DateTime.Now;
                order.Delivered = model.Order.Delivered;
                order.BankTransfer = model.Order.BankTransfer;
                order.CashTransaction = model.Order.CashTransaction;
                order.PaymentReceived = model.Order.PaymentReceived;
                order.ShippingTotal = model.Order.ShippingTotal;
                order.ClientOrderNumber = model.Order.ClientOrderNumber;
                order.InvoiceSent = model.Order.InvoiceSent;

                string status;
                if (model.Order.Delivered == true && model.Order.PaymentReceived == true)
                    status = "Completed";
                else
                    status = "In Progress";

                order.Status = status;

                db.Entry(order).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

                if (model.Order.InvoiceSent != null)
                {
                    if (order.InvoiceSent == true && !string.IsNullOrEmpty(customer.Email))
                        SendInvoice(order.OrderId, model.SelectedBankAcountOption);
                }

                if (customer != null && order != null && db.CashOrderDetails.Any(c => c.CashOrderId == order.OrderId))
                    result.Result = FreeMarketResult.Success;
                else
                    result.Result = FreeMarketResult.Failure;
            }

            return result;
        }

        public static void RefundOrder(int id)
        {
            using (FreeMarketEntities db = new FreeMarketEntities())
            {
                CashOrder order = db.CashOrders.Find(id);

                if (order != null)
                {
                    order.Status = "Refunded";
                    db.Entry(order).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
            }
        }

        public static List<GetDeliveryLabelsCashOrder_Result> GetDeliveryLabels()
        {
            using (FreeMarketEntities db = new FreeMarketEntities())
            {
                return db.GetDeliveryLabelsCashOrder().ToList();
            }
        }

        public static void AddStockToCustodian(int orderId, int productNumber, int supplierNumber, int custodianNumber, int quantity)
        {
            using (FreeMarketEntities db = new FreeMarketEntities())
            {

                ProductCustodian custodian = db.ProductCustodians
                                .Where(c => c.CustodianNumber == custodianNumber &&
                                                    c.ProductNumber == productNumber &&
                                                    c.SupplierNumber == supplierNumber)
                                                    .FirstOrDefault();

                custodian.QuantityOnHand += quantity;

                db.Entry(custodian).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
        }

        public static void RemoveStockFromCustodian(int orderId, int productNumber, int supplierNumber, int custodianNumber, int quantity)
        {
            using (FreeMarketEntities db = new FreeMarketEntities())
            {

                ProductCustodian custodian = db.ProductCustodians
                                .Where(c => c.CustodianNumber == custodianNumber &&
                                                    c.ProductNumber == productNumber &&
                                                    c.SupplierNumber == supplierNumber)
                                                    .FirstOrDefault();

                custodian.QuantityOnHand -= quantity;

                db.Entry(custodian).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
        }
    }

    public class CashOrderMetaData
    {
        [DisplayName("Delivered")]
        public bool Delivered { get; set; }

        [DisplayName("Payment Received")]
        public bool PaymentReceived { get; set; }

        [DisplayName("Cash Transaction")]
        public bool CashTransaction { get; set; }

        [DisplayName("Bank Transfer")]
        public bool BankTransfer { get; set; }

        [DisplayName("Send Invoice By Email")]
        public bool InvoiceSent { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        [DisplayName("Shipping Total")]
        public decimal ShippingTotal { get; set; }

        [DisplayName("Client Order Number")]
        public string ClientOrderNumber { get; set; }
    }
}