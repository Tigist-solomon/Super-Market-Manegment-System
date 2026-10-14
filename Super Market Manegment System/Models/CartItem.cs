using System;
using System.Collections.Generic;
using System.Text;

namespace Super_Market_Manegment_System.Models
{
public class CartItem
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } 
        public int Quantity { get; set; }
        public decimal Price { get; set; }  
        public decimal Total
        {
            get { return Quantity * Price; }
        }
    }
}
