using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;

namespace FreeMarket.Models
{
    public class CashOrderViewModel
    {
        public CashOrder Order { get; set; }
        public List<CashOrderDetail> OrderDetails { get; set; }

        [DisplayName("Custodian")]
        public int SelectedCustodian { get; set; }

        [DisplayName("Custodian")]
        public string CustodianName { get; set; }
        public List<SelectListItem> Custodians { get; set; }
        public ProductCollection Products { get; set; }

        public string SelectedBankAcountOption { get; set; }

        public List<string> BankAcountOptions { get; set; }

        public List<SelectListItem> CustomerType { get; set; }

        [DisplayName("Select Type of Customer")]
        public string SelectedCustomerType { get; set; }

        public List<SelectListItem> ShippingType { get; set; }

        [DisplayName("Shipping")]
        public string SelectedShippingType { get; set; }

        public static List<CashOrderViewModel> GetOrders(string searchCriteria)
        {
            List<CashOrderViewModel> model = new List<CashOrderViewModel>();

            if (string.IsNullOrEmpty(searchCriteria))
                return model;

            using (FreeMarketEntities db = new FreeMarketEntities())
            {
                List<FilterCashOrder_Result> orders = db.FilterCashOrder(searchCriteria).ToList();

                if (orders == null)
                    return model;

                foreach (FilterCashOrder_Result result in orders)
                {
                    CashOrderViewModel viewModel = new CashOrderViewModel();
                    viewModel.Order = new CashOrder
                    {
                        CashCustomerId = (int)result.CashCustomerId,
                        OrderId = (int)result.OrderId,
                        Total = (int)result.Total,
                        CustomerDeliveryAddress = result.DeliveryAddress,
                        CustomerEmail = result.Email,
                        CustomerName = result.Name,
                        CustomerPhone = result.PhoneNumber,
                        DatePlaced = result.DatePlaced,
                        Delivered = result.Delivered,
                        BankTransfer = result.BankTransfer,
                        CashTransaction = result.CashTransaction,
                        PaymentReceived = result.PaymentReceived,
                        InvoiceSent = result.InvoiceSent,
                        ShippingTotal = result.ShippingTotal,
                        ContactName = result.ContactName
                    };

                    viewModel.OrderDetails = db.GetCashOrderDetails(viewModel.Order.OrderId)
                        .Select(c => new CashOrderDetail
                        {
                            CashOrderId = c.CashOrderId,
                            CashOrderItemId = c.CashOrderItemId,
                            CustodianNumber = c.CustodianNumber,
                            Price = c.Price,
                            ProductNumber = c.ProductNumber,
                            Quantity = c.Quantity,
                            SupplierNumber = c.SupplierNumber,
                            Description = c.Description,
                            SupplierName = c.SupplierName,
                            Weight = c.Weight,
                            OrderItemTotal = c.OrderItemTotal
                        })
                        .ToList();

                    model.Add(viewModel);
                }
            }

            return model;
        }

        public static List<CashOrderViewModel> GetOutstandingPaymentOrders()
        {
            List<CashOrderViewModel> model = new List<CashOrderViewModel>();

            using (FreeMarketEntities db = new FreeMarketEntities())
            {
                List<FilterCashOrder_Result> orders = db.FilterCashOrder("all")
                    .Where(c => c.PaymentReceived == false).ToList();

                if (orders == null)
                    return model;

                foreach (FilterCashOrder_Result result in orders)
                {
                    CashOrderViewModel viewModel = new CashOrderViewModel();
                    viewModel.Order = new CashOrder
                    {
                        CashCustomerId = (int)result.CashCustomerId,
                        OrderId = (int)result.OrderId,
                        Total = (int)result.Total,
                        CustomerDeliveryAddress = result.DeliveryAddress,
                        CustomerEmail = result.Email,
                        CustomerName = result.Name,
                        CustomerPhone = result.PhoneNumber,
                        DatePlaced = result.DatePlaced,
                        Delivered = result.Delivered,
                        BankTransfer = result.BankTransfer,
                        CashTransaction = result.CashTransaction,
                        PaymentReceived = result.PaymentReceived,
                        InvoiceSent = result.InvoiceSent,
                        ShippingTotal = result.ShippingTotal,
                        ContactName = result.ContactName
                    };

                    viewModel.OrderDetails = db.GetCashOrderDetails(viewModel.Order.OrderId)
                        .Select(c => new CashOrderDetail
                        {
                            CashOrderId = c.CashOrderId,
                            CashOrderItemId = c.CashOrderItemId,
                            CustodianNumber = c.CustodianNumber,
                            Price = c.Price,
                            ProductNumber = c.ProductNumber,
                            Quantity = c.Quantity,
                            SupplierNumber = c.SupplierNumber,
                            Description = c.Description,
                            SupplierName = c.SupplierName,
                            Weight = c.Weight,
                            OrderItemTotal = c.OrderItemTotal
                        })
                        .ToList();

                    model.Add(viewModel);
                }
            }

            return model;
        }

