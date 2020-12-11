using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace Attestation
{
    public class NameToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string input = value as string;
            if(input == "00000000")
            {
                return Brushes.LightGreen;
            }
            else
            {
                return Brushes.LightPink;
            }

            /*if (value == null) return Colors.White.ToString();

            if (value.ToString().ToUpper().Contains("CMS")) return "LIME";

            return "ORANGE";*/
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
