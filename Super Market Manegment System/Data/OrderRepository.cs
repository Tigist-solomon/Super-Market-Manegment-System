using Microsoft.Data.SqlClient;
using Super_Market_Manegment_System.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows.Controls.Primitives;

namespace Super_Market_Manegment_System.Data
{
    public class OrderRepository
    {
        public int SaveOrder(Order o)
        {
            int orderId = 1;

            using (SqlConnection con = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;" +
                                                              "Initial Catalog=SupermarketDb;" +
                                                               "Integrated Security=True;"))
            {
                con.Open();

                string query = "INSERT INTO Orders " +
                               "(date,total_amount,status) " +
                               " OUTPUT INSERTED.order_id VALUES (@date,@total_amount,'pending')";
                SqlCommand cmd = new SqlCommand(query, con);

                cmd.Parameters.AddWithValue("@date", o.OrderDate);
                cmd.Parameters.AddWithValue("@total_amount", o.TotalAmount);

               orderId=(int) cmd.ExecuteScalar();

            }

            return orderId;

        }


        public List<Order> GetOrders()
        {
            List<Order> orders = new List<Order>();

            using (SqlConnection con = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;" +
                                                             "Initial Catalog=SupermarketDb;" +
                                                              "Integrated Security=True;"))
            {
                con.Open();

                string query = "SELECT * FROM Orders WHERE status = 'Pending'";

                SqlCommand cmd = new SqlCommand(query, con);

                SqlDataReader data = cmd.ExecuteReader();

                while (data.Read())
                {

                    orders.Add(new Order
                    {
                       
                        OrderID = Convert.ToInt32(data["order_id"]),
                        OrderDate = Convert.ToDateTime(data["date"]),
                        TotalAmount = Convert.ToDecimal(data["total_amount"])
                        


                    });

                }
            }
            return orders;
        }

        public void SaveOrderDetails(int orderId, CartItem item)
        {
            using (SqlConnection con = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=SupermarketDB;Integrated Security=True;"))
            {
                con.Open();

                string query = "INSERT INTO OrderDetails (order_id,product_id,product_name,price,quantity, subtotal)" +
                             " values (@oid,@pid,@name,@pr,@quan,@sub)   ";

                SqlCommand cmd = new SqlCommand(query, con);

                cmd.Parameters.AddWithValue("@oid", orderId);
                cmd.Parameters.AddWithValue("@pid", item.ProductId);
                cmd.Parameters.AddWithValue("@quan", item.Quantity);
                cmd.Parameters.AddWithValue("@sub", item.Total);
                cmd.Parameters.AddWithValue("@pr", item.Price);
                cmd.Parameters.AddWithValue("@name", item.ProductName);

                cmd.ExecuteNonQuery();
            }
        }

        public List<OrderDetail> GetOrderDetails()
        {
            List<OrderDetail> list = new List<OrderDetail>();

            using (SqlConnection con = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=SupermarketDB;Integrated Security=True;"))
            {
                con.Open();

                
                string query = "SELECT * FROM OrderDetails WHERE order_id IN " +
                               "(SELECT order_id FROM Orders WHERE status = 'pending')";

                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader data = cmd.ExecuteReader();

                while (data.Read())
                {
                    list.Add(new OrderDetail
                    {
                        OrderDetailID = Convert.ToInt32(data["order_detail_id"]),
                        ProductID = Convert.ToInt32(data["product_id"]),
                        Price = data["price"] == DBNull.Value ? 0 : Convert.ToDecimal(data["price"]),
                        OrderID = Convert.ToInt32(data["order_id"]),
                        ProductName = data["product_name"].ToString(),
                        Quantity = Convert.ToInt32(data["quantity"])

                    });
                }
            }

            return list;
        }

        public void UpdateOrderStatus(int orderId)
        {
            using (SqlConnection con = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=SupermarketDB;Integrated Security=True;"))
            {
                con.Open();

                string query = "UPDATE Orders SET status = 'completed' WHERE order_id = @id";
                SqlCommand cmd = new SqlCommand(query, con);

                cmd.Parameters.AddWithValue("@id", orderId);
                cmd.ExecuteNonQuery();
            }
        }


       

    }
}