using BlApi;
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
                btAddProduct.Visibility = Visibility.Hidden;
                btUpdateProduct.Visibility = Visibility.Visible;
                txbProductID.IsReadOnly = true;
                txbProductID.Foreground =Brushes.White;
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
            BO.Product product = bl.Product.GetById(int.Parse(txbProductID.Text));
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog()==true)
            {
                NewImage.Source = new BitmapImage(new Uri(openFileDialog.FileName));
                openFileDialog.FileName = txbProductName.Text.ToString() + ".png";
                string imageName = product.ImageRelativeName.Substring(product.ImageRelativeName.LastIndexOf("\\"));
                if (!File.Exists(Environment.CurrentDirectory[..^4] + @"\Images\" + imageName))
                {
                    File.Copy(product.ImageRelativeName, Environment.CurrentDirectory[..^4] + @"\Images\" + imageName);
                    product.ImageRelativeName = @"\Images\" + imageName;
                }
                bl.Product.AddProduct(product!);
                MessageBox.Show("New Product was added succesfully","Success",MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            BO.Product product = bl.Product.GetById(int.Parse(txbProductID.Text));
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                string imageName = openFileDialog.FileName.
                string newN = Environment.CurrentDirectory[..^4] + @"\Images\" + openFileDialog.FileName;
                

                //NewImage.Source = new BitmapImage(new Uri(openFileDialog.FileName));
                //openFileDialog.FileName = txbProductName.Text.ToString() + ".png";
                //string imageName = product.ImageRelativeName.Substring(product.ImageRelativeName.LastIndexOf("\\"));
                if (!File.Exists(Environment.CurrentDirectory[..^4] + @"\Images\" + imageName))
                {
                    File.Copy(openFileDialog.FileName, newN);
                    //File.Copy(product.ImageRelativeName, Environment.CurrentDirectory[..^4] + @"\Images\" + imageName);
                    product.ImageRelativeName = @"\Images\" + imageName;
                }
                bl.Product.UpdateProduct(product!);
                MessageBox.Show("New Product was added succesfully", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
