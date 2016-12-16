using System;
using Windows.UI.Xaml.Data;

namespace Notes.UWP.Converters
{
    public class BoolToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string culture)
        {
            var boolValue = value as bool?;
            if (boolValue == null)
            {
                throw new ArgumentException("BoolToVisibilityConverter work only with bool type");
            }
            return ((bool)value) ?  false : true;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string culture)
        {
            throw new NotImplementedException();
        }
    }
}
 