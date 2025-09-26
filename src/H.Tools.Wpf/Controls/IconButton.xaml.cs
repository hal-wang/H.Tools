using System.Windows;
using System.Windows.Controls;

namespace H.Tools.Wpf.Controls;

public partial class IconButton : Button
{
    public IconButton()
    {
        InitializeComponent();
    }


    public string Icon
    {
        get { return (string)GetValue(IconProperty); }
        set { SetValue(IconProperty, value); }
    }

    // Using a DependencyProperty as the backing store for Icon.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty IconProperty =
        DependencyProperty.Register("Icon", typeof(string), typeof(IconButton), new PropertyMetadata(string.Empty));


    public double IconSize
    {
        get { return (double)GetValue(IconSizeProperty); }
        set { SetValue(IconSizeProperty, value); }
    }

    // Using a DependencyProperty as the backing store for IconSize.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty IconSizeProperty =
        DependencyProperty.Register("IconSize", typeof(double), typeof(IconButton), new PropertyMetadata(20.0));
}
