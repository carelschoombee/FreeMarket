using System.Collections.Generic;
using System.Linq;

namespace FreeMarket.Models
{
    public class ReportCustomer
    {
        public CashCustomer Customer { get; set; }
        public List<CashOrder> Orders { get; set; }
        public List<List<CashOrderDetail>> OrderDetails { get; set; }
        public CashCustomerMetaInformation MetaData { get; set; }

        public ReportCustomer()
        {
            Customer = new CashCustomer();
            Orders = new List<CashOrder>();
            OrderDetails = new List<List<CashOrderDetail>>();
        }

        public static List<ReportCustomer> GetReportCustomers(string filterType)
        {
            List<ReportCustomer> reportCustomers = new List<ReportCustomer>();

            List<CashCustomer> cashCustomers = new List<CashCustomer>();

            using (FreeMarketEntities db = new FreeMarketEntities())
            {
                if (filterType == "all")
                    cashCustomers = db.CashCustomers
                    .ToList();
                else
                    cashCustomers = db.CashCustomers
                    .Where(c => c.Type == filterType)
                    .ToList();

                foreach (CashCustomer x in cashCustomers)
                {
                    List<CashOrder> cashOrders = db.CashOrders
                        .Where(c => c.CashCustomerId == x.Id && c.Status == "Completed")
                        .OrderByDescending(c => c.DatePlaced)
                        .ToList();

                    List<List<CashOrderDetail>> allOrderDetails = new List<List<CashOrderDetail>>();
                    List<CashOrderDetail> cashOrderDetails = new List<CashOrderDetail>();
                    List<CashCustomerMetaInformation> MetaData = new List<CashCustomerMetaInformation>();

                    foreach (CashOrder o in cashOrders)
                    {
                        cashOrderDetails = db.CashOrderDetails
                            .Where(c => c.CashOrderId == o.OrderId)
                            .OrderByDescending(c => c.Quantity)
                            .ToList();

                        allOrderDetails.Add(cashOrderDetails);
                    }

                    CashCustomerMetaInformation metaData = new CashCustomerMetaInformation();
                    metaData.ExtractMetaData(cashOrders, allOrderDetails);

                    reportCustomers.Add(new ReportCustomer
                    {
                        Customer = x,
                        Orders = cashOrders,
                        OrderDetails = allOrderDetails,
                        MetaData = metaData
                    });
                }
            }

            reportCustomers = reportCustomers
                .OrderByDescending(c => c.MetaData.TotalSalesAccrossAllProducts)
                .ToList();

            return reportCustomers;
        }
    }
}