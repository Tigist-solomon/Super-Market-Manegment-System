using System;
using System.Collections.Generic;
using System.Text;

namespace Super_Market_Manegment_System.Models
{
  public class OrderDetail
    {
        public int OrderDetailID { get; set; }
        public int OrderID { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string ProductName { get; set; }

        
    }
}
