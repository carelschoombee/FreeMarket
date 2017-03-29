﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FreeMarket.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class FreeMarketEntities : DbContext
    {
        public FreeMarketEntities()
            : base("name=FreeMarketEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<ActivityLogging> ActivityLoggings { get; set; }
        public virtual DbSet<AddressName> AddressNames { get; set; }
        public virtual DbSet<Courier> Couriers { get; set; }
        public virtual DbSet<CourierStockMovementLog> CourierStockMovementLogs { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<ExceptionLogging> ExceptionLoggings { get; set; }
        public virtual DbSet<Location> Locations { get; set; }
        public virtual DbSet<OrderStatu> OrderStatus { get; set; }
        public virtual DbSet<PaymentGatewayParameter> PaymentGatewayParameters { get; set; }
        public virtual DbSet<PaymentGatewayPaymentMethod> PaymentGatewayPaymentMethods { get; set; }
        public virtual DbSet<PaymentGatewayTransactionStatu> PaymentGatewayTransactionStatus { get; set; }
        public virtual DbSet<PreferredCommunicationMethod> PreferredCommunicationMethods { get; set; }
        public virtual DbSet<ProductCustodian> ProductCustodians { get; set; }
        public virtual DbSet<ProductPicture> ProductPictures { get; set; }
        public virtual DbSet<SiteConfiguration> SiteConfigurations { get; set; }
        public virtual DbSet<Supplier> Suppliers { get; set; }
        public virtual DbSet<SystemAction> SystemActions { get; set; }
        public virtual DbSet<AuditUser> AuditUsers { get; set; }
        public virtual DbSet<SitePicture> SitePictures { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<SupplierLocation> SupplierLocations { get; set; }
        public virtual DbSet<CustomerAddress> CustomerAddresses { get; set; }
        public virtual DbSet<CourierReview> CourierReviews { get; set; }
        public virtual DbSet<ProductReview> ProductReviews { get; set; }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<Special> Specials { get; set; }
        public virtual DbSet<PriceHistory> PriceHistories { get; set; }
        public virtual DbSet<PostalFee> PostalFees { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        public virtual DbSet<OrderHeader> OrderHeaders { get; set; }
        public virtual DbSet<Custodian> Custodians { get; set; }
        public virtual DbSet<PaymentGatewayMessage> PaymentGatewayMessages { get; set; }
        public virtual DbSet<Support> Supports { get; set; }
        public virtual DbSet<CashOrderDetail> CashOrderDetails { get; set; }
        public virtual DbSet<ProductSupplier> ProductSuppliers { get; set; }
        public virtual DbSet<CharliesTransportCourierFeeReference> CharliesTransportCourierFeeReferences { get; set; }
        public virtual DbSet<TimeFreightCourierFeeReference> TimeFreightCourierFeeReferences { get; set; }
        public virtual DbSet<CashCustomer> CashCustomers { get; set; }
        public virtual DbSet<CashOrder> CashOrders { get; set; }
        public virtual DbSet<FreeMarketOwner> FreeMarketOwners { get; set; }
    
        public virtual ObjectResult<GetAllCouriersReviewList_Result> GetAllCouriersReviewList()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetAllCouriersReviewList_Result>("GetAllCouriersReviewList");
        }
    
        public virtual ObjectResult<GetAllProductsByDepartment_Result> GetAllProductsByDepartment(Nullable<int> departmentNumber)
        {
            var departmentNumberParameter = departmentNumber.HasValue ?
                new ObjectParameter("DepartmentNumber", departmentNumber) :
                new ObjectParameter("DepartmentNumber", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetAllProductsByDepartment_Result>("GetAllProductsByDepartment", departmentNumberParameter);
        }
    
        public virtual ObjectResult<GetAllProductsReview_Result> GetAllProductsReview()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetAllProductsReview_Result>("GetAllProductsReview");
        }
    
        public virtual ObjectResult<GetDeliveryTypeHistory_Result> GetDeliveryTypeHistory(string customerNumber)
        {
            var customerNumberParameter = customerNumber != null ?
                new ObjectParameter("CustomerNumber", customerNumber) :
                new ObjectParameter("CustomerNumber", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetDeliveryTypeHistory_Result>("GetDeliveryTypeHistory", customerNumberParameter);
        }
    
        public virtual ObjectResult<GetDetailsForShoppingCart_Result> GetDetailsForShoppingCart(Nullable<int> orderNumber)
        {
            var orderNumberParameter = orderNumber.HasValue ?
                new ObjectParameter("OrderNumber", orderNumber) :
                new ObjectParameter("OrderNumber", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetDetailsForShoppingCart_Result>("GetDetailsForShoppingCart", orderNumberParameter);
        }
    
        public virtual ObjectResult<GetItemHistory_Result> GetItemHistory(string customerNumber)
        {
            var customerNumberParameter = customerNumber != null ?
                new ObjectParameter("CustomerNumber", customerNumber) :
                new ObjectParameter("CustomerNumber", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetItemHistory_Result>("GetItemHistory", customerNumberParameter);
        }
    
        public virtual ObjectResult<Nullable<int>> GetNumberOfItemsInOrder(Nullable<int> orderNumber)
        {
            var orderNumberParameter = orderNumber.HasValue ?
                new ObjectParameter("OrderNumber", orderNumber) :
                new ObjectParameter("OrderNumber", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("GetNumberOfItemsInOrder", orderNumberParameter);
        }
    
        public virtual ObjectResult<GetPriceHistories_Result> GetPriceHistories()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetPriceHistories_Result>("GetPriceHistories");
        }
    
        public virtual ObjectResult<Nullable<int>> ValidateSpecialDeliveryCode(Nullable<int> postalCode)
        {
            var postalCodeParameter = postalCode.HasValue ?
                new ObjectParameter("postalCode", postalCode) :
                new ObjectParameter("postalCode", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("ValidateSpecialDeliveryCode", postalCodeParameter);
        }
    
        public virtual ObjectResult<GetAllProductsInOrder_Result> GetAllProductsInOrder(Nullable<int> orderNumber)
        {
            var orderNumberParameter = orderNumber.HasValue ?
                new ObjectParameter("orderNumber", orderNumber) :
                new ObjectParameter("orderNumber", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetAllProductsInOrder_Result>("GetAllProductsInOrder", orderNumberParameter);
        }
    
        public virtual ObjectResult<GetAllCouriersReview_Result> GetAllCouriersReview(Nullable<int> orderNumber)
        {
            var orderNumberParameter = orderNumber.HasValue ?
                new ObjectParameter("orderNumber", orderNumber) :
                new ObjectParameter("orderNumber", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetAllCouriersReview_Result>("GetAllCouriersReview", orderNumberParameter);
        }
    
        public virtual ObjectResult<GetNumberOfItemsStatistic_Result> GetNumberOfItemsStatistic()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetNumberOfItemsStatistic_Result>("GetNumberOfItemsStatistic");
        }
    
        public virtual ObjectResult<GetAllProductCustodians_Result> GetAllProductCustodians()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetAllProductCustodians_Result>("GetAllProductCustodians");
        }
    
        public virtual ObjectResult<GetCustodianInfo_Result> GetCustodianInfo(Nullable<int> productNumber, Nullable<int> supplierNumber)
        {
            var productNumberParameter = productNumber.HasValue ?
                new ObjectParameter("productNumber", productNumber) :
                new ObjectParameter("productNumber", typeof(int));
    
            var supplierNumberParameter = supplierNumber.HasValue ?
                new ObjectParameter("supplierNumber", supplierNumber) :
                new ObjectParameter("supplierNumber", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetCustodianInfo_Result>("GetCustodianInfo", productNumberParameter, supplierNumberParameter);
        }
    
        public virtual ObjectResult<GetAllProductsIncludingDeactivated_Result> GetAllProductsIncludingDeactivated()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetAllProductsIncludingDeactivated_Result>("GetAllProductsIncludingDeactivated");
        }
    
        public virtual ObjectResult<ValidatePaymentAmount_Result> ValidatePaymentAmount(string orderNumber)
        {
            var orderNumberParameter = orderNumber != null ?
                new ObjectParameter("orderNumber", orderNumber) :
                new ObjectParameter("orderNumber", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<ValidatePaymentAmount_Result>("ValidatePaymentAmount", orderNumberParameter);
        }
    
        public virtual ObjectResult<GetOrderDetails_Result> GetOrderDetails(Nullable<int> orderNumber)
        {
            var orderNumberParameter = orderNumber.HasValue ?
                new ObjectParameter("orderNumber", orderNumber) :
                new ObjectParameter("orderNumber", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetOrderDetails_Result>("GetOrderDetails", orderNumberParameter);
        }
    
        public virtual ObjectResult<GetOrderReport_Result> GetOrderReport(Nullable<int> orderNumber)
        {
            var orderNumberParameter = orderNumber.HasValue ?
                new ObjectParameter("OrderNumber", orderNumber) :
                new ObjectParameter("OrderNumber", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetOrderReport_Result>("GetOrderReport", orderNumberParameter);
        }
    
        public virtual ObjectResult<GetDeliveryLabels_Result> GetDeliveryLabels()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetDeliveryLabels_Result>("GetDeliveryLabels");
        }
    
        public virtual ObjectResult<GetOrderHistory_Result> GetOrderHistory(string customerNumber)
        {
            var customerNumberParameter = customerNumber != null ?
                new ObjectParameter("CustomerNumber", customerNumber) :
                new ObjectParameter("CustomerNumber", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetOrderHistory_Result>("GetOrderHistory", customerNumberParameter);
        }
    
        public virtual ObjectResult<GetPaymentGatewayMessages_Result> GetPaymentGatewayMessages(string orderNumber)
        {
            var orderNumberParameter = orderNumber != null ?
                new ObjectParameter("orderNumber", orderNumber) :
                new ObjectParameter("orderNumber", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetPaymentGatewayMessages_Result>("GetPaymentGatewayMessages", orderNumberParameter);
        }
    
        public virtual ObjectResult<GetCashOrderDetails_Result> GetCashOrderDetails(Nullable<int> orderNumber)
        {
            var orderNumberParameter = orderNumber.HasValue ?
                new ObjectParameter("orderNumber", orderNumber) :
                new ObjectParameter("orderNumber", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetCashOrderDetails_Result>("GetCashOrderDetails", orderNumberParameter);
        }
    
        public virtual ObjectResult<Nullable<decimal>> CalculateDeliveryFee(Nullable<decimal> weight, Nullable<int> orderNumber)
        {
            var weightParameter = weight.HasValue ?
                new ObjectParameter("weight", weight) :
                new ObjectParameter("weight", typeof(decimal));
    
            var orderNumberParameter = orderNumber.HasValue ?
                new ObjectParameter("orderNumber", orderNumber) :
                new ObjectParameter("orderNumber", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<decimal>>("CalculateDeliveryFee", weightParameter, orderNumberParameter);
        }
    
        public virtual ObjectResult<Nullable<decimal>> CalculateDeliveryFeeAdhoc(Nullable<decimal> weight, Nullable<int> postalCode)
        {
            var weightParameter = weight.HasValue ?
                new ObjectParameter("weight", weight) :
                new ObjectParameter("weight", typeof(decimal));
    
            var postalCodeParameter = postalCode.HasValue ?
                new ObjectParameter("postalCode", postalCode) :
                new ObjectParameter("postalCode", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<decimal>>("CalculateDeliveryFeeAdhoc", weightParameter, postalCodeParameter);
        }
    
        public virtual ObjectResult<Nullable<decimal>> CalculatePostOfficeFee(Nullable<decimal> totalWeight)
        {
            var totalWeightParameter = totalWeight.HasValue ?
                new ObjectParameter("totalWeight", totalWeight) :
                new ObjectParameter("totalWeight", typeof(decimal));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<decimal>>("CalculatePostOfficeFee", totalWeightParameter);
        }
    
        public virtual ObjectResult<Nullable<decimal>> CalculateTimeFreightFee(Nullable<decimal> weight, Nullable<int> orderNumber, ObjectParameter courierFee)
        {
            var weightParameter = weight.HasValue ?
                new ObjectParameter("weight", weight) :
                new ObjectParameter("weight", typeof(decimal));
    
            var orderNumberParameter = orderNumber.HasValue ?
                new ObjectParameter("orderNumber", orderNumber) :
                new ObjectParameter("orderNumber", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<decimal>>("CalculateTimeFreightFee", weightParameter, orderNumberParameter, courierFee);
        }
    
        public virtual ObjectResult<Nullable<decimal>> CalculateTimeFreightFeeAdhoc(Nullable<decimal> weight, Nullable<int> postalCode, ObjectParameter courierFee)
        {
            var weightParameter = weight.HasValue ?
                new ObjectParameter("weight", weight) :
                new ObjectParameter("weight", typeof(decimal));
    
            var postalCodeParameter = postalCode.HasValue ?
                new ObjectParameter("postalCode", postalCode) :
                new ObjectParameter("postalCode", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<decimal>>("CalculateTimeFreightFeeAdhoc", weightParameter, postalCodeParameter, courierFee);
        }
    
        public virtual ObjectResult<GetAllProducts_Result> GetAllProducts()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetAllProducts_Result>("GetAllProducts");
        }
    
        public virtual ObjectResult<GetProduct_Result> GetProduct(Nullable<int> productNumber, Nullable<int> supplierNumber)
        {
            var productNumberParameter = productNumber.HasValue ?
                new ObjectParameter("ProductNumber", productNumber) :
                new ObjectParameter("ProductNumber", typeof(int));
    
            var supplierNumberParameter = supplierNumber.HasValue ?
                new ObjectParameter("SupplierNumber", supplierNumber) :
                new ObjectParameter("SupplierNumber", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetProduct_Result>("GetProduct", productNumberParameter, supplierNumberParameter);
        }
    
        public virtual ObjectResult<Nullable<decimal>> CalculateCharliesFee(Nullable<decimal> weight, Nullable<int> orderNumber, ObjectParameter courierFee)
        {
            var weightParameter = weight.HasValue ?
                new ObjectParameter("weight", weight) :
                new ObjectParameter("weight", typeof(decimal));
    
            var orderNumberParameter = orderNumber.HasValue ?
                new ObjectParameter("orderNumber", orderNumber) :
                new ObjectParameter("orderNumber", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<decimal>>("CalculateCharliesFee", weightParameter, orderNumberParameter, courierFee);
        }
    
        public virtual ObjectResult<Nullable<decimal>> CalculateCharliesFeeAdhoc(Nullable<decimal> weight, Nullable<int> postalCode, ObjectParameter courierFee)
        {
            var weightParameter = weight.HasValue ?
                new ObjectParameter("weight", weight) :
                new ObjectParameter("weight", typeof(decimal));
    
            var postalCodeParameter = postalCode.HasValue ?
                new ObjectParameter("postalCode", postalCode) :
                new ObjectParameter("postalCode", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<decimal>>("CalculateCharliesFeeAdhoc", weightParameter, postalCodeParameter, courierFee);
        }
    
        public virtual ObjectResult<Nullable<decimal>> CalculateLocalDeliveryFee(Nullable<decimal> weight, Nullable<int> orderNumber)
        {
            var weightParameter = weight.HasValue ?
                new ObjectParameter("weight", weight) :
                new ObjectParameter("weight", typeof(decimal));
    
            var orderNumberParameter = orderNumber.HasValue ?
                new ObjectParameter("orderNumber", orderNumber) :
                new ObjectParameter("orderNumber", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<decimal>>("CalculateLocalDeliveryFee", weightParameter, orderNumberParameter);
        }
    
        public virtual ObjectResult<Nullable<decimal>> CalculateLocalDeliveryFeeAdhoc(Nullable<decimal> weight, Nullable<int> postalCode)
        {
            var weightParameter = weight.HasValue ?
                new ObjectParameter("weight", weight) :
                new ObjectParameter("weight", typeof(decimal));
    
            var postalCodeParameter = postalCode.HasValue ?
                new ObjectParameter("postalCode", postalCode) :
                new ObjectParameter("postalCode", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<decimal>>("CalculateLocalDeliveryFeeAdhoc", weightParameter, postalCodeParameter);
        }
    
        public virtual ObjectResult<GetDeliveryLabelsCashOrder_Result> GetDeliveryLabelsCashOrder()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetDeliveryLabelsCashOrder_Result>("GetDeliveryLabelsCashOrder");
        }
    
        public virtual ObjectResult<FilterAuditUser_Result> FilterAuditUser(string filterCriteria)
        {
            var filterCriteriaParameter = filterCriteria != null ?
                new ObjectParameter("filterCriteria", filterCriteria) :
                new ObjectParameter("filterCriteria", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<FilterAuditUser_Result>("FilterAuditUser", filterCriteriaParameter);
        }
    
        public virtual ObjectResult<FilterCashCustomers_Result> FilterCashCustomers(string filterCriteria)
        {
            var filterCriteriaParameter = filterCriteria != null ?
                new ObjectParameter("filterCriteria", filterCriteria) :
                new ObjectParameter("filterCriteria", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<FilterCashCustomers_Result>("FilterCashCustomers", filterCriteriaParameter);
        }
    
        public virtual ObjectResult<FilterCashOrder_Result> FilterCashOrder(string filterCriteria)
        {
            var filterCriteriaParameter = filterCriteria != null ?
                new ObjectParameter("filterCriteria", filterCriteria) :
                new ObjectParameter("filterCriteria", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<FilterCashOrder_Result>("FilterCashOrder", filterCriteriaParameter);
        }
    
        public virtual ObjectResult<FilterCustomers_Result> FilterCustomers(string filterCriteria)
        {
            var filterCriteriaParameter = filterCriteria != null ?
                new ObjectParameter("filterCriteria", filterCriteria) :
                new ObjectParameter("filterCriteria", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<FilterCustomers_Result>("FilterCustomers", filterCriteriaParameter);
        }
    
        public virtual ObjectResult<GetCashOrderReport_Result> GetCashOrderReport(Nullable<int> orderNumber, string bankAccountType)
        {
            var orderNumberParameter = orderNumber.HasValue ?
                new ObjectParameter("OrderNumber", orderNumber) :
                new ObjectParameter("OrderNumber", typeof(int));
    
            var bankAccountTypeParameter = bankAccountType != null ?
                new ObjectParameter("BankAccountType", bankAccountType) :
                new ObjectParameter("BankAccountType", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetCashOrderReport_Result>("GetCashOrderReport", orderNumberParameter, bankAccountTypeParameter);
        }
    }
}
