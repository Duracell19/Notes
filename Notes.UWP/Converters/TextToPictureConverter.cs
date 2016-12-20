using System;
using Windows.UI.Xaml.Data;

namespace Notes.UWP.Converters
{
    public class TextToPictureConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            string picture = "";

            if (value == null)
            {
                return picture;
            }

            var collection = value as string;

            if (collection == null)
            {
                throw new ArgumentException("TextToPictureConverter work only with IEnumerable type");
            }

            var data = collection.Split('.');
            if (data.Length > 0)
            {
                switch (data[data.Length - 1])
                {
                    case "png": picture = "ms-appx:///Assets/image.png"; break;
                    case "jpg": picture = "ms-appx:///Assets/image.png"; break;
                    case "jpeg": picture = "ms-appx:///Assets/image.png"; break;
                    case "pdf": picture = "ms-appx:///Assets/pdf.png"; break;
                    case "txt": picture = "ms-appx:///Assets/txt.png"; break;
                    case "doc": picture = "ms-appx:///Assets/docx.png"; break;
                    case "docx": picture = "ms-appx:///Assets/docx.png"; break;
                    case "xls": picture = "ms-appx:///Assets/excel.png"; break;
                    case "xlsx": picture = "ms-appx:///Assets/excel.png"; break;
                    default: picture = "ms-appx:///Assets/unknown.png"; break;
                }
            }
            return picture;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
