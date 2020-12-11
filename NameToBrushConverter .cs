using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace Attestation
{
    public class NameToBrushConverter : IValueConverter
    {
        private Global global;
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            global = Global.getInstance();
            string input = value as string;
            if (global.checkSum(input))
            {
                return Brushes.LightGreen;
            }
            else
            {
                return Brushes.LightPink;
            }
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
