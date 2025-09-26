using System.Windows;

namespace H.Tools.Wpf.Proxy;

public class BindingProxy : Freezable
{
    protected override Freezable CreateInstanceCore()
    {
        return new BindingProxy();
    }

    public object DataContext
    {
        get { return GetValue(DataContextProperty); }
        set { SetValue(DataContextProperty, value); }
    }

    // Using a DependencyProperty as the backing store for DataContext.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty DataContextProperty =
        DependencyProperty.Register("DataContext", typeof(object), typeof(BindingProxy), new PropertyMetadata(null));
}
