using System;
using System.Collections.Generic;
using System.Linq;

namespace FreeMarket.Models
{
    public class CashCustomerMetaInformation
    {
        public List<ProductMetaInformation> ProductsBought { get; set; }
        public decimal TotalSalesAccrossAllProducts { get; set; }

        public CashCustomerMetaInformation()
        {
            ProductsBought = new List<ProductMetaInformation>();
        }

        public void ExtractMetaData(List<CashOrder> cashOrders, List<List<CashOrderDetail>> cashOrderDetails)
        {
            foreach (List<CashOrderDetail> detailsList in cashOrderDetails)
            {
                foreach (CashOrderDetail detail in detailsList)
                {
                    if (ProductsBought
                        .Where(c => c.Product.ProductNumber == detail.ProductNumber && c.Product.SupplierNumber == detail.SupplierNumber)
                        .FirstOrDefault() == null)
                    {
                        Product p = Product.GetProduct(detail.ProductNumber, detail.SupplierNumber);
                        int totalSold = detailsList.Where(c => c.ProductNumber == detail.ProductNumber
                            && c.SupplierNumber == detail.SupplierNumber)
                            .Sum(c => c.Quantity);
                        decimal totalWeightSold = totalSold * p.Weight;
                        int latestOrder = detailsList
                            .Where(c => c.ProductNumber == detail.ProductNumber && c.SupplierNumber == detail.SupplierNumber)
                            .Max(c => c.CashOrderId);
                        DateTime? lastPurchased = cashOrders.Where(c => c.OrderId == latestOrder).FirstOrDefault().DatePlaced;
                        decimal totalSales = detailsList.Where(c => c.ProductNumber == detail.ProductNumber && c.SupplierNumber == detail.SupplierNumber)
                            .Sum(c => c.OrderItemTotal);

                        ProductsBought.Add(new ProductMetaInformation()
                        {
                            Product = p,
                            TotalQuantitySold = totalSold,
                            TotalWeightSold = totalWeightSold,
                            LastPurchased = lastPurchased,
                            TotalSales = totalSales
                        });
                    }
                }

            }

            TotalSalesAccrossAllProducts = ProductsBought.Sum(c => c.TotalSales);
        }
    }
}