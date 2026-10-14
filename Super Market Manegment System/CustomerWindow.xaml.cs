using Microsoft.EntityFrameworkCore.Storage;
using Super_Market_Manegment_System.Controller;
using Super_Market_Manegment_System.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Super_Market_Manegment_System
{
    /// <summary>
    /// Interaction logic for CustomerWindow.xaml
    /// </summary>
    public partial class CustomerWindow : Window
    {
        private ProductController controller = new ProductController();
        private List<CartItem> cart = new List<CartItem>();
        public CustomerWindow()
        {
            InitializeComponent();

            LoadProduct();
        }

        public void LoadProduct()
        {
            ProductGrid.ItemsSource = controller.LoadProducts();
        }

        private void btnAddToCart_Click(object sender, RoutedEventArgs e)
        {
            if(ProductGrid.SelectedItem == null)
            {
                MessageBox.Show("Select product first");
                return;
            }

            Product p = (Product)ProductGrid.SelectedItem;
            bool found = false;

            foreach (CartItem item in cart)
            {
                if (item.ProductId == p.product_id)
                {
                    item.Quantity++;
                    found = true;
                    break;
                }
            }

            if (!found)
            {
                CartItem item = new CartItem();
                item.ProductId = p.product_id;
                item.ProductName = p.name;
                item.Price = p.price;
                item.Quantity = 1;
                cart.Add(item);
            }

            RefreshCartUI();
            CalculateTotal();
        }

        private void btnOrder_Click(object sender, RoutedEventArgs e)
        {
            if (cart.Count == 0)
            {
                MessageBox.Show("Cart is Empty");
                return;
            }

            decimal total = 0;

            foreach(CartItem item in cart)
            {
                total += item.Total;

            }

            Order o = new Order();
            o.OrderDate = DateTime.Now;
            o.TotalAmount = total;
           
            int orderId = new OrderController().SaveOrder(o);

            foreach (CartItem item in cart)
            {
                new OrderController().SaveOrderDetails(orderId, item);
            }

            MessageBox.Show("Order Sent to Cashier");


            cart.Clear();
            CartList.Items.Clear();
            txtTotal.Clear();

        }

        public void CalculateTotal()
        {
            decimal total = 0;

            foreach(var item in cart)
            {
                total += item.Total;
            }

            txtTotal.Text = total + "Birr";

                
        }

        private void RefreshCartUI()
        {
            CartList.Items.Clear();

            foreach (CartItem item in cart)
            {
                CartList.Items.Add(item.ProductName + " x" + item.Quantity + " = " + item.Total + " Birr");
            }
        }
    }
}
