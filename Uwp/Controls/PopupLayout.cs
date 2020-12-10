using HTools.Uwp.Helpers;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;

namespace HTools.Uwp.Controls
{
    /// <summary>
    /// 好用的弹出层
    /// </summary>
    public class PopupLayout : ContentControl
    {
        private readonly Popup _popup;
        /// <summary>
        /// 
        /// </summary>
        public PopupLayout()
        {
            RequestedTheme = ThemeHelper.ElementTheme;
            ThemeHelper.ThemeChanged += () => RequestedTheme = ThemeHelper.ElementTheme;

            HorizontalAlignment = HorizontalAlignment.Stretch;
            HorizontalContentAlignment = HorizontalAlignment.Stretch;
            VerticalAlignment = VerticalAlignment.Stretch;
            VerticalContentAlignment = VerticalAlignment.Stretch;

            WindowSizeChanged();
            Window.Current.SizeChanged += (ss, ee) => WindowSizeChanged();
            _popup = new Popup()
            {
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch,
                Child = this
            };
        }

        private void WindowSizeChanged()
        {
            this.Width = Window.Current.Bounds.Width;
            this.Height = Window.Current.Bounds.Height;
        }

        /// <summary>
        /// 
        /// </summary>
        public bool IsOpen
        {
            get { return (bool)GetValue(IsOpenProperty); }
            set { SetValue(IsOpenProperty, value); }
        }

        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty IsOpenProperty =
            DependencyProperty.Register("IsOpen", typeof(bool), typeof(PopupLayout), new PropertyMetadata(false, OnIsOpenChanged));

        private static void OnIsOpenChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as PopupLayout)._popup.IsOpen = (bool)e.NewValue;
            (d as PopupLayout).IsOpenChanged?.Invoke((bool)e.NewValue);
        }

        /// <summary>
        /// 
        /// </summary>
        public Action<bool> IsOpenChanged;

        /// <summary>
        /// 
        /// </summary>
        public bool IsLoading
        {
            get { return (bool)GetValue(IsLoadingProperty); }
            set { SetValue(IsLoadingProperty, value); }
        }

        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty IsLoadingProperty =
            DependencyProperty.Register("IsLoading", typeof(bool), typeof(PopupLayout), new PropertyMetadata(false, (d, e) => (d as PopupLayout).IsLoadingChanged?.Invoke((bool)e.NewValue)));

        /// <summary>
        /// 
        /// </summary>
        public Action<bool> IsLoadingChanged;
    }
}
