using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;

namespace FreeMarket.Models
{
    public class Dashboard
    {
        public SalesInfo SalesInformation { get; set; }

        public RatingsInfo RatingsInformation { get; set; }

        [DisplayName("Filter")]
        [RegularExpression(@"([a-zA-Z0-9_@\s]+)", ErrorMessage = "Alphanumeric characters only")]
        public string CustomerSearchCriteria { get; set; }
        public List<AspNetUser> Customers { get; set; }

        public List<OrderHeader> ConfirmedOrders { get; set; }
        public List<OrderHeader> InTransitOrders { get; set; }
        public List<OrderHeader> RefundPending { get; set; }
        public List<OrderHeader> RefundableOrders { get; set; }
        public List<CashOrderViewModel> CashOrders { get; set; }

        [DisplayName("Time Period")]
        [Required]
        public string SelectedYear { get; set; }
        public List<SelectListItem> YearOptions { get; set; }

        [DisplayName("Time Period")]
        [Required]
        public DateTime SelectedMonth { get; set; }

        public string Period { get; set; }

        [DisplayName("Filter")]
        public int OrderSearchCriteria { get; set; }
        public OrderHeaderViewModel SearchedOrder { get; set; }

        [DisplayName("Filter")]
        public string AuditSearchCriteria { get; set; }
        public int WebsiteHits { get; set; }

        public string SMSCredits { get; set; }

        [DisplayName("Filter")]
        public string CashSalesCriteria { get; set; }

        [DisplayName("Filter")]
        public bool CashSalesFilter { get; set; }

        public decimal TotalSales { get; set; }

        public string SelectedCashOrderSearchCriteria { get; set; }
        public List<SelectListItem> CashOrderSearchCriteria { get; set; }

        public string SelectedReport { get; set; }
        public List<SelectListItem> SelectedReportOptions { get; set; }

        public List<ReportCustomer> ReportCustomers { get; set; }

        public Dashboard()
        {

        }

        public Dashboard(string year, string period)
        {
            if (!string.IsNullOrEmpty(year))
                SelectedYear = year;
            else
                SelectedYear = DateTime.Now.Year.ToString();

            Period = period;
            SelectedMonth = DateTime.Now;
            SalesInformation = new SalesInfo(int.Parse(SelectedYear));

            Initialize();
        }

        public Dashboard(DateTime date, string period)
        {
            SelectedYear = null;

            Period = period;
            SelectedMonth = date;
            SalesInformation = new SalesInfo(date);

            Initialize();
        }

        public async void Initialize()
        {
            using (FreeMarketEntities db = new FreeMarketEntities())
            {
                DateTime minDate;
                if (db.OrderHeaders.FirstOrDefault() == null)
                    minDate = DateTime.Now.AddYears(-1);
                else
                    minDate = (DateTime)db.OrderHeaders.Min(c => c.OrderDatePlaced);

                DateTime maxDate = DateTime.Now;

                int i = minDate.Year;

                YearOptions = new List<SelectListItem>();

                YearOptions.Add(new SelectListItem
                {
                    Text = "Since Inception",
                    Value = "0"
                });

                while (i <= maxDate.Year)
                {
                    YearOptions.Add(new SelectListItem
                    {
                        Text = i.ToString() + "/" + (i + 1).ToString(),
                        Value = i.ToString(),
                        Selected = (i.ToString() == SelectedYear ? true : false)
                    });
                    i++;
                }

                RatingsInformation = new RatingsInfo();
                Customers = new List<AspNetUser>();
                ConfirmedOrders = db.OrderHeaders.Where(c => c.OrderStatus == "Confirmed").OrderBy(c => c.DeliveryDate).ToList();
                InTransitOrders = db.OrderHeaders.Where(c => c.OrderStatus == "InTransit").OrderBy(c => c.DeliveryDate).ToList();
                RefundPending = db.OrderHeaders.Where(c => c.OrderStatus == "RefundPending").OrderBy(c => c.DeliveryDate).ToList();
                RefundableOrders = db.OrderHeaders.Where(c => c.OrderStatus == "Confirmed" || c.OrderStatus == "InTransit").ToList();
                CashOrders = CashOrderViewModel.GetOrders("all");

                TotalSales = SalesInformation.SalesDetails.Sum(c => c.Value);

                CashOrderSearchCriteria = new List<SelectListItem>()
                {
                    new SelectListItem() { Text = "All Records", Value = "AllRecords", Selected = true },
                    new SelectListItem() { Text = "Outstanding Payments", Value = "OutstandingPayments", Selected = false },
                    new SelectListItem() { Text = "Undelivered Orders", Value = "UndeliveredOrders", Selected = false },
                    new SelectListItem() { Text = "Cash Transactions", Value = "CashTransactions", Selected = false },
                    new SelectListItem() { Text = "Bank Transfers", Value = "BankTransfers", Selected = false },
                    new SelectListItem() { Text = "Text Search", Value = "TextSearch", Selected = false }
                };

                SelectedReportOptions = new List<SelectListItem>()
                {
                    new SelectListItem() { Text = "Sales by Customer, All", Value = "SalesCustomerAll", Selected = true },
                    new SelectListItem() { Text = "Sales by Customer, Individual", Value = "SalesCustomerIndividual", Selected = false },
                    new SelectListItem() { Text = "Sales by Customer, Company", Value = "SalesCustomerCompany", Selected = false }
                };

                List<AuditUser> hits = new List<AuditUser>();
                hits = db.AuditUsers.Where(c => c.Action == 32).ToList();
                if (hits.Count > 0)
                {
                    WebsiteHits = hits.Count;
                }

                try
                {
                    SMSHelper helper = new SMSHelper();
                    SMSCredits = await helper.CheckCredits();
                }
                catch (Exception e)
                {
                    ExceptionLogging.LogException(e);
                }

                ReportCustomers = ReportCustomer.GetReportCustomers("all");
            }
        }
    }
}