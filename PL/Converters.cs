using BO;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace PL;

class ConvertImagePathToBitmap : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        try
        {
            string imageRelativeName=(string)value;
            string currentDir = Environment.CurrentDirectory[..^4];
            string imageFullName = currentDir + imageRelativeName;
            BitmapImage bitmapImage = new BitmapImage(new Uri(imageFullName));
            return bitmapImage;
        }
        catch (Exception ex)
        {
            string imageRelativeName = @"\Images\No image.png";
            string currentDir = Environment.CurrentDirectory[..^4];
            string imageFullName = currentDir + imageRelativeName;
            BitmapImage bitmapImage = new BitmapImage(new Uri(imageFullName));
            return bitmapImage;

        }
    }
    public object ConvertBack(object value,Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }

}
class ConvertPrograssBarToColor : IValueConverter
{
    static readonly Random rand = new Random();
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        OrderStatus orderStatus=(OrderStatus)value;
        switch(orderStatus)
        {
            case OrderStatus.Ordered:
                return rand.Next(0,25);
            case OrderStatus.Shipped:
                return rand.Next(25, 75);
            case OrderStatus.Delivered:
                return 100;
            default:
                return 0;
        }
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
