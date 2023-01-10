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
using System.Xml;

namespace PL
{
    /// <summary>
    /// Interaction logic for NewOrder.xaml
    /// </summary>
    public partial class NewOrder : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get();
        BO.Cart cart = new BO.Cart();

        public NewOrder()
        {
            InitializeComponent();
            //var v = bl.Product.GetListProducts(); 
            ProductView.ItemsSource = bl.Product.Catalog();
            CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.CategoryForWPF));
            CategorySelector.SelectedItem = BO.CategoryForWPF.All;
            //BO.Cart cart;
        }

        private void CategorySelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BO.CategoryForWPF category = (BO.CategoryForWPF)CategorySelector.SelectedItem;
            if (category == BO.CategoryForWPF.All)
                ProductView.ItemsSource = bl?.Product.Catalog();
            else
                ProductView.ItemsSource = bl?.Product.GetProductItemsByCategory((BO.Category)category);
        }

        private void ProductView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            int productID = ((BO.ProductItem)ProductView.SelectedItem).ID;
            new ProductItemView(productID, cart).ShowDialog();
            ProductView.ItemsSource = bl?.Product.Catalog();
            CategorySelector.SelectedItem = BO.CategoryForWPF.All;

        }
    }
}
