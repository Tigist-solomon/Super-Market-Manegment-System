using System;
using System.Collections.Generic;
using System.Text;

namespace Super_Market_Manegment_System.Models
{
    public class Order
    {
        public int OrderID { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
     
    }
}
