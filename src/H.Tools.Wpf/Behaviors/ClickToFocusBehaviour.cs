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
        if (d is not ClickToFocusBehaviour behaviour) return;

        behaviour.SetFocusVisualStyle();
    }

    public bool PreviewMouseLeftButtonDownEnable
    {
        get { return (bool)GetValue(PreviewMouseLeftButtonDownEnableProperty); }
        set { SetValue(PreviewMouseLeftButtonDownEnableProperty, value); }
    }

    // Using a DependencyProperty as the backing store for PreviewMouseLeftButtonDownEnable.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty PreviewMouseLeftButtonDownEnableProperty =
        DependencyProperty.Register(nameof(PreviewMouseLeftButtonDownEnable), typeof(bool), typeof(ClickToFocusBehaviour), new PropertyMetadata(true, OnEnableChanged));


    public bool PreviewMouseLeftButtonUpEnable
    {
        get { return (bool)GetValue(PreviewMouseLeftButtonUpEnableProperty); }
        set { SetValue(PreviewMouseLeftButtonUpEnableProperty, value); }
    }

    // Using a DependencyProperty as the backing store for PreviewMouseLeftButtonUpEnable.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty PreviewMouseLeftButtonUpEnableProperty =
        DependencyProperty.Register(nameof(PreviewMouseLeftButtonUpEnable), typeof(bool), typeof(ClickToFocusBehaviour), new PropertyMetadata(false, OnEnableChanged));


    public bool MouseLeftButtonDownEnable
    {
        get { return (bool)GetValue(MouseLeftButtonDownEnableProperty); }
        set { SetValue(MouseLeftButtonDownEnableProperty, value); }
    }

    // Using a DependencyProperty as the backing store for MouseLeftButtonDownEnable.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty MouseLeftButtonDownEnableProperty =
        DependencyProperty.Register(nameof(MouseLeftButtonDownEnable), typeof(bool), typeof(ClickToFocusBehaviour), new PropertyMetadata(false, OnEnableChanged));


    public bool MouseLeftButtonUpEnable
    {
        get { return (bool)GetValue(MouseLeftButtonUpEnableProperty); }
        set { SetValue(MouseLeftButtonUpEnableProperty, value); }
    }

    // Using a DependencyProperty as the backing store for MouseLeftButtonUpEnable.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty MouseLeftButtonUpEnableProperty =
        DependencyProperty.Register(nameof(MouseLeftButtonUpEnable), typeof(bool), typeof(ClickToFocusBehaviour), new PropertyMetadata(true, OnEnableChanged));


    private static void OnEnableChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not ClickToFocusBehaviour behaviour) return;

        behaviour.AttachEvents();
    }

    private void AttachEvents()
    {
        DetachEvents();

        if (PreviewMouseLeftButtonDownEnable)
        {
            AssociatedObject.PreviewMouseLeftButtonDown += SetFacusEvent;
        }
        if (PreviewMouseLeftButtonUpEnable)
        {
            AssociatedObject.PreviewMouseLeftButtonUp += SetFacusEvent;
        }
        if (MouseLeftButtonDownEnable)
        {
            AssociatedObject.MouseLeftButtonDown += SetFacusEvent;
        }
        if (MouseLeftButtonUpEnable)
        {
            AssociatedObject.MouseLeftButtonUp += SetFacusEvent;
        }
    }

    private void DetachEvents()
    {
        AssociatedObject.PreviewMouseLeftButtonDown -= SetFacusEvent;
        AssociatedObject.PreviewMouseLeftButtonUp -= SetFacusEvent;
        AssociatedObject.MouseLeftButtonDown -= SetFacusEvent;
        AssociatedObject.MouseLeftButtonUp -= SetFacusEvent;
    }

    protected override void OnAttached()
    {
        base.OnAttached();

        SetFocusVisualStyle();
        AttachEvents();
    }

    protected override void OnDetaching()
    {
        base.OnDetaching();

        DetachEvents();
    }

    private void SetFocusVisualStyle()
    {
        if (Target is not FrameworkElement fe) return;

        fe.FocusVisualStyle = null;
    }

    private void SetFacusEvent(object? sender, MouseButtonEventArgs eventArgs)
    {
        Target?.Focus();
    }
}
