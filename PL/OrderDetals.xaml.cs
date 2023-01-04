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
using System.Windows.Shapes;

namespace PL
{
    /// <summary>
    /// Interaction logic for OrderDetals.xaml
    /// </summary>
    public partial class OrderDetals : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get();
        public OrderDetals(int orderID)
        {
            InitializeComponent();
            cbxStatus.ItemsSource = Enum.GetValues(typeof(BO.OrderStatus));
            BO.Order order = bl.Order.GetById(orderID);
            txbID.Text = orderID.ToString();
            txbCustomerName.Text = order.CustomerName;
            txbCustomerEmail.Text = order.CustomerEmail;
            txbCustomerAddress.Text = order.CustomerAddress;
            txbDeliveryDate.Text = order.DeliveryDate.ToString();
            if (txbDeliveryDate.Text == "")
            {
                txbDeliveryDate.Visibility = Visibility.Hidden;
                cbDeliveryDate.Visibility = Visibility.Visible;
            }
            else
            {
                txbDeliveryDate.IsReadOnly = true;
                cbDeliveryDate.Visibility = Visibility.Hidden;
            }
            txbToatalPrice.Text = order.TotalPrice.ToString();
            txbOrderDate.Text = order.OrderDate.ToString();
            if (txbOrderDate.Text == "")
            {
                txbOrderDate.Visibility = Visibility.Hidden;
                //cb isibility = Visibility.Visible;
            }
            else
                txbOrderDate.IsReadOnly = true;
            txbPaymentDate.Text = order.PaymentDate.ToString();
            //if (txbPaymentDate.Text == "")
            //{
            //    txbPaymentDate.Visibility = Visibility.Hidden;
            //    cbPaymantDate.Visibility = Visibility.Visible;
            //}
            //else
            //{
            //    txbPaymentDate.IsReadOnly = true;
            //    cbPaymantDate.Visibility = Visibility.Hidden;
            // }
            txbShipDate.Text = order.ShipDate.ToString();
            if (txbShipDate.Text == "")
            {
                txbShipDate.Visibility = Visibility.Hidden;
                cbShipDate.Visibility = Visibility.Visible;
            }
            else
            {
                txbShipDate.IsReadOnly = true;
                cbShipDate.Visibility = Visibility.Hidden;
            }
            cbxStatus.SelectedItem = order.Status;
            lvOrderItem.ItemsSource = order.Items;
            txbID.IsReadOnly = true;
            txbID.Foreground = Brushes.Gray;
            txbCustomerName.IsReadOnly = true;
            txbCustomerName.Foreground = Brushes.Gray;
            txbCustomerEmail.IsReadOnly = true;
            txbCustomerEmail.Foreground = Brushes.Gray;
            txbCustomerAddress.IsReadOnly = true;
            txbCustomerAddress.Foreground = Brushes.Gray;
            txbToatalPrice.IsReadOnly = true;
            txbToatalPrice.Foreground = Brushes.Gray;
        }

        private void btUpdate_Click(object sender, RoutedEventArgs e)
        {
            int id = int.Parse(txbID.Text);
            if (cbShipDate.IsChecked == true)
                bl?.Order.OrderShipUpdate(id);
            if (cbDeliveryDate.IsChecked == true)
                bl?.Order.OrderDeliveryUpdate(id);
            this.Close();
        }
    }
}
