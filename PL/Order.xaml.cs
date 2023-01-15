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
    /// Interaction logic for Order.xaml
    /// </summary>
    public partial class Order : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get();
        public IEnumerable<BO.OrderForList?> currentOrder
        {
            get { return (IEnumerable<BO.OrderForList?>)GetValue(currentcurrentOrder); }
            set { SetValue(currentcurrentOrder, value); }
        }
        public static readonly DependencyProperty currentcurrentOrder =
            DependencyProperty.Register("currentOrder", typeof(IEnumerable<BO.OrderForList?>), typeof(Window), new PropertyMetadata(null));
        public Order()
        {
            InitializeComponent();
            currentOrder = bl.Order.OrderListForManager();
            lvOrderForList.ItemsSource = currentOrder;
        }

        private void lvOrderForList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            int orderID = ((BO.OrderForList)lvOrderForList.SelectedItem).ID; 
            new OrderDetals(orderID, true).ShowDialog();
            //ProductView.ItemsSource = bl?.Product.GetListProducts();
            //CategorySelector.SelectedItem = BO.CategoryForWPF.All;
        }
    }
}
