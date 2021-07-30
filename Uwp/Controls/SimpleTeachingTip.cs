using Microsoft.UI.Xaml.Controls;
using System;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace HTools.Uwp.Controls {
    internal class SimpleTeachingTip : PopupLayout {
        #region 依赖属性
        public FrameworkElement Target {
            get { return (FrameworkElement)GetValue(TargetProperty); }
            set { SetValue(TargetProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Target.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TargetProperty =
            DependencyProperty.Register("Target", typeof(FrameworkElement), typeof(SimpleTeachingTip), new PropertyMetadata(null));


        public TeachingTipPlacementMode PreferredPlacement {
            get { return (TeachingTipPlacementMode)GetValue(PreferredPlacementProperty); }
            set { SetValue(PreferredPlacementProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PreferredPlacement.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PreferredPlacementProperty =
            DependencyProperty.Register("PreferredPlacement", typeof(TeachingTipPlacementMode), typeof(SimpleTeachingTip), new PropertyMetadata(TeachingTipPlacementMode.Auto));


        public string Title {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Title.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(SimpleTeachingTip), new PropertyMetadata(string.Empty));


        public double BackgroundOpacity {
            get { return (double)GetValue(BackgroundOpacityProperty); }
            set { SetValue(BackgroundOpacityProperty, value); }
        }

        // Using a DependencyProperty as the backing store for BackgroundOpacity.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BackgroundOpacityProperty =
            DependencyProperty.Register("BackgroundOpacity", typeof(double), typeof(SimpleTeachingTip), new PropertyMetadata(0.4));


        public bool Clickable {
            get { return (bool)GetValue(ClickableProperty); }
            set { SetValue(ClickableProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Clickable.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ClickableProperty =
            DependencyProperty.Register("Clickable", typeof(bool), typeof(SimpleTeachingTip), new PropertyMetadata(false));
        #endregion

        public SimpleTeachingTip(string title = "", object content = null) {
            DefaultStyleKey = typeof(SimpleTeachingTip);
            Title = title;
            Content = content is string str
                ? new TextBlock() {
                    Text = str,
                    TextWrapping = TextWrapping.Wrap
                }
                : content;
            Loaded += (ss, ee) => Focus(FocusState.Programmatic);
        }

        protected TeachingTip TeachingTip = null;
        protected override void OnApplyTemplate() {
            base.OnApplyTemplate();

            TeachingTip = (TeachingTip)GetTemplateChild("TeachingTip");
            if (TeachingTip == null) {
                _openErr = true;
                throw new ArgumentException("模板设置不正确（找不到对应TeachingTip）");
            }
        }

        public async Task<TeachingTipClosedEventArgs> ShowAtAsync(FrameworkElement target = null) {
            if (target != null) {
                Target = target;
            }

            IsOpen = true;
            await Open();

            TaskCompletionSource<TeachingTipClosedEventArgs> showWaiter = new();
            TeachingTip.Closed += (ss, ee) => {
                showWaiter.SetResult(ee);
                IsOpen = false;
            };
            return await showWaiter.Task;
        }

        private bool _openErr = false;
        private async Task Open() {
            if (_openErr || (TeachingTip != null && TeachingTip.IsOpen)) {
                return;
            }

            if (TeachingTip == null) {
                await TaskExtend.SleepAsync();
                await Open();
            } else {
                TeachingTip.IsOpen = true;
            }
        }
    }
}
