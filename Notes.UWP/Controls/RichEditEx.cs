﻿using Windows.UI.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Notes.UWP
{
    public class RichEditEx : RichEditBox
    {
        public RichEditEx() : base()
        {
            this.TextChanged += RichEditEx_TextChanged;
        }

        private void RichEditEx_TextChanged(object sender, RoutedEventArgs e)
        {
            var t = GetText();
            if (t != Text)
            {
                Text = t;
            }
        }
        public TextGetOptions TextGetOption { get; set; } = TextGetOptions.None;
        public TextSetOptions TextSetOption { get; set; } = TextSetOptions.None;

        string GetText()
        {
            string t;
            this.Document.GetText(TextGetOption, out t);
            return t;
        }


        void SetText(string text)
        {
            if (GetText() != text)
            {
                this.Document.SetText(TextSetOption, text);
            }
        }

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }
        // Using a DependencyProperty as the backing store for TextProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(RichEditEx), new PropertyMetadata(string.Empty, callback));

        private static void callback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var reb = (RichEditEx)d;
            reb.SetText((string)e.NewValue);
        }
    }
}