        public static List<CashOrderViewModel> GetUndeliveredOrders()
        {
            List<CashOrderViewModel> model = new List<CashOrderViewModel>();

            using (FreeMarketEntities db = new FreeMarketEntities())
            {
                List<FilterCashOrder_Result> orders = db.FilterCashOrder("all")
                    .Where(c => c.Delivered == false).ToList();

                if (orders == null)
                    return model;

                foreach (FilterCashOrder_Result result in orders)
                {
                    CashOrderViewModel viewModel = new CashOrderViewModel();
                    viewModel.Order = new CashOrder
                    {
                        CashCustomerId = (int)result.CashCustomerId,
                        OrderId = (int)result.OrderId,
                        Total = (int)result.Total,
                        CustomerDeliveryAddress = result.DeliveryAddress,
                        CustomerEmail = result.Email,
                        CustomerName = result.Name,
                        CustomerPhone = result.PhoneNumber,
                        DatePlaced = result.DatePlaced,
                        Delivered = result.Delivered,
                        BankTransfer = result.BankTransfer,
                        CashTransaction = result.CashTransaction,
                        PaymentReceived = result.PaymentReceived,
                        InvoiceSent = result.InvoiceSent,
                        ShippingTotal = result.ShippingTotal,
                        ContactName = result.ContactName
                    };

                    viewModel.OrderDetails = db.GetCashOrderDetails(viewModel.Order.OrderId)
                        .Select(c => new CashOrderDetail
                        {
                            CashOrderId = c.CashOrderId,
                            CashOrderItemId = c.CashOrderItemId,
                            CustodianNumber = c.CustodianNumber,
                            Price = c.Price,
                            ProductNumber = c.ProductNumber,
                            Quantity = c.Quantity,
                            SupplierNumber = c.SupplierNumber,
                            Description = c.Description,
                            SupplierName = c.SupplierName,
                            Weight = c.Weight,
                            OrderItemTotal = c.OrderItemTotal
                        })
                        .ToList();

                    model.Add(viewModel);
                }
            }

            return model;
        }

        public static List<CashOrderViewModel> GetCashPaymentOrders()
        {
            List<CashOrderViewModel> model = new List<CashOrderViewModel>();

            using (FreeMarketEntities db = new FreeMarketEntities())
            {
                List<FilterCashOrder_Result> orders = db.FilterCashOrder("all")
                    .Where(c => c.CashTransaction == true).ToList();

                if (orders == null)
                    return model;

                foreach (FilterCashOrder_Result result in orders)
                {
                    CashOrderViewModel viewModel = new CashOrderViewModel();
                    viewModel.Order = new CashOrder
                    {
                        CashCustomerId = (int)result.CashCustomerId,
                        OrderId = (int)result.OrderId,
                        Total = (int)result.Total,
                        CustomerDeliveryAddress = result.DeliveryAddress,
                        CustomerEmail = result.Email,
                        CustomerName = result.Name,
                        CustomerPhone = result.PhoneNumber,
                        DatePlaced = result.DatePlaced,
                        Delivered = result.Delivered,
                        BankTransfer = result.BankTransfer,
                        CashTransaction = result.CashTransaction,
                        PaymentReceived = result.PaymentReceived,
                        InvoiceSent = result.InvoiceSent,
                        ShippingTotal = result.ShippingTotal,
                        ContactName = result.ContactName
                    };

                    viewModel.OrderDetails = db.GetCashOrderDetails(viewModel.Order.OrderId)
                        .Select(c => new CashOrderDetail
                        {
                            CashOrderId = c.CashOrderId,
                            CashOrderItemId = c.CashOrderItemId,
                            CustodianNumber = c.CustodianNumber,
                            Price = c.Price,
                            ProductNumber = c.ProductNumber,
                            Quantity = c.Quantity,
                            SupplierNumber = c.SupplierNumber,
                            Description = c.Description,
                            SupplierName = c.SupplierName,
                            Weight = c.Weight,
                            OrderItemTotal = c.OrderItemTotal
                        })
                        .ToList();

                    model.Add(viewModel);
                }
            }

            return model;
        }

