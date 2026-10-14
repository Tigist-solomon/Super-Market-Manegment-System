using Microsoft.Data.SqlClient;
using Super_Market_Manegment_System.Models;
using System.Collections.Generic;

namespace Super_Market_Manegment_System.Data
{
   public class ProductRepository
    {
        //for unknown size
        public List<Product> GetProducts()
        { 
            List<Product> products = new List<Product>();

             using (SqlConnection con = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;" +
                                                               "Initial Catalog=SupermarketDb;" +
                                                                "Integrated Security=True;" 
                                                                ))
            {
                con.Open();
                string query = "SELECT * FROM Products";
                SqlCommand cmd = new SqlCommand(query, con);

                SqlDataReader data= cmd.ExecuteReader();
                while(data.Read())
                    {
                    products.Add(new Product
                    {
                        product_id = (int)data["product_id"],
                        name = data["name"].ToString(),
                        category_id = (int)data["category_id"],
                        price = (decimal)data["price"],
                        quantity = (int)data["quantity"]

                    }); 
                     
                }
          
            }
             return products;
        }

        public void AddProduct(Product p)
        {
            using (SqlConnection con = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;" +
                                                               "Initial Catalog=SupermarketDb;" +
                                                                "Integrated Security=True;"))
            { 
                con.Open();
                string query = "INSERT INTO Products(name,category_id,price,quantity)" +
                               "VALUES (@name,@category_id,@price,@quantity)";
                SqlCommand cmd = new SqlCommand(query, con);

                cmd.Parameters.AddWithValue("@name", p.name);
                cmd.Parameters.AddWithValue("@category_id", p.category_id);
                cmd.Parameters.AddWithValue("@price", p.price);
                cmd.Parameters.AddWithValue("@quantity", p.quantity);

                cmd.ExecuteNonQuery();

            }

        }

        public void UpdateProduct(Product p)
        {
            using (SqlConnection con = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;" +
                                                               "Initial Catalog=SupermarketDb;" +
                                                                "Integrated Security=True;"))
            {
                con.Open();

               string query = "UPDATE Products SET " +
                              "name=@name, " +
                              "category_id=@category_id, " +
                              "price=@price, " +
                              "quantity=@quantity " +
                              "WHERE product_id=@product_id";

                SqlCommand cmd = new SqlCommand(query, con);

                cmd.Parameters.AddWithValue("@product_id", p.product_id);
                cmd.Parameters.AddWithValue("@name", p.name);
                cmd.Parameters.AddWithValue("@category_id", p.category_id);
                cmd.Parameters.AddWithValue("@price", p.price);
                cmd.Parameters.AddWithValue("@quantity", p.quantity);

                cmd.ExecuteNonQuery();

            }
        }

        public void DeleteProduct(int id)
        {
            using (SqlConnection con = new SqlConnection(
                "Data Source=(localdb)\\MSSQLLocalDB;" +
                "Initial Catalog=SupermarketDb;" +
                "Integrated Security=True;"))
            {
                con.Open();

           string query1 = "DELETE FROM OrderDetails WHERE product_id=@id";

                SqlCommand cmd1 = new SqlCommand(query1, con);
                cmd1.Parameters.AddWithValue("@id", id);
                cmd1.ExecuteNonQuery();

                
           string query2 = "DELETE FROM Products WHERE product_id=@id";

                SqlCommand cmd2 = new SqlCommand(query2, con);
                cmd2.Parameters.AddWithValue("@id", id);
                cmd2.ExecuteNonQuery();
            }
        }
        public void ReduceQuantity(int productId)
        {
            using (SqlConnection con = new SqlConnection("Data Source = (localdb)\\MSSQLLocalDB; " +
                                                            "Initial Catalog=SupermarketDb;" +
                                                             "Integrated Security=True;"))
            {
                con.Open();

                string query =  "UPDATE Products " +
                                 "SET quantity = quantity - 1 " +
                                 "WHERE product_id=@id";

                SqlCommand cmd = new SqlCommand(query, con);

                cmd.Parameters.AddWithValue("@id", productId);

                cmd.ExecuteNonQuery();
            }
        }
    }
}
