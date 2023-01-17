using BO;
using System;
using System.CodeDom.Compiler;
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
    /// Interaction logic for NewOrder.xaml
    /// </summary>
    public partial class NewOrder : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get();
        BO.Cart cart  { get; set; }
        public ObservableCollection<BO.ProductItem?> currentProductItems
        {
            get { return (ObservableCollection<BO.ProductItem?>)GetValue(currentProductItemsProperty); }
            set { SetValue(currentProductItemsProperty, value); }
        }

        public static readonly DependencyProperty currentProductItemsProperty =
            DependencyProperty.Register("currentProductItems", typeof(ObservableCollection<BO.ProductItem?>), typeof(Window), new PropertyMetadata(null));


        public NewOrder(BO.Cart c)
        {
            InitializeComponent();
            //var v = bl.Product.GetListProducts(); 
            currentProductItems = new ObservableCollection<BO.ProductItem?> (bl.Product.Catalog());
            CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.CategoryForWPF));
            CategorySelector.SelectedItem = BO.CategoryForWPF.All;
            cart = c;
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            currentProductItems = new ObservableCollection<BO.ProductItem?>(bl!.Product.Catalog());
        }


        private void CategorySelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BO.CategoryForWPF category = (BO.CategoryForWPF)CategorySelector.SelectedItem;
            if (category == BO.CategoryForWPF.All)
                currentProductItems = new ObservableCollection<BO.ProductItem?>(bl!.Product.Catalog());
            else
                currentProductItems = new ObservableCollection<BO.ProductItem?>(bl!.Product.GetProductItemsByCategory((BO.Category)category));
        }

        private void ProductView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            int productID = ((BO.ProductItem)ProductView.SelectedItem).ID;
            new ProductItemView(productID, cart).ShowDialog();
            currentProductItems = new ObservableCollection<BO.ProductItem?>(bl!.Product.Catalog());
            CategorySelector.SelectedItem = BO.CategoryForWPF.All;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new CartView(cart).ShowDialog();
        }

        private void cbGroupByCategory_Checked(object sender, RoutedEventArgs e)
        {
            var GroupingByCategory = from productItem in bl?.Product.Catalog()
                                     group productItem by productItem.Category into g
                                     select g;
            //List<BO.ProductItem?> productItems = new List<BO.ProductItem?>();
            //foreach(var group in GroupingByCategory)
            //{
            //    foreach(var item in group)
            //    {
            //        productItems.Add(item);
            //    }
            //}
            //ProductView.ItemsSource = productItems;
            //CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(ProductView.ItemsSource);
            //if (view.GroupDescriptions.Count < 1)
            //{
            //    PropertyGroupDescription groupDescription = new PropertyGroupDescription("Category");
            //    view.GroupDescriptions.Add(groupDescription);
            //}


        }
    }
}
