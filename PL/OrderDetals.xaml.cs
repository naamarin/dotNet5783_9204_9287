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
        public OrderDetals(int orderID, bool flag)
        {
            InitializeComponent();
            cbxStatus.ItemsSource = Enum.GetValues(typeof(BO.OrderStatus));
            BO.Order order = bl.Order.GetById(orderID);
            txbID.Text = orderID.ToString();
            txbCustomerName.Text = order.CustomerName;
            txbCustomerEmail.Text = order.CustomerEmail;
            txbCustomerAddress.Text = order.CustomerAddress;
            txbDeliveryDate.Text = order.DeliveryDate.ToString();
            txbToatalPrice.Text = order.TotalPrice.ToString();
            txbOrderDate.Text = order.OrderDate.ToString();
            txbPaymentDate.Text = order.PaymentDate.ToString();
            txbShipDate.Text = order.ShipDate.ToString();
            lvOrderItem.ItemsSource = order.Items;
            cbxStatus.SelectedItem = order.Status;
            if (flag == true)
            {
                
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
                if (txbOrderDate.Text == "")
                {
                    txbOrderDate.Visibility = Visibility.Hidden;
                }
                else
                    txbOrderDate.IsReadOnly = true;
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
            }
            else
            {
                btUpdate.Visibility = Visibility.Hidden;
                cbDeliveryDate.Visibility= Visibility.Hidden;
                cbShipDate.Visibility= Visibility.Hidden;
            }
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
