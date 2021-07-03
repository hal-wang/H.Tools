using Windows.UI.Xaml;

namespace HTools.Uwp
{
    /// <summary>
    /// 用于向VM传递View，建议少用，不符合MVVM设计思想
    /// 主要用来配合FrameworkElement.FindName找元素，目前用于贴靠提醒ShowSticky
    /// </summary>
    public interface IElementViewModel
    {
        /// <summary>
        /// 
        /// </summary>
        FrameworkElement View { get; set; }
    }
}
