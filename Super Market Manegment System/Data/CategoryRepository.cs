using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using Super_Market_Manegment_System.Models;
using Microsoft.Data.SqlClient;

namespace Super_Market_Manegment_System.Data
{
    public class CategoryRepository
    {
        public List<Category> GetCategories()
        {
            List<Category> categories = new List<Category>();

            using (SqlConnection con = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;" +
                                                                  "Initial Catalog=SupermarketDb;" +
                                                                   "Integrated Security=True;"))
            {
                con.Open();

                string query = "SELECT * FROM Category";

                SqlCommand cmd = new SqlCommand(query, con);

                SqlDataReader data = cmd.ExecuteReader();

                while (data.Read())
                {
                    categories.Add(new Category {
                        category_id = (int)data["category_id"],
                        categoryName = data["name"].ToString()
                    
                    
                    });
                }
                return categories;
            }
        }
    }
}
