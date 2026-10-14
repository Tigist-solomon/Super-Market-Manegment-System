using Microsoft.Data.SqlClient;
using System;
using System.Text;
using System.Security.Cryptography;
using System.Windows;
using System.Windows.Shapes;


namespace Super_Market_Manegment_System
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private string HashPassword(string value)
        {
            using (SHA256 sha = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(value);
                byte[] hash = sha.ComputeHash(bytes);
                StringBuilder builder = new StringBuilder();
                foreach (byte b in hash)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            
          
            using (SqlConnection con = new SqlConnection(@" Data Source = (localdb)\MSSQLLocalDB; Initial Catalog = SupermarketDB; 
                                                             Integrated Security = True; "))
            {
                try
                {
                    con.Open();


                    string query = "SELECT role FROM Users WHERE username=@u AND (password=@pHash OR password=@pPlain)";


                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@u", txtusername.Text);
                    cmd.Parameters.AddWithValue("@pHash", HashPassword(txtpassword.Password));
                    cmd.Parameters.AddWithValue("@pPlain", txtpassword.Password);

                    var role = cmd.ExecuteScalar();

                    if (role != null)
                    {
                       

                        if (role.ToString() == "Admin")
                        {
                           

                            AdminWindow admin = new AdminWindow();
                            admin.Show();


                        }
                        else if (role.ToString() == "Cashier")
                        {
                           

                            MainWindow cashier = new MainWindow();
                            cashier.Show();
                           
                        }


                        else if (role.ToString() == "Customer")
                        {
                            CustomerWindow customer = new CustomerWindow();
                            customer.Show();
                        }
                         this.Close();


                    }
                    else
                    {
                        MessageBox.Show("Please Enter Valid Name and Password");
                    }
                }
                catch(Exception ex) {

                    MessageBox.Show(ex.Message);
                }
            }
        }

         

    }
}