﻿using FreeMarket.Infrastructure;
using System;
using System.ComponentModel;
using System.Linq;

namespace FreeMarket.Models
{
    public class ConfirmInvoiceViewModel
    {
        public ShoppingCart Cart { get; set; }
        public Support Support { get; set; }
        public DateTime MinDispatchDate { get; set; }

        [EnforceTrue(ErrorMessage = "You must accept the terms and conditions before you can place your order.")]
        [DisplayName("Terms and Conditions")]
        public bool TermsAndConditions { get; set; }

        public Courier Courier { get; set; }

        public ConfirmInvoiceViewModel()
        {
            Cart = new ShoppingCart();
        }

        public ConfirmInvoiceViewModel(ShoppingCart cart)
        {
            Cart = cart;
            MinDispatchDate = OrderHeader.GetDispatchDay(OrderHeader.GetSuggestedDeliveryTime());

            using (FreeMarketEntities db = new FreeMarketEntities())
            {
                Courier = db.Couriers.Find(cart.Order.CourierNumber);
                Support = db.Supports.FirstOrDefault();
            }
        }
    }
}