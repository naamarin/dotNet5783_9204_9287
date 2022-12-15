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
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get();

        int productID;
        public Window1(int id = 0)
        {
            productID = id;
            InitializeComponent();
            CategoryOptions.ItemsSource = Enum.GetValues(typeof(BO.Category));
            if (id != 0)
            {
                BO.Product product = bl.Product.GetById(productID);
                txbProductID.Text = product.ID.ToString();
                CategoryOptions.SelectedItem = product.Category;
                txbProductName.Text = product.Name;
                txbProductPrice.Text = product.Price.ToString();
                txbProductStockCount.Text = product.StockCount.ToString();
                btAddProduct.Visibility = Visibility.Hidden;
                btUpdateProduct.Visibility = Visibility.Visible;
                txbProductID.IsReadOnly = true;
            }
            else
            {
                txbProductID.Visibility = Visibility.Visible;
                btAddProduct.Visibility = Visibility.Visible;
                btUpdateProduct.Visibility = Visibility.Hidden;
            }
        }

        private void btAddProduct_Click(object sender, RoutedEventArgs e)
        {
            if (txbProductID.Text == "" || CategoryOptions.SelectedItem == null || txbProductName.Text == "" || txbProductPrice.Text == "" || txbProductStockCount.Text == "")
            {
                MessageBox.Show("One or more of the requested fields are empty", "ERROR");
                return;
            }
            try
            {
                bl?.Product.AddProduct(new BO.Product
                {
                    ID = int.Parse(txbProductID.Text),
                    Category = (BO.Category)CategoryOptions.SelectedItem,
                    Name = txbProductName.Text,
                    Price = int.Parse(txbProductPrice.Text),
                    StockCount = int.Parse(txbProductStockCount.Text),
                });
                this.Close();
            }
            catch (ArgumentException boEx)
            {
                MessageBox.Show(boEx.Message, "ERROR");
            }
            catch (Exception ex)
            {
                MessageBox.Show("ID, Price and amount must be numbers", "ERROR");
            }
        }

        private void btUpdateProduct_Click(object sender, RoutedEventArgs e)
        {
            if (txbProductID.Text == "" || CategoryOptions.SelectedItem == null || txbProductName.Text == "" || txbProductPrice.Text == "" || txbProductStockCount.Text == "")
            {
                MessageBox.Show("One or more of the requested fields are empty", "ERROR");
                return;
            }
            try
            {
                bl?.Product.UpdateProduct(new BO.Product
                {
                    ID = int.Parse(txbProductID.Text),
                    Category = (BO.Category)CategoryOptions.SelectedItem,
                    Name = txbProductName.Text,
                    Price = int.Parse(txbProductPrice.Text),
                    StockCount = int.Parse(txbProductStockCount.Text),
                });
                this.Close();
            }
            catch (ArgumentException boEx)
            {
                MessageBox.Show(boEx.Message, "ERROR");
            }
            catch (Exception ex)
            {
                MessageBox.Show("ID, Price and amount must be numbers", "ERROR");
            }
            
        }

    }
}