        public static List<CashOrderViewModel> GetBankTransferPaymentOrders()
        {
            List<CashOrderViewModel> model = new List<CashOrderViewModel>();

            using (FreeMarketEntities db = new FreeMarketEntities())
            {
                List<FilterCashOrder_Result> orders = db.FilterCashOrder("all")
                    .Where(c => c.BankTransfer == true).ToList();

                if (orders == null)
                    return model;

                foreach (FilterCashOrder_Result result in orders)
                {
                    CashOrderViewModel viewModel = new CashOrderViewModel();
                    viewModel.Order = new CashOrder
                    {
                        CashCustomerId = (int)result.CashCustomerId,
                        OrderId = (int)result.OrderId,
                        Total = (int)result.Total,
                        CustomerDeliveryAddress = result.DeliveryAddress,
                        CustomerEmail = result.Email,
                        CustomerName = result.Name,
                        CustomerPhone = result.PhoneNumber,
                        DatePlaced = result.DatePlaced,
                        Delivered = result.Delivered,
                        BankTransfer = result.BankTransfer,
                        CashTransaction = result.CashTransaction,
                        PaymentReceived = result.PaymentReceived,
                        InvoiceSent = result.InvoiceSent,
                        ShippingTotal = result.ShippingTotal,
                        ContactName = result.ContactName
                    };

                    viewModel.OrderDetails = db.GetCashOrderDetails(viewModel.Order.OrderId)
                        .Select(c => new CashOrderDetail
                        {
                            CashOrderId = c.CashOrderId,
                            CashOrderItemId = c.CashOrderItemId,
                            CustodianNumber = c.CustodianNumber,
                            Price = c.Price,
                            ProductNumber = c.ProductNumber,
                            Quantity = c.Quantity,
                            SupplierNumber = c.SupplierNumber,
                            Description = c.Description,
                            SupplierName = c.SupplierName,
                            Weight = c.Weight,
                            OrderItemTotal = c.OrderItemTotal
                        })
                        .ToList();

                    model.Add(viewModel);
                }
            }

            return model;
        }

        public static CashOrderViewModel GetOrder(int id)
        {
            CashOrderViewModel model = new CashOrderViewModel();

            if (id == 0)
                return model;

            using (FreeMarketEntities db = new FreeMarketEntities())
            {
                model.Order = db.CashOrders.Find(id);
                CashCustomer customer = db.CashCustomers.Find(model.Order.CashCustomerId);

                model.Order.CustomerDeliveryAddress = customer.DeliveryAddress;
                model.Order.CustomerEmail = customer.Email;
                model.Order.CustomerName = customer.Name;
                model.Order.CustomerPhone = customer.PhoneNumber;
                model.Order.ClientVatNumber = customer.ClientVatNumber;
                model.Order.ContactName = customer.ContactName;
                model.Order.HeadOfficeAddress = customer.HeadOfficeAddress;

                model.OrderDetails = db.GetCashOrderDetails(model.Order.OrderId)
                    .Select(c => new CashOrderDetail
                    {
                        CashOrderId = c.CashOrderId,
                        CashOrderItemId = c.CashOrderItemId,
                        CustodianNumber = c.CustodianNumber,
                        Price = c.Price,
                        ProductNumber = c.ProductNumber,
                        Quantity = c.Quantity,
                        SupplierNumber = c.SupplierNumber,
                        Description = c.Description,
                        SupplierName = c.SupplierName,
                        Weight = (int)c.Weight,
                        OrderItemTotal = c.OrderItemTotal
                    })
                    .ToList();

                model.Products = ProductCollection.GetAllProducts();

                for (int i = 0; i < model.Products.Products.Count; i++)
                {
                    CashOrderDetail qty = model.OrderDetails
                        .Where(c => c.ProductNumber == model.Products.Products[i].ProductNumber && c.SupplierNumber == model.Products.Products[i].SupplierNumber)
                        .FirstOrDefault();

                    if (qty != null)
                        model.Products.Products[i].CashQuantity = qty.Quantity;
                    else
                        model.Products.Products[i].CashQuantity = 0;

                    foreach (SelectListItem item in model.Products.Products[i].Prices)
                    {
                        decimal price = model.OrderDetails.Where(c => c.ProductNumber == model.Products.Products[i].ProductNumber)
                            .Select(c => c.Price)
                            .FirstOrDefault();

                        if (price > 0 && item.Value == price.ToString())
                        {
                            item.Selected = true;
                        }
                    }

                }

                model.Custodians = db.Custodians.Select
                    (c => new SelectListItem
                    {
                        Text = c.CustodianName,
                        Value = c.CustodianNumber.ToString()
                    }).ToList();

                model.SelectedCustomerType = customer.Type;

                if (model.Order.ShippingTotal > 0)
                {
                    model.SelectedShippingType = "Charge";
                    model.ShippingType = new List<SelectListItem> {
                    new SelectListItem { Text = "Free Delivery", Selected = false, Value = "Free" },
                    new SelectListItem { Text = "Specify Delivery Cost", Selected = true, Value = "Charge" }
                    };
                }
                else
                {
                    model.SelectedShippingType = "Free";
                    model.ShippingType = new List<SelectListItem> {
                    new SelectListItem { Text = "Free Delivery", Selected = true, Value = "Free" },
                    new SelectListItem { Text = "Specify Delivery Cost", Selected = false, Value = "Charge" }
                    };
                }
            }

            return model;
        }

