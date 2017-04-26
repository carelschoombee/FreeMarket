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

        public void ExtractMetaData(List<CashOrder> cashOrders, List<CashOrderDetail> cashOrderDetails)
        {
            foreach (CashOrderDetail detail in cashOrderDetails)
            {
                if (ProductsBought
                    .Where(c => c.Product.ProductNumber == detail.ProductNumber && c.Product.SupplierNumber == detail.SupplierNumber)
                    .FirstOrDefault() == null)
                {
                    Product p = Product.GetProduct(detail.ProductNumber, detail.SupplierNumber);

                    ProductsBought.Add(new ProductMetaInformation()
                    {
                        Product = p,
                    });
                }
            }

            foreach (ProductMetaInformation p in ProductsBought)
            {
                int totalSold = cashOrderDetails.Where(c => c.ProductNumber == p.Product.ProductNumber
                            && c.SupplierNumber == p.Product.SupplierNumber)
                            .Sum(c => c.Quantity);
                decimal totalWeightSold = totalSold * p.Product.Weight;
                int latestOrder = cashOrderDetails
                    .Where(c => c.ProductNumber == p.Product.ProductNumber && c.SupplierNumber == p.Product.SupplierNumber)
                    .Max(c => c.CashOrderId);
                DateTime? lastPurchased = cashOrders.Where(c => c.OrderId == latestOrder).FirstOrDefault().DatePlaced;
                decimal totalSales = cashOrderDetails.Where(c => c.ProductNumber == p.Product.ProductNumber && c.SupplierNumber == p.Product.SupplierNumber)
                    .Sum(c => c.OrderItemTotal);

                p.TotalQuantitySold = totalSold;
                p.TotalWeightSold = totalWeightSold;
                p.LastPurchased = lastPurchased;
                p.TotalSales = totalSales;
            }

            TotalSalesAccrossAllProducts = ProductsBought.Sum(c => c.TotalSales);
        }
    }
}