using Hubery.Tools.Uwp.Controls.Message;
using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Windows.ApplicationModel.Core;
using Windows.UI.Xaml;

namespace Hubery.Tools.Uwp.Helpers
{
    public static class MessageHelper
    {
        private static readonly MessageList _message = new MessageList();
        public async static void Show(string content, MessageType messageType = MessageType.Primary, int duration = 3000)
        {
            try
            {
                await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
                {
                    _message.ShowMessage(content, messageType, duration);
                });
            }
            catch (Exception ex)
            {
                LogHelper.Instance.Log(ex);
            }
        }

        public static void ShowPrimary(string content, int duration = 3000)
        {
            Show(content, MessageType.Primary, duration);
        }

        public static void ShowInfo(string content, int duration = 3000)
        {
            Show(content, MessageType.Info, duration);
        }

        public static void ShowWarning(string content, int duration = 3000)
        {
            Show(content, MessageType.Warning, duration);
        }

        public static void ShowDanger(string content, int duration = 3000)
        {
            Show(content, MessageType.Danger, duration);
        }

        public static void ShowError(Exception ex, [CallerMemberName] string source = null)
        {
            try
            {
                Debug.WriteLine(ex);
                LogHelper.Instance.Log(ex, source);
                ShowDanger(ex.Message, 5000);
            }
            catch (Exception exc)
            {
                LogHelper.Instance.Log(exc, "ShowError，" + source);
            }
        }

        public static async void ShowToast(string str, int milliseconds = 2000)
        {
            await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                new NotifyPopup(str, milliseconds).Show();
            });
        }

        public static void ShowSticky(FrameworkElement target, string text, MessageType messageType = MessageType.Warning)
        {
            new StickyMessage().Show(target, text, messageType);
        }
    }
}
