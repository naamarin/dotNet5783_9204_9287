using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {   
   
        BlApi.IBl? bl = BlApi.Factory.Get();
        BO.Cart cart { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            cart = new BO.Cart() { Items = new List<BO.OrderItem?>(), TotalPrice = 0, CustomerAddress = "", CustomerEmail = "", CustomerName = "" };
        }

        private void btOrderTracking_Click(object sender, RoutedEventArgs e)
        {
            new OrderTrackingView().Show();
        }

        private void btManagerView_Click(object sender, RoutedEventArgs e)
        {
            new ManagerView().Show();
            cart = new BO.Cart() { Items = new List<BO.OrderItem?>(), TotalPrice = 0, CustomerAddress = "", CustomerEmail = "", CustomerName = "" };
        }

        private void btNewOrderDisplay_Click(object sender, RoutedEventArgs e)
        {
            new NewOrder(cart).Show();
        }
    }
}
