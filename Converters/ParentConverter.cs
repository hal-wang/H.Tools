using Microsoft.Toolkit.Uwp.UI.Extensions;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace HTools.Converters
{
    internal class ParentConverter : IValueConverter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter">目标控件（Tag值 或 层数）:属性名</param>
        /// <param name="language"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            FrameworkElement element = value as FrameworkElement;
            if (parameter is string)
            {
                var result = element.FindParentByName(parameter as string);
                if (result != null)
                {
                    return result;
                }
            }

            Type type;
            if (parameter is Type t)
            {
                type = t;
            }
            else if (parameter is string str)
            {
                type = Type.GetType(str);
            }
            else
            {
                throw new NotSupportedException();
            }

            var mi = typeof(LogicalTree).GetMethod("FindParent").MakeGenericMethod(type);
            return mi.Invoke(null, new object[] { element });
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
