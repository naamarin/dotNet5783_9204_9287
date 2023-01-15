using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace PL
{
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
                string imageRelativeName = @"\Images\Fries.png";
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
}
