using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FreeMarket.Models
{
    [MetadataType(typeof(CashCustomerMetaData))]
    public partial class CashCustomer
    {
        public static CashCustomer GetCustomer(int id)
        {
            CashCustomer customer = new CashCustomer();

            using (FreeMarketEntities db = new FreeMarketEntities())
            {
                customer = db.CashCustomers.Find(id);
            }

            return customer;
        }

        public static void SaveCustomer(CashCustomer model)
        {
            using (FreeMarketEntities db = new FreeMarketEntities())
            {
                if (string.IsNullOrEmpty(model.HeadOfficeAddress))
                    model.HeadOfficeAddress = "Head Office";

                db.Entry(model).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
        }
    }

    public class CashCustomerMetaData
    {
        [Required]
        public string Name { get; set; }

        [DisplayName("Delivery Address")]
        [StringLength(1100)]
        public string DeliveryAddress { get; set; }

        [DisplayName("Head Office Address")]
        [StringLength(1100)]
        public string HeadOfficeAddress { get; set; }

        public string Email { get; set; }

        [StringLength(256)]
        public string PhoneNumber { get; set; }

        [DisplayName("Client Vat Number")]
        public string ClientVatNumber { get; set; }

        [DisplayName("Contact Name")]
        public string ContactName { get; set; }

        public string Type { get; set; }
    }
}