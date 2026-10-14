using System;
using System.Collections.Generic;
using System.Text;
using Super_Market_Manegment_System.Data;
using Super_Market_Manegment_System.Models;

namespace Super_Market_Manegment_System.Controller
{
public class OrderController
    {
        private OrderRepository repo = new OrderRepository();
        
        public int SaveOrder(Order o)
        {
           return repo.SaveOrder(o);
        }


       public List<OrderDetail> GetOrderDetails()
        {
           // repo.GetOrderDetails();
            return repo.GetOrderDetails();
        }
      
        public List<Order> GetOrders()
        {
            return repo.GetOrders();
        }

        
      public void SaveOrderDetails(int orderId, CartItem item)
        {
            repo.SaveOrderDetails(orderId,item);
        }


        public void UpdateOrderStatus(int orderId)
        {
            repo.UpdateOrderStatus(orderId);
        }

       
    }
}
