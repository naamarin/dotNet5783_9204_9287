using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Printing;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PL
{
    /// <summary>
    /// Interaction logic for CartView.xaml
    /// </summary>
    public partial class CartView : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get();

        BO.Cart cart { get; set; }

        public BO.Cart? currentCart
        {
            get { return (BO.Cart?)GetValue(currentCartProperty); }
            set { SetValue(currentCartProperty, value); }
        }
        public static readonly DependencyProperty currentCartProperty =
            DependencyProperty.Register("currentCart", typeof(BO.Cart), typeof(Window), new PropertyMetadata(null));


        public CartView(BO.Cart c)
        {
            InitializeComponent();
            currentCart = c;
            cart = c;
        }

        private void btMakeOrder_Click(object sender, RoutedEventArgs e)
        {
            int orderID = bl.Cart.MakeOrder(cart);
            MessageBox.Show("Your order ID is: " + orderID, "succuss");
            //cart = new BO.Cart() { Items = new List<BO.OrderItem?>(), TotalPrice = 0, CustomerAddress = "", CustomerEmail = "", CustomerName = "" };
            this.Close();
        }

        private void btRemove_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
