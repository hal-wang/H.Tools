using HTools.Uwp.Controls.Message;
using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Windows.ApplicationModel.Core;
using Windows.UI.Xaml;

namespace HTools.Uwp.Helpers {
    /// <summary>
    /// 
    /// </summary>
    public static class MessageHelper {
        private static readonly MessageList _message = new();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="content"></param>
        /// <param name="messageType"></param>
        /// <param name="duration"></param>
        public static async void Show(string content, MessageType messageType = MessageType.Primary, int duration = 3000) {
            try {
                await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () => {
                    _message.ShowMessage(content, messageType, duration);
                });
            } catch (Exception ex) {
                Debug.WriteLine(ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="content"></param>
        /// <param name="duration"></param>
        public static void ShowPrimary(string content, int duration = 3000) {
            Show(content, MessageType.Primary, duration);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="content"></param>
        /// <param name="duration"></param>
        public static void ShowInfo(string content, int duration = 3000) {
            Show(content, MessageType.Info, duration);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="content"></param>
        /// <param name="duration"></param>
        public static void ShowWarning(string content, int duration = 3000) {
            Show(content, MessageType.Warning, duration);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="content"></param>
        /// <param name="duration"></param>
        public static void ShowDanger(string content, int duration = 3000) {
            Show(content, MessageType.Danger, duration);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="duration"></param>
        /// <param name="source"></param>
        public static void ShowError(Exception ex, int duration = 5000, [CallerMemberName] string source = null) {
            try {
                Debug.WriteLine(source);
                Debug.WriteLine(ex);
                ShowDanger(ex.Message, duration);
            } catch (Exception exc) {
                Debug.WriteLine(exc);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <param name="milliseconds"></param>
        public static async void ShowToast(string str, int milliseconds = 2000) {
            await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () => {
                new NotifyPopup(str, milliseconds).Show();
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="target"></param>
        /// <param name="text"></param>
        /// <param name="messageType"></param>
        public static void ShowSticky(FrameworkElement target, string text, MessageType messageType = MessageType.Warning) {
            new StickyMessage().Show(target, text, messageType);
        }
    }
}
