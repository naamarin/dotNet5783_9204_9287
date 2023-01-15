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
using System.Xml;

namespace PL
{
    /// <summary>
    /// Interaction logic for Window2.xaml
    /// </summary>
    public partial class Window2 : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get();
        public ObservableCollection<BO.ProductForList?> currentProductForList
        {
            get { return (ObservableCollection<BO.ProductForList?>)GetValue(currentProductForListProperty); }
            set { SetValue(currentProductForListProperty, value); }
        }
        public static readonly DependencyProperty currentProductForListProperty =
            DependencyProperty.Register("currentProductForList", typeof(ObservableCollection<BO.ProductForList?>), typeof(Window), new PropertyMetadata(null));

        public Window2()
        {
            InitializeComponent();
            //var v = bl.Product.GetListProducts(); 
            //currentProductForList = bl.Product.GetListProducts();
            CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.CategoryForWPF));
            CategorySelector.SelectedItem = BO.CategoryForWPF.All;
        }
        private void Window_Activated(object sender, EventArgs e)
        {
            currentProductForList = new ObservableCollection<BO.ProductForList?>(bl!.Product.GetListProducts());
        }

        private void CategorySelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BO.CategoryForWPF category = (BO.CategoryForWPF)CategorySelector.SelectedItem;
            if (category == BO.CategoryForWPF.All)
                ProductView.ItemsSource = bl?.Product.GetListProducts();
            else
                ProductView.ItemsSource = bl?.Product.GetListProductsByCategory((BO.Category)category);
        }

        private void btnAddProduct_Click(object sender, RoutedEventArgs e)
        {
            new Window1().ShowDialog();
            ProductView.ItemsSource = bl?.Product.GetListProducts();
            CategorySelector.SelectedItem = BO.CategoryForWPF.All;
        }

        private void ProductView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            int productID = ((BO.ProductForList)ProductView.SelectedItem).ID;
            new Window1(productID).ShowDialog();
            //ProductView.ItemsSource = bl?.Product.GetListProducts();
            CategorySelector.SelectedItem = BO.CategoryForWPF.All;

        }
    }
}
