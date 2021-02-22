using HTools.Uwp.Helpers;

namespace HTools.Uwp.Controls.Color
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class ColorItem
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="color"></param>
        public ColorItem(Windows.UI.Color color)
        {
            Color = color;
        }

        /// <summary>
        /// display name
        /// </summary>
        public string Name => ResourcesHelper.GetHToolsResStr($"ColorSelecterColor{Color.ToString().Replace("#", "")}");

        /// <summary>
        /// 
        /// </summary>
        public Windows.UI.Color Color { get; set; }
    }
}