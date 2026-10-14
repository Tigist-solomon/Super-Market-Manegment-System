using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
namespace Super_Market_Manegment_System.Models
{
 public class Product
    
        {
            public int product_id { get; set; }
            public string name { get; set; }
            public int category_id { get; set; }
            public decimal price { get; set; }
            public int quantity { get; set; }
        

    }
}
