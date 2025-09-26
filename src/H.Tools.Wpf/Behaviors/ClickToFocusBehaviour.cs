using Microsoft.Xaml.Behaviors;
using System.Windows;
using System.Windows.Input;

namespace H.Tools.Wpf.Behaviors;

public sealed class ClickToFocusBehaviour : Behavior<UIElement>
{
    public static readonly DependencyProperty TargetProperty =
        DependencyProperty.Register("Target", typeof(UIElement), typeof(ClickToFocusBehaviour),
        new FrameworkPropertyMetadata(default(UIElement), FrameworkPropertyMetadataOptions.None, OnTargetChanged));

    public UIElement Target
    {
        get { return (UIElement)GetValue(TargetProperty); }
        set { SetValue(TargetProperty, value); }
    }

    private static void OnTargetChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        (d as ClickToFocusBehaviour)!.SetFocusVisualStyle();
    }

    protected override void OnAttached()
    {
        base.OnAttached();

        SetFocusVisualStyle();
        AssociatedObject.PreviewMouseLeftButtonDown += PreviewMouseLeftButtonDown;
    }

    protected override void OnDetaching()
    {
        base.OnDetaching();

        AssociatedObject.PreviewMouseLeftButtonDown -= PreviewMouseLeftButtonDown;
    }

    private void SetFocusVisualStyle()
    {
        if (Target is not FrameworkElement fe) return;

        fe.FocusVisualStyle = null;
    }

    private void PreviewMouseLeftButtonDown(object? sender, MouseButtonEventArgs eventArgs)
    {
        Target?.Focus();
    }
}