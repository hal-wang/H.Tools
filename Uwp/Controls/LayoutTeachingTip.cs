using Microsoft.UI.Xaml.Controls;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace HTools.Uwp.Controls {
    internal class LayoutTeachingTip : PopupLayout {
        public LayoutTeachingTip(TeachingTip teachingTip) {
            DefaultStyleKey = typeof(LayoutTeachingTip);
            Content = teachingTip;
            Loaded += (ss, ee) => Focus(FocusState.Programmatic);
        }

        public double BackgroundOpacity {
            get { return (double)GetValue(BackgroundOpacityProperty); }
            set { SetValue(BackgroundOpacityProperty, value); }
        }

        // Using a DependencyProperty as the backing store for BackgroundOpacity.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BackgroundOpacityProperty =
            DependencyProperty.Register("BackgroundOpacity", typeof(double), typeof(LayoutTeachingTip), new PropertyMetadata(0.4));


        public bool Clickable {
            get { return (bool)GetValue(ClickableProperty); }
            set { SetValue(ClickableProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Clickable.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ClickableProperty =
            DependencyProperty.Register("Clickable", typeof(bool), typeof(LayoutTeachingTip), new PropertyMetadata(false));

        private TeachingTip TeachingTip => Content as TeachingTip;

        public async Task<TeachingTipClosedEventArgs> ShowAtAsync(FrameworkElement target) {
            TeachingTip.Target = target;
            IsOpen = true;
            TeachingTip.IsOpen = true;

            TaskCompletionSource<TeachingTipClosedEventArgs> showWaiter = new();
            TeachingTip.Closed += (ss, ee) => {
                showWaiter.SetResult(ee);
                IsOpen = false;
            };
            return await showWaiter.Task;
        }
    }
}
