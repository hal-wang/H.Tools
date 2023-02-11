using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace UwpTest.Views
{
    public sealed partial class ConvertersPage : Page
    {
        public ConvertersPage()
        {
            this.InitializeComponent();
        }

        public string TestString
        {
            get { return (string)GetValue(TestStringProperty); }
            set { SetValue(TestStringProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TestString.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TestStringProperty =
            DependencyProperty.Register("TestString", typeof(string), typeof(ConvertersPage), new PropertyMetadata("test"));
    }
}
