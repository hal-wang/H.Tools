namespace Hubery.Tools.Uwp.Controls.Loading
{
    /// <summary>
    /// 
    /// </summary>
    public interface ILoading
    {
        /// <summary>
        /// 
        /// </summary>
        public bool Clickable { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public double BackgroundOpacity { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public double PaneOpacity { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public double Size { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string LoadingStr { get; set; }
    }
}
