using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Super_Market_Manegment_System.Controller;
using Super_Market_Manegment_System.Data;
using Super_Market_Manegment_System.Models;

namespace Super_Market_Manegment_System
{
    /// <summary>
    /// Interaction logic for AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        private CategoryRepository categoryRepo = new CategoryRepository();
        private ProductController controller = new ProductController();
        public AdminWindow()
        {
            InitializeComponent();
            try
            {
                ProductGrid.ItemsSource = controller.LoadProducts();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            LoadProducts();

            LoadCategories();
        }
        private void LoadProducts()
        {
             ProductGrid.ItemsSource = controller.LoadProducts();
        }

        private void LoadCategories()
        {
            cmbCategory.ItemsSource=categoryRepo.GetCategories();

            cmbCategory.DisplayMemberPath = "categoryName";

            cmbCategory.SelectedValuePath = "category_id";
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Product p = new Product
                {
                   name = txtName.Text,
                   category_id =(int)cmbCategory.SelectedValue,
                    price = Convert.ToDecimal(txtPrice.Text),
                    quantity = Convert.ToInt32(txtQuantity.Text)

                };
                controller.AddProduct(p);
                MessageBox.Show("Product Added");
                LoadProducts();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try {
                
                if (ProductGrid.SelectedItem is Product )
                {
                   
                    Product p = ProductGrid.SelectedItem as Product;
                      
                    p.name = txtName.Text;
                    p.price= Convert.ToDecimal(txtPrice.Text);
                    p.quantity= Convert.ToInt32(txtQuantity.Text);
                    p.category_id =(int)cmbCategory.SelectedValue;
                  

                    controller.UpdateProduct(p);
                    MessageBox.Show("Product Updated");

                    LoadProducts();
                }
                else
                {
                    MessageBox.Show("Please Select Item");
                }
                
            }
            catch(Exception ee)
            {
                MessageBox.Show(ee.Message);
            }


        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                if (ProductGrid.SelectedItem is Product )
                {
                   
                    Product p = ProductGrid.SelectedItem as Product;
                    controller.DeleteProduct(p.product_id);

                    MessageBox.Show("Deleted");
                    LoadProducts();
                }
                else
                {
                    MessageBox.Show("Please Select Item");
                }
            }
            catch(Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }

        private void onChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ProductGrid.SelectedItem != null) 
            {
                Product p = (Product)ProductGrid.SelectedItem;
                txtName.Text= p.name;
                txtPrice.Text = p.price.ToString();
                txtQuantity.Text = p.quantity.ToString();
                cmbCategory.SelectedValue = p.category_id;


            
            }


            /*
            if (ProductGrid.SelectedItem is Product)
            {
                Product q= ProductGrid.SelectedItem as Product;
                txtName.Text = q.name;
               cmbCategory.Text = q.category_id.ToString();
                txtQuantity.Text=q.quantity.ToString();
                txtPrice.Text=q.price.ToString();


            }*/
        }
    }
}
