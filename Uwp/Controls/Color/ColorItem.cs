namespace Hubery.Tools.Uwp.Controls.Color
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
        /// <param name="name"></param>
        /// <param name="isDark"></param>
        public ColorItem(Windows.UI.Color color, string name, bool isDark = false)
        {
            Color = color;
            Name = name;
            IsDark = isDark;
        }

        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Windows.UI.Color Color { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsDark { get; set; }
    }
}