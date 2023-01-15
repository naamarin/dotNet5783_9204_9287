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
    /// Interaction logic for OrderTrackingView.xaml
    /// </summary>
    public partial class OrderTrackingView : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get();
        //BO.Cart cart1 = new BO.Cart();
        public BO.OrderTracking? currentOrderTracking
        {
            get { return (BO.OrderTracking?)GetValue(currentOrderTrackingProperty); }
            set { SetValue(currentOrderTrackingProperty, value); }
        }
        public static readonly DependencyProperty currentOrderTrackingProperty =
            DependencyProperty.Register("currentOrderTracking", typeof(BO.OrderTracking), typeof(Window), new PropertyMetadata(null));

        public OrderTrackingView()
        {
            InitializeComponent();
            btViewOrder.Visibility = Visibility.Hidden;
            
        }

        private void btOrderTracking_Click(object sender, RoutedEventArgs e)
        {
            int orderID;
            if (!int.TryParse(txbOrderID.Text, out orderID))
                MessageBox.Show("Enter numbers only!", "ERROR");
            else
            {
                try
                {
                    currentOrderTracking = bl.Order.TrackingOrder(orderID);
                    txbStatus.Text = currentOrderTracking.Status.ToString();
                    btViewOrder.Visibility = Visibility.Visible;
                }
                catch(BO.BlOrderDoesNotExsist ex)
                {
                    MessageBox.Show(ex.Message, "ERROR");
                }
                
            }
        }

        private void btViewOrder_Click(object sender, RoutedEventArgs e)
        {
            int orderID = int.Parse(txbOrderID.Text);
            new OrderDetals(orderID, false).ShowDialog();
        }
    }
}
