using HTools.Uwp.Controls.Loading;
using System;

namespace HTools.Uwp.Helpers
{
    /// <summary>
    /// 
    /// </summary>
    public static class LoadingHelper
    {
        #region Loading
        /// <summary>
        /// 
        /// </summary>
        public static Action<bool> IsLoadingChanged;

        private static bool _isLoading = false;
        /// <summary>
        /// 
        /// </summary>
        public static bool IsLoading
        {
            get { return _isLoading; }
            private set
            {
                _isLoading = value;
                IsLoadingChanged?.Invoke(value);
            }
        }

        private static LayoutLoading _loading = null;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="loadingStr">加载时的文本，空字符串不显示</param>
        /// <param name="clickable">能否点击界面元素</param>
        /// <param name="backgroundOpacity">背景透明度</param>
        /// <param name="paneOpacity">加载Loading背景透明度</param>
        public static void Show(string loadingStr = "", bool clickable = false, double backgroundOpacity = 0.5, double paneOpacity = 0.6)
        {
            if (_loading == null)
            {
                _loading = new LayoutLoading();
            }

            _loading.Show(loadingStr, clickable, backgroundOpacity, paneOpacity);
            IsLoading = true;
        }

        /// <summary>
        /// 
        /// </summary>
        public static void Hide()
        {
            _loading?.Hide();
            _loading = null;
            IsLoading = false;
        }
        #endregion
    }
}