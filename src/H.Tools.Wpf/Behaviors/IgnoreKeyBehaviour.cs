using Microsoft.Xaml.Behaviors;
using System.Windows;
using System.Windows.Input;

namespace H.Tools.Wpf.Behaviors;

public class IgnoreKeyBehaviour : Behavior<FrameworkElement>
{
    public Key? Key
    {
        get { return (Key?)GetValue(KeyProperty); }
        set { SetValue(KeyProperty, value); }
    }

    // Using a DependencyProperty as the backing store for Key.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty KeyProperty =
        DependencyProperty.Register("Key", typeof(Key?), typeof(IgnoreKeyBehaviour), new PropertyMetadata(null));


    protected override void OnAttached()
    {
        base.OnAttached();

        if (AssociatedObject != null)
        {
            AssociatedObject.FocusVisualStyle = null;
            AssociatedObject.PreviewKeyDown += OnPreviewKey;
            AssociatedObject.PreviewKeyUp += OnPreviewKey;
            AssociatedObject.KeyUp += OnPreviewKey;
            AssociatedObject.KeyDown += OnPreviewKey;
        }
    }

    protected override void OnDetaching()
    {
        base.OnDetaching();

        if (AssociatedObject != null)
        {
            AssociatedObject.PreviewKeyDown -= OnPreviewKey;
            AssociatedObject.PreviewKeyUp -= OnPreviewKey;
            AssociatedObject.KeyUp -= OnPreviewKey;
            AssociatedObject.KeyDown -= OnPreviewKey;
        }
    }

    private void OnPreviewKey(object sender, KeyEventArgs e)
    {
        if (Key == null) return;

        if (e.Key == Key)
        {
            e.Handled = true;
        }
    }
}
