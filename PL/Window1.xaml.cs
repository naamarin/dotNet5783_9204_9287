using BO;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
        public BO.Product? currentProduct
        {
            get { return (BO.Product?)GetValue(currentProductProperty); }
            set { SetValue(currentProductProperty, value); }
        }
        public static readonly DependencyProperty currentProductProperty = 
            DependencyProperty.Register("currentProduct", typeof(BO.Product), typeof(Window), new PropertyMetadata(null));
        int productID;
        public Window1(int id = 0)
        {
            productID = id;
            InitializeComponent();
            CategoryOptions.ItemsSource = Enum.GetValues(typeof(BO.Category));
            if (id != 0)
            {
                currentProduct = bl.Product.GetById(productID);
                //txbProductID.Text = product.ID.ToString();
                //CategoryOptions.SelectedItem = product.Category;
                //txbProductName.Text = product.Name;
                //txbProductPrice.Text = product.Price.ToString();
                //txbProductStockCount.Text = product.StockCount.ToString();
                //NewImage.Source = new BitmapImage(new Uri(currentProduct.ImageRelativeName!));
                btAddProduct.Visibility = Visibility.Hidden;
                btUpdateProduct.Visibility = Visibility.Visible;
                txbProductID.IsReadOnly = true;
                txbProductID.Foreground =Brushes.Gray;
                AddImage.Visibility = Visibility.Hidden;
                UpdateImage.Visibility = Visibility.Visible;
            }
            else
            {
                currentProduct = new BO.Product();  //?
                txbProductID.Visibility = Visibility.Visible;
                btAddProduct.Visibility = Visibility.Visible;
                btUpdateProduct.Visibility = Visibility.Hidden;
                UpdateImage.Visibility = Visibility.Hidden;
                AddImage.Visibility = Visibility.Visible;
            }
        }

        private void btAddProduct_Click(object sender, RoutedEventArgs e)
        {
            if (txbProductID.Text == "" || CategoryOptions.SelectedItem == null || txbProductName.Text == "" || txbProductPrice.Text == "" || txbProductStockCount.Text == "")
            {
                MessageBox.Show("One or more of the requested fields are empty", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {

                bl?.Product.AddProduct(currentProduct);
                //bl?.Product.AddProduct(new BO.Product
                //{
                //    ID = int.Parse(txbProductID.Text),
                //    Category = (BO.Category)CategoryOptions.SelectedItem,
                //    Name = txbProductName.Text,
                //    Price = int.Parse(txbProductPrice.Text),
                //    StockCount = int.Parse(txbProductStockCount.Text),
                //});
                this.Close();
            }
            catch (ArgumentException boEx)
            {
                MessageBox.Show(boEx.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("ID, Price and amount must be numbers", "ERROR",MessageBoxButton.OK,MessageBoxImage.Warning);
            }
        }

        private void btUpdateProduct_Click(object sender, RoutedEventArgs e)
        {
            if (txbProductID.Text == "" || CategoryOptions.SelectedItem == null || txbProductName.Text == "" || txbProductPrice.Text == "" || txbProductStockCount.Text == "")
            {
                MessageBox.Show("One or more of the requested fields are empty", "ERROR",MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                bl?.Product.UpdateProduct(currentProduct!);
                //bl?.Product.UpdateProduct(new BO.Product
                //{
                //    ID = int.Parse(txbProductID.Text),
                //    Category = (BO.Category)CategoryOptions.SelectedItem,
                //    Name = txbProductName.Text,
                //    Price = int.Parse(txbProductPrice.Text),
                //    StockCount = int.Parse(txbProductStockCount.Text),
                //});
                this.Close();
            }
            catch (ArgumentException boEx)
            {
                MessageBox.Show(boEx.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("ID, Price and amount must be numbers", "ERROR", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog()==true)
            {
                NewImage.Source = new BitmapImage(new Uri(openFileDialog.FileName));
                openFileDialog.FileName = lName.Content.ToString() + "pnj";
                File.Move(@"C:\Users\User\source\repos\shoham212\dotNet5783_9204_9287\Images", openFileDialog.FileName);
            }
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
                if (openFileDialog.ShowDialog() == true)
                {
                NewImage.Source = new BitmapImage(new Uri(openFileDialog.FileName));
                openFileDialog.FileName = lName.Content.ToString() + "pnj";
                File.Move(@"C:\Users\User\source\repos\shoham212\dotNet5783_9204_9287\Images", openFileDialog.FileName);
                }
        }
    }
}
