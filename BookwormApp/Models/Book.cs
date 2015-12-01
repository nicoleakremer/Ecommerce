using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookwormApp.Models
{
    public class Book
    {
        public int BookId { get; set; }
        public int PublisherId { get; set; }
        public Nullable<int> InventoryId { get; set; }
        public string Title { get; set; }
        public Nullable<int> Edition { get; set; }
        public Nullable<int> Rating { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public Nullable<int> ISBN { get; set; }
        public Nullable<System.DateTime> CopyRightDate { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Biography { get; set; }
        public decimal RetailPrice { get; set; }
    
    }
}