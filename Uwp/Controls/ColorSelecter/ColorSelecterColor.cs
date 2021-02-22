namespace HTools.Uwp.Controls
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class ColorSelecterColor
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="color"></param>
        public ColorSelecterColor(Windows.UI.Color color)
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