
using System.Text;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Super_Market_Manegment_System.Models;
using Super_Market_Manegment_System.Database;

using Microsoft.Data.SqlClient;
using Super_Market_Manegment_System.Controller;


namespace Super_Market_Manegment_System
{

    public partial class MainWindow : Window
    {
        private OrderController orderController = new OrderController();
        private ProductController controller = new ProductController();
        private int currentOrderId = 0;
        

        private List<CartItem> cart = new List<CartItem>();
        public MainWindow()
        {

            InitializeComponent();
            try
            {
                LoadOrders();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            LoadProducts();


        }
      
        private void btnAddToCart_Click(object sender, RoutedEventArgs e)
        {
            if (ProductGrid.SelectedItem != null)
            {
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
                    CartItem newitem = new CartItem();

                    newitem.ProductId = p.product_id;
                    newitem.ProductName = p.name;
                    newitem.Quantity = 1;
                    newitem.Price = p.price;

                    cart.Add(newitem);

                }

                RefreshCartUI();
                CalculateTotal();
            }
            else
            {
                MessageBox.Show("Please select a product");
            }


        }

        private void btnPay_Click(object sender, RoutedEventArgs e)
        {


            try
            {
                decimal total = 0;

                foreach (var item in cart)
                {
                    total += item.Price * item.Quantity;

                }


                if (currentOrderId > 0)
                {
                    foreach (var item in cart)
                    {
                        for (int i = 0; i < item.Quantity; i++)
                        {
                            controller.ReduceQuantity(item.ProductId);
                        }
                    }

                    orderController.UpdateOrderStatus(currentOrderId);
                }
                else
                {
                    //new
                    Order o = new Order
                    {
                        OrderDate = DateTime.Now,
                        TotalAmount = total
                    };

                    int orderId = orderController.SaveOrder(o);

                    foreach (var item in cart)
                    {
                        orderController.SaveOrderDetails(orderId, item);

                        for (int i = 0; i < item.Quantity; i++)
                        {
                            controller.ReduceQuantity(item.ProductId);
                        }
                    }

                   
                        orderController.UpdateOrderStatus(currentOrderId);
                    
                }

                MessageBox.Show("Payment Successful");

                cart.Clear();

                CartList.Items.Clear();

                txtTotal.Clear();

                LoadProducts();
                LoadOrders();
                currentOrderId = 0;

            }



            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    
        
        public void LoadProducts()
        {
            ProductGrid.ItemsSource = controller.LoadProducts();
        }

        public void CalculateTotal()
        {
            decimal total = 0;
            foreach (var item in cart)
            {
                total += item.Total;
            }

            txtTotal.Text = total + "Birr";
        }


        public void LoadOrders()
        {

            try
            {
                OrderGrid.ItemsSource = orderController.GetOrderDetails();
             
            }
            catch (Exception ex)
            {
                MessageBox.Show( ex.Message);
            }
        }

        void RefreshCartUI()
        {
            CartList.Items.Clear ();

            foreach(var item in cart)
            {
                CartList.Items.Add(item.ProductName + " x" + item.Quantity + " = " + item.Total + " Birr");

            }
        }

        private void btnAddFromOrder_Click(object sender, RoutedEventArgs e)
        {
            if (OrderGrid.SelectedItem == null)
            {
                MessageBox.Show("Select order item");
                return;
            }

            OrderDetail selected = (OrderDetail)OrderGrid.SelectedItem;

            currentOrderId = selected.OrderID;


            bool found = false;

            foreach (CartItem item in cart)
            {
                if (item.ProductId == selected.ProductID)
                {
                    item.Quantity += selected.Quantity; 
                    found = true;
                    break;
                }
            }

            if (!found)
            {
                cart.Add(new CartItem
                {
                    ProductId = selected.ProductID,   
                    ProductName = selected.ProductName,
                    Quantity = selected.Quantity,
                    Price = selected.Price
                });
            }
            RefreshCartUI();
            CalculateTotal();
        }

       
    }
    } 
