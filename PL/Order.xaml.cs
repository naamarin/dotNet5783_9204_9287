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
        public Order()
        {
            InitializeComponent();
            lvOrderForList.ItemsSource = bl.Order.OrderListForManager();
        }

        private void lvOrderForList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            int orderID = ((BO.OrderForList)lvOrderForList.SelectedItem).ID;
            new OrderDetals(orderID).ShowDialog();
            //ProductView.ItemsSource = bl?.Product.GetListProducts();
            //CategorySelector.SelectedItem = BO.CategoryForWPF.All;
        }
    }
}
