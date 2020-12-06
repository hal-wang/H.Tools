using Hubery.Tools.Uwp.Controls;
using Hubery.Tools.Uwp.Controls.Dialog;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Threading.Tasks;
using Windows.Data.Xml.Dom;
using Windows.Services.Store;
using Windows.UI.Notifications;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Hubery.Tools.Uwp.Helpers
{
    /// <summary>
    /// 
    /// </summary>
    public static class PopupHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="target"></param>
        /// <param name="title"></param>
        /// <param name="content"></param>
        /// <param name="placement"></param>
        /// <param name="clickable"></param>
        /// <param name="backgroundOpacity"></param>
        /// <returns></returns>
        public async static Task<TeachingTipClosedEventArgs> ShowTeachingTip(FrameworkElement target, string title, object content = null, TeachingTipPlacementMode placement = TeachingTipPlacementMode.Auto, bool clickable = false, double backgroundOpacity = 0.4)
        {
            return await new LayoutTeachingTip(title, content)
            {
                PreferredPlacement = placement,
                Clickable = clickable,
                BackgroundOpacity = backgroundOpacity
            }.ShowAt(target);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="content"></param>
        /// <param name="title"></param>
        /// <param name="primaryButtonText"></param>
        /// <param name="secondButtonText"></param>
        /// <param name="isPrimaryDefault"></param>
        /// <param name="closeButtonText"></param>
        /// <returns></returns>
        public async static Task<ContentDialogResult> ShowDialog(string content, string title = "提示", string primaryButtonText = "确定", string secondButtonText = null, bool? isPrimaryDefault = true, string closeButtonText = null)
        {
            LayoutDialog layoutDialog = new LayoutDialog()
            {
                Title = title,
                Content = content,
                PrimaryButtonText = primaryButtonText
            };

            if (string.IsNullOrEmpty(secondButtonText))
            {
                layoutDialog.DefaultButton = ContentDialogButton.Primary;
            }
            else
            {
                layoutDialog.SecondaryButtonText = secondButtonText;

                if (!string.IsNullOrEmpty(closeButtonText))
                {
                    layoutDialog.CloseButtonText = closeButtonText;
                }

                switch (isPrimaryDefault)
                {
                    case null:
                        layoutDialog.DefaultButton = ContentDialogButton.Close;
                        break;
                    case true:
                        layoutDialog.DefaultButton = ContentDialogButton.Primary;
                        break;
                    case false:
                        layoutDialog.DefaultButton = ContentDialogButton.Secondary;
                        break;
                }
            }

            return await layoutDialog.ShowAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async static Task ShowRating() => await StoreRequestHelper.SendRequestAsync(StoreContext.GetDefault(), 16, string.Empty);

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <returns></returns>
        //public async static Task ShowFeedback() => await StoreServicesFeedbackLauncher.GetDefault().LaunchAsync();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="str1"></param>
        /// <param name="str2"></param>
        /// <param name="str3"></param>
        public static void ShowHint(string str1, string str2, string str3)
        {
            ToastTemplateType toastTemplate = ToastTemplateType.ToastImageAndText04;
            XmlDocument toastXml = ToastNotificationManager.GetTemplateContent(toastTemplate);

            XmlNodeList toastTextElements = toastXml.GetElementsByTagName("text");
            toastTextElements[0].AppendChild(toastXml.CreateTextNode(str1));
            toastTextElements[1].AppendChild(toastXml.CreateTextNode(str2));
            toastTextElements[2].AppendChild(toastXml.CreateTextNode(str3));

            XmlNodeList toastImageAttributes = toastXml.GetElementsByTagName("image");
            ((XmlElement)toastImageAttributes[0]).SetAttribute("src", $"ms-appx:///Assets/StoreLogo.scale-400.png");

            IXmlNode toastNode = toastXml.SelectSingleNode("/toast");
            ((XmlElement)toastNode).SetAttribute("duration", "long");

            XmlElement audio = toastXml.CreateElement("audio");
            audio.SetAttribute("src", $"ms-winsoundevent:Notification.Default");
            toastNode.AppendChild(audio);

            ToastNotification toast = new ToastNotification(toastXml);
            ToastNotificationManager.CreateToastNotifier().Show(toast);
        }
    }
}