using Windows.UI;

namespace Hubery.Tools.Uwp.Controls.ColorSelecter
{
    /// <summary>
    /// 
    /// </summary>
    public class ColorItem
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="color"></param>
        /// <param name="name"></param>
        /// <param name="isDark"></param>
        public ColorItem(Color color, string name, bool isDark = false)
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
        public Color Color { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsDark { get; set; }
    }
}