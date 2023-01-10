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
    /// Interaction logic for ProductItemView.xaml
    /// </summary>
    public partial class ProductItemView : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get();
        BO.Cart cart = new BO.Cart();
        public BO.ProductItem? currentProduct
        {
            get { return (BO.ProductItem?)GetValue(currentProductProperty); }
            set { SetValue(currentProductProperty, value); }
        }
        public static readonly DependencyProperty currentProductProperty =
            DependencyProperty.Register("currentProduct", typeof(BO.ProductItem), typeof(Window), new PropertyMetadata(null));
        public ProductItemView(int id, BO.Cart c)
        {
            InitializeComponent();
            currentProduct = bl.Product.ProductDeatails(id);
            if (!currentProduct.InStock)
                btnAddToCart.Visibility = Visibility.Hidden;
            cart = c;
        }

        private void btnAddToCart_Click(object sender, RoutedEventArgs e)
        {
            cart = bl.Cart.AddItemToCart(cart, currentProduct.ID);
            this.Close();
        }
    }
}
