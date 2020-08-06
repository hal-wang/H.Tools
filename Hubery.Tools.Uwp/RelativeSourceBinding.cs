using Hubery.Tools.Uwp.Helpers;
using System;
using Windows.UI.Xaml;

namespace Hubery.Tools.Uwp
{
    /// <summary>
    /// 
    /// </summary>
    public class RelativeSourceBinding
    {
        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty AncestorTypeProperty = DependencyProperty.RegisterAttached("AncestorType", typeof(Type), typeof(RelativeSourceBinding), new PropertyMetadata(default(Type), OnAncestorTypeChanged));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetAncestorType(DependencyObject element, Type value)
        {
            element.SetValue(AncestorTypeProperty, value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static Type GetAncestorType(DependencyObject element)
        {
            return (Type)element.GetValue(AncestorTypeProperty);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        private static void OnAncestorTypeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((FrameworkElement)d).Loaded -= OnFrameworkElementLoaded;

            if (e.NewValue != null)
            {
                ((FrameworkElement)d).Loaded += OnFrameworkElementLoaded;
                OnFrameworkElementLoaded((FrameworkElement)d, null);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void OnFrameworkElementLoaded(object sender, RoutedEventArgs e)
        {
            var ancestorType = GetAncestorType((FrameworkElement)sender);
            if (ancestorType != null)
            {
                var findAncestor = ((FrameworkElement)sender).FindVisualParent(ancestorType);
                RelativeSourceBinding.SetAncestor(((FrameworkElement)sender), findAncestor);
            }
            else
            {
                RelativeSourceBinding.SetAncestor(((FrameworkElement)sender), null);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty AncestorProperty = DependencyProperty.RegisterAttached("Ancestor", typeof(UIElement), typeof(RelativeSourceBinding), new PropertyMetadata(default(FrameworkElement)));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetAncestor(DependencyObject element, UIElement value)
        {
            element.SetValue(AncestorProperty, value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static UIElement GetAncestor(DependencyObject element)
        {
            return (UIElement)element.GetValue(AncestorProperty);
        }
    }
}
