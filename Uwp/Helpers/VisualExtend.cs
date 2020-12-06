using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;

namespace Hubery.Tools.Uwp.Helpers
{
    /// <summary>
    /// 
    /// </summary>
    public static class VisualExtend
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="element"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static UIElement FindVisualParent(this UIElement element, Type type)
        {
            UIElement parent = element;
            while (parent != null)
            {
                if (type.IsAssignableFrom(parent.GetType()))
                {
                    return parent;
                }
                parent = VisualTreeHelper.GetParent(parent) as UIElement;
            }
            return null;
        }
    }
}
