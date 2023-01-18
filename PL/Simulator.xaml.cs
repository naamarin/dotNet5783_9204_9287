using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Threading;
using System.ComponentModel;

namespace PL
{
    /// <summary>
    /// Interaction logic for Simulator.xaml
    /// </summary>
    public partial class Simulator : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get();
        public ObservableCollection<BO.OrderForList?> currentOrder
        {
            get { return (ObservableCollection<BO.OrderForList?>)GetValue(currentcurrentOrder); }
            set { SetValue(currentcurrentOrder, value); }
        }

        public static readonly DependencyProperty currentcurrentOrder =
            DependencyProperty.Register("currentOrder", typeof(ObservableCollection<BO.OrderForList?>), typeof(Window), new PropertyMetadata(null));
        public Simulator()
        {
            InitializeComponent();
            currentOrder = new ObservableCollection<BO.OrderForList?>(bl!.Order.OrderListForManager());
            //Thread.CurrentThread.Name = "Main Thread";
            //Thread t = new Thread(NameOfFunction);
        }

        private void btStartTracking_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btStopTracking_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btViewOrderTracking_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            var orderForList = (BO.OrderForList)button.DataContext;
            new OrderTrackingView(orderForList.ID).ShowDialog();
        }
    }
}
