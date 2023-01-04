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
                txbDeliveryDate.Text = "--/--/----";
            txbToatalPrice.Text = order.TotalPrice.ToString();
            txbOrderDate.Text = order.OrderDate.ToString();
            if (txbOrderDate.Text == "")
                txbOrderDate.Text = "--/--/----";
            txbPaymentDate.Text = order.PaymentDate.ToString();
            if (txbPaymentDate.Text == "")
                txbPaymentDate.Text = "--/--/----";
            txbShipDate.Text = order.ShipDate.ToString();
            if (txbShipDate.Text == "")
                txbShipDate.Text = "--/--/----";
            cbxStatus.SelectedItem = order.Status;
            lvOrderItem.ItemsSource = order.Items;
        }

    }
}