        public static CashOrderViewModel CreateNewOrder(int id = 0)
        {
            CashOrderViewModel model = new CashOrderViewModel();

            model.Order = new CashOrder();

            using (FreeMarketEntities db = new FreeMarketEntities())
            {
                if (id != 0)
                {
                    CashCustomer customer = db.CashCustomers.Find(id);
                    if (customer != null)
                    {
                        model.Order.CashCustomerId = customer.Id;
                        model.Order.CustomerDeliveryAddress = customer.DeliveryAddress;
                        model.Order.CustomerEmail = customer.Email;
                        model.Order.CustomerName = customer.Name;
                        model.Order.CustomerPhone = customer.PhoneNumber;
                        model.Order.ClientVatNumber = customer.ClientVatNumber;
                        model.Order.ContactName = customer.ContactName;
                        model.Order.HeadOfficeAddress = customer.HeadOfficeAddress;
                    }
                    model.SelectedCustomerType = customer.Type;
                }

                model.Products = ProductCollection.GetAllProducts();

                model.Custodians = db.Custodians.Select
                    (c => new SelectListItem
                    {
                        Text = c.CustodianName,
                        Value = c.CustodianNumber.ToString()
                    }).ToList();
            }

            model.OrderDetails = new List<CashOrderDetail>();

            model.BankAcountOptions = new List<string> { "Personal", "Business" };
            model.SelectedBankAcountOption = "Personal";

            model.CustomerType = new List<SelectListItem> {
                new SelectListItem { Text = "Normal Customer", Selected = false, Value = "NormalCustomer" },
                new SelectListItem { Text = "Company / Retail", Selected = false, Value = "CompanyRetail" }
            };

            model.ShippingType = new List<SelectListItem> {
                new SelectListItem { Text = "Free Delivery", Selected = true, Value = "Free" },
                new SelectListItem { Text = "Specify Delivery Cost", Selected = false, Value = "Charge" }
            };

            model.Order.ShippingTotal = 0;

            return model;
        }

        public static void InitializeDropDowns(CashOrderViewModel model)
        {
            using (FreeMarketEntities db = new FreeMarketEntities())
            {
                model.Custodians = db.Custodians.Select
                    (c => new SelectListItem
                    {
                        Text = c.CustodianName,
                        Value = c.CustodianNumber.ToString()
                    }).ToList();

                model.BankAcountOptions = new List<string> { "Personal", "Business" };
                model.SelectedBankAcountOption = "Personal";

                model.CustomerType = new List<SelectListItem> {
                    new SelectListItem { Text = "Normal Customer", Selected = false, Value = "NormalCustomer" },
                    new SelectListItem { Text = "Company / Retail", Selected = false, Value = "CompanyRetail" }
                };

                model.ShippingType = new List<SelectListItem> {
                    new SelectListItem { Text = "Free Delivery", Selected = true, Value = "Free" },
                    new SelectListItem { Text = "Specify Delivery Cost", Selected = false, Value = "Charge" }
                };

            }
        }
    }
}