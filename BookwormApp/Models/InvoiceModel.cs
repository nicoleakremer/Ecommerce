using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookwormApp.Models
{
    public class InvoiceModel
    {

        public SHIPPING shipping { get; set; }
        public BILLING billing { get; set; }
        public CREDIT_CARD card { get; set; }
        public List<BOOK> books { get; set; }
        public decimal TotalCost { get; set; }
        public List<decimal> RetailPrice { get; set; }
        public List<int> Quantity { get; set; }
        public int cartId { get; set; }


    }
}