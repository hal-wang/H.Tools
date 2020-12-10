using System;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace HTools.Uwp.Controls.Dialog
{
    /// <summary>
    /// ConentDialog虽好，但不能多用哦！ 
    /// 用LayoutDialog吧！ 
    /// 可以无限叠加调用的对话框。与StickyMessage不冲突。
    /// 设置IsOpen=false即关闭对话框并返回Result。
    /// 可以自由控制对话框关闭时机。
    /// </summary>
    public class LayoutDialog : PopupLayout
    {
        #region DependencyProperty
        /// <summary>
        /// 
        /// </summary>
        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(LayoutDialog), new PropertyMetadata(string.Empty));

        /// <summary>
        /// 
        /// </summary>
        public double TitleFontSize
        {
            get { return (double)GetValue(TitleFontSizeProperty); }
            set { SetValue(TitleFontSizeProperty, value); }
        }

        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty TitleFontSizeProperty =
            DependencyProperty.Register("TitleFontSize", typeof(double), typeof(LayoutDialog), new PropertyMetadata(18));

        /// <summary>
        /// 
        /// </summary>
        public bool IsExitButtonVisible
        {
            get { return (bool)GetValue(IsExitButtonVisibleProperty); }
            set { SetValue(IsExitButtonVisibleProperty, value); }
        }

        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty IsExitButtonVisibleProperty =
            DependencyProperty.Register("IsExitButtonVisible", typeof(bool), typeof(LayoutDialog), new PropertyMetadata(false));

        /// <summary>
        /// 
        /// </summary>
        public bool CloseButtonVisible
        {
            get { return (bool)GetValue(CloseButtonVisibleProperty); }
            set { SetValue(CloseButtonVisibleProperty, value); }
        }

        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty CloseButtonVisibleProperty =
            DependencyProperty.Register("CloseButtonVisible", typeof(bool), typeof(LayoutDialog), new PropertyMetadata(false));

        /// <summary>
        /// 
        /// </summary>
        public string PrimaryButtonText
        {
            get { return (string)GetValue(PrimaryButtonTextProperty); }
            set { SetValue(PrimaryButtonTextProperty, value); }
        }

        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty PrimaryButtonTextProperty =
            DependencyProperty.Register("PrimaryButtonText", typeof(string), typeof(LayoutDialog), new PropertyMetadata(null, (d, e) => (d as LayoutDialog).SetBtnVisible()));
        /// <summary>
        /// 
        /// </summary>
        public string SecondaryButtonText
        {
            get { return (string)GetValue(SecondaryButtonTextProperty); }
            set { SetValue(SecondaryButtonTextProperty, value); }
        }

        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty SecondaryButtonTextProperty =
            DependencyProperty.Register("SecondaryButtonText", typeof(string), typeof(LayoutDialog), new PropertyMetadata(null, (d, e) => (d as LayoutDialog).SetBtnVisible()));

        /// <summary>
        /// 
        /// </summary>
        public string CloseButtonText
        {
            get { return (string)GetValue(CloseButtonTextProperty); }
            set { SetValue(CloseButtonTextProperty, value); }
        }
        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty CloseButtonTextProperty =
            DependencyProperty.Register("CloseButtonText", typeof(string), typeof(LayoutDialog), new PropertyMetadata(null, (d, e) => (d as LayoutDialog).SetBtnVisible()));

        private void SetBtnVisible()
        {
            if (string.IsNullOrEmpty(PrimaryButtonText) && string.IsNullOrEmpty(SecondaryButtonText) && string.IsNullOrEmpty(CloseButtonText))
            {
                VisualStateManager.GoToState(this, "NoneVisible", false);
            }
            else if (!string.IsNullOrEmpty(PrimaryButtonText) && string.IsNullOrEmpty(SecondaryButtonText) && string.IsNullOrEmpty(CloseButtonText))
            {
                VisualStateManager.GoToState(this, "PrimaryVisible", false);
            }
            else if (string.IsNullOrEmpty(PrimaryButtonText) && !string.IsNullOrEmpty(SecondaryButtonText) && string.IsNullOrEmpty(CloseButtonText))
            {
                VisualStateManager.GoToState(this, "SecondaryVisible", false);
            }
            else if (string.IsNullOrEmpty(PrimaryButtonText) && string.IsNullOrEmpty(SecondaryButtonText) && !string.IsNullOrEmpty(CloseButtonText))
            {
                VisualStateManager.GoToState(this, "CloseVisible", false);
            }
            else if (!string.IsNullOrEmpty(PrimaryButtonText) && !string.IsNullOrEmpty(SecondaryButtonText) && string.IsNullOrEmpty(CloseButtonText))
            {
                VisualStateManager.GoToState(this, "PrimaryAndSecondaryVisible", false);
            }
            else if (!string.IsNullOrEmpty(PrimaryButtonText) && string.IsNullOrEmpty(SecondaryButtonText) && !string.IsNullOrEmpty(CloseButtonText))
            {
                VisualStateManager.GoToState(this, "PrimaryAndCloseVisible", false);
            }
            else if (string.IsNullOrEmpty(PrimaryButtonText) && !string.IsNullOrEmpty(SecondaryButtonText) && !string.IsNullOrEmpty(CloseButtonText))
            {
                VisualStateManager.GoToState(this, "SecondaryAndCloseVisible", false);
            }
            else
            {
                VisualStateManager.GoToState(this, "AllVisible", false);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public ContentDialogButton DefaultButton
        {
            get { return (ContentDialogButton)GetValue(DefaultButtonProperty); }
            set { SetValue(DefaultButtonProperty, value); }
        }

        /// <summary>
        /// 
        /// </summary>
        // Using a DependencyProperty as the backing store for DefaultButton.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DefaultButtonProperty =
            DependencyProperty.Register("DefaultButton", typeof(ContentDialogButton), typeof(LayoutDialog), new PropertyMetadata(ContentDialogButton.None, (d, e) => (d as LayoutDialog).SetBtnStyle()));

        private void SetBtnStyle()
        {
            switch (DefaultButton)
            {
                case ContentDialogButton.Primary:
                    VisualStateManager.GoToState(this, "PrimaryAsDefaultButton", false);
                    PrimaryButton?.Focus(FocusState.Pointer);
                    break;
                case ContentDialogButton.Secondary:
                    VisualStateManager.GoToState(this, "SecondaryAsDefaultButton", false);
                    SecondaryButton?.Focus(FocusState.Pointer);
                    break;
                case ContentDialogButton.Close:
                    VisualStateManager.GoToState(this, "CloseAsDefaultButton", false);
                    CloseButton?.Focus(FocusState.Pointer);
                    break;
                default:
                    VisualStateManager.GoToState(this, "NoDefaultButton", false);
                    break;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public object TitleExtension
        {
            get { return (object)GetValue(TitleExtensionProperty); }
            set { SetValue(TitleExtensionProperty, value); }
        }

        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty TitleExtensionProperty =
            DependencyProperty.Register("TitleExtension", typeof(object), typeof(LayoutDialog), new PropertyMetadata(null));
        #endregion

        //private bool _isDialogOpen = false;
        //private Promise<ContentDialogResult> _promise = null;

        /// <summary>
        /// 显示对话框，直到IsOpen = false
        /// </summary>
        /// <returns>返回Result，可在IsOpen = false之前手动设置 Result </returns>
        public async Task<ContentDialogResult> ShowAsync()
        {
            //return _promise.
            //_promise = new Promise<ContentDialogResult>();
            //IsOpen = true;

            //_isDialogOpen = true;
            IsOpen = true;
            _backButtonVisibility = SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility;
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Collapsed;

            while (IsOpen)
            {
                await TaskExtend.SleepAsync();
            }

            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = _backButtonVisibility;

            return Result;
        }

        private AppViewBackButtonVisibility _backButtonVisibility;

        /// <summary>
        /// 
        /// </summary>
        public LayoutDialog()
        {
            //if (Style == null)
            //{
            //    Style = ResourcesHelper.GetResource<Style>("LayoutDialog");
            //}

            this.DefaultStyleKey = typeof(LayoutDialog);

            //IsOpenChanged += OnIsOpenChanged;
            Loaded += LayoutDialog_Loaded;
        }

        private void LayoutDialog_Loaded(object sender, RoutedEventArgs e)
        {
            switch (DefaultButton)
            {
                case ContentDialogButton.Primary:
                    PrimaryButton?.Focus(FocusState.Programmatic);
                    break;
                case ContentDialogButton.Secondary:
                    SecondaryButton?.Focus(FocusState.Programmatic);
                    break;
                case ContentDialogButton.Close:
                    CloseButton?.Focus(FocusState.Programmatic);
                    break;
                default:
                    if (IsExitButtonVisible)
                    {
                        ExitButton?.Focus(FocusState.Programmatic);
                    }
                    else
                    {
                        this.Focus(FocusState.Programmatic);
                    }
                    break;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        protected Button PrimaryButton = null;
        /// <summary>
        /// 
        /// </summary>
        protected Button SecondaryButton = null;
        /// <summary>
        /// 
        /// </summary>
        protected Button CloseButton = null;
        /// <summary>
        /// 
        /// </summary>
        protected Button ExitButton = null;

        /// <summary>
        /// 
        /// </summary>
        public ContentDialogResult Result { get; set; } = ContentDialogResult.None;

        /// <summary>
        /// 应用模板时触发
        /// </summary>
        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            PrimaryButton = (Button)GetTemplateChild("PrimaryButton");
            SecondaryButton = (Button)GetTemplateChild("SecondaryButton");
            CloseButton = (Button)GetTemplateChild("CloseButton");
            ExitButton = (Button)GetTemplateChild("ExitButton");

            if (PrimaryButton == null)
            {
                throw new ArgumentException("模板设置不正确（找不到对应按钮）");
            }

            SetBtnEvent(PrimaryButton, PrimaryButtonClick, ContentDialogResult.Primary);
            SetBtnEvent(SecondaryButton, SecondaryButtonClick, ContentDialogResult.Secondary);
            SetBtnEvent(CloseButton, CloseButtonClick, ContentDialogResult.None);
            SetBtnEvent(ExitButton, CloseButtonClick, ContentDialogResult.None);

            SetBtnStyle();
            SetBtnVisible();
        }

        private void SetBtnEvent(Button button, TypedEventHandler<LayoutDialog, LayoutDialogButtonClickEventArgs> clickEvent, ContentDialogResult contentDialogResult)
        {
            button.Click += (ss, ee) =>
            {
                var args = new LayoutDialogButtonClickEventArgs();
                clickEvent?.Invoke(this, args);
                if (!args.Cancel)
                {
                    Result = contentDialogResult;
                    this.IsOpen = false;
                }
            };
        }

        /// <summary>
        /// 
        /// </summary>
        public event TypedEventHandler<LayoutDialog, LayoutDialogButtonClickEventArgs> PrimaryButtonClick;

        /// <summary>
        /// 
        /// </summary>

        public event TypedEventHandler<LayoutDialog, LayoutDialogButtonClickEventArgs> SecondaryButtonClick;

        /// <summary>
        /// 
        /// </summary>

        public event TypedEventHandler<LayoutDialog, LayoutDialogButtonClickEventArgs> CloseButtonClick;

        //public event TypedEventHandler<LayoutDialog, bool> DialogOpenChanged;

    }
}
