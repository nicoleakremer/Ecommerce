//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BookwormApp
{
    using System;
    using System.Collections.Generic;
    
    public partial class BOOK_CART
    {
        public int BookId { get; set; }
        public int CartId { get; set; }
        public Nullable<int> Quantity { get; set; }
    
        public virtual CART CART { get; set; }
        public virtual BOOK BOOK { get; set; }
    }
}
