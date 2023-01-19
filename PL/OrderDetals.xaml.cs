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
        bool flag;
        int id;
        public BO.Order? currentOrderr
        {
            get { return (BO.Order?)GetValue(currentOrderProperty); }
            set { SetValue(currentOrderProperty, value); }
        }
        public static readonly DependencyProperty currentOrderProperty =
            DependencyProperty.Register("currentOrderr", typeof(BO.Order), typeof(Window), new PropertyMetadata(null));

        public OrderDetals(int orderID, bool f)
        {
            InitializeComponent();
            flag = f;
            id = orderID;
            cbxStatus.ItemsSource = Enum.GetValues(typeof(BO.OrderStatus));
            currentOrderr = bl.Order.GetById(orderID);
            txbDeliveryDate.Text = currentOrderr.DeliveryDate.ToString();
            txbOrderDate.Text = currentOrderr.OrderDate.ToString(); 
            txbShipDate.Text = currentOrderr.ShipDate.ToString();   
            txbPaymentDate.Text = currentOrderr.PaymentDate.ToString();
            cbxStatus.SelectedItem = currentOrderr.Status;
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
                txbCustomerAddress.IsReadOnly = false;
                txbCustomerName.IsReadOnly = false;
                txbCustomerEmail.IsReadOnly = false;
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
            int id = int.Parse(currentOrderr!.ID.ToString());
            BO.Order order = bl.Order.GetById(id);
            if (cbShipDate.IsChecked == true)
            {
                bl?.Order.OrderShipUpdate(id);
                order.Status = BO.OrderStatus.Shipped;
            }
            if (cbDeliveryDate.IsChecked == true)
            {
                bl?.Order.OrderDeliveryUpdate(id);
                order.Status = BO.OrderStatus.Delivered;
            }
            if (flag == true)
            {
                order = bl.Order.GetById(id);
                order.CustomerName = txbCustomerName.Text;
                order.CustomerEmail = txbCustomerEmail.Text;
                order.CustomerAddress = txbCustomerAddress.Text;
                bl!.Order.UpdateDeatails(order);
                
            }
            this.Close();
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            currentOrderr = bl.Order.GetById(id);
        }
    }
}
