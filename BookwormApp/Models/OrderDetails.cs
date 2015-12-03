using System;
using System.ComponentModel.DataAnnotations;

namespace WingtipToys.Models
{
    public class OrderDetail
    {
        public int InvoicelId { get; set; }

        public int CustomerId { get; set; }

        public int CartId { get; set; }

        public int BillingId { get; set; }

        public int ShippingId { get; set; }

        public DateTime DateOrder { get; set; }

        public Double TotalCost { get; set; }

    }
}