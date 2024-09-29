using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using _CRUD_interface_Sales;
using data_acess;

namespace CRUD_interface_sales_wpf
{

    public partial class MainWindow : Window
    {
        static string connectionString = @"Data Source=DESKTOP-A1447MR\SQLEXPRESS;
													  Initial Catalog=Sales;
													  Integrated Security=True;
													  TrustServerCertificate=True";
        Sales shop = null; 
            public MainWindow()
            {
                InitializeComponent();
                shop = new Sales();
            }

        private void ShowCustomers_Click(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource = shop.GetAllCustomers();

        }
        private void ShowSellers_Click(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource = shop.GetAllSallers();
        }

        private void ShowSales_Click(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource = shop.GetAllSales();
        }

           
        }
    }