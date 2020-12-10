using GalaSoft.MvvmLight;

namespace HTools.Uwp.Controls.Setting
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class SettingSplitViewModel : ViewModelBase
    {
        private bool _isLoading = false;
        /// <summary>
        /// 
        /// </summary>
        public bool IsLoading
        {
            get => _isLoading;
            set => Set(ref _isLoading, value);
        }
    }
}
