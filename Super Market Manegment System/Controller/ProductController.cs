using Super_Market_Manegment_System.Data;
using Super_Market_Manegment_System.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Super_Market_Manegment_System.Controller
{
  public class ProductController
    {
        private ProductRepository repo= new ProductRepository();
        public List<Product> LoadProducts()
        {
            return repo.GetProducts();
        }
        public void AddProduct(Product p)
        {
            repo.AddProduct(p);
        }

        public void UpdateProduct(Product p)
        { 
            repo.UpdateProduct(p);
        }
        public void DeleteProduct(int id) 
        {
           repo.DeleteProduct(id); 
        }

        public void ReduceQuantity(int id)
        {
            repo.ReduceQuantity(id);
        }

    }
}
