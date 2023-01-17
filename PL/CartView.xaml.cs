using BlApi;
using BO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        //BO.Cart cart { get; set; }

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
            //cart = c;
        }

        //private void Window_Activated(object sender, EventArgs e)
        //{
        //    currentCart = cart;
        //}

        private void btMakeOrder_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int orderID = bl.Cart.MakeOrder(currentCart);
                
                MessageBox.Show("Your order ID is: " + orderID, "succuss", MessageBoxButton.OK, MessageBoxImage.Information);
                customerAddressTextBox.Text = "";
                customerEmailTextBox.Text = "";
                customerNameTextBox.Text = "";
                currentCart.Items = null;
                this.Close();
            }
            catch(BO.BlClientDeatalesNotValid ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
           
        }

        private void btRemove_Click(object sender, RoutedEventArgs e)
        {

            var button = (Button)sender;
            var orderItem = (BO.OrderItem)button.DataContext;
            orderItemListView.ItemsSource = bl.Cart.RemoveOrderItem(currentCart!, orderItem.ProductID);
            totalPriceTextBox.Text = currentCart!.TotalPrice.ToString();
            MessageBox.Show("Order item romoved" , "succuss", MessageBoxButton.OK, MessageBoxImage.Information);
            //currentCart.Items = cart.Items;
            //orderItemListView.Items.Refresh();
            // currentCart = cart;
            //currentCart = new Cart(currentCart);
        }


        private void txAmount_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            var orderItem = (BO.OrderItem)textBox.DataContext;
            bl.Cart.UpdateCart(currentCart!, orderItem.ProductID, int.Parse(textBox.Text));
        }
    }
}
