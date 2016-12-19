using System;
using System.Linq;
using Windows.UI.Xaml.Data;

namespace Notes.UWP.Converters
{
    public class TextToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value == null)
            {
                return false;
            }

            var collection = value as string;
            
            if (collection == null)
            {
                throw new ArgumentException("TextToVisibilityConverter work only with IEnumerable type");
            }
            
            return collection.Any() ? true : false;
        }
        
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }

    }
}
