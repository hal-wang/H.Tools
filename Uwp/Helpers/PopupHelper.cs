using HTools.Uwp.Controls;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Threading.Tasks;
using Windows.Data.Xml.Dom;
using Windows.Services.Store;
using Windows.UI;
using Windows.UI.Notifications;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace HTools.Uwp.Helpers
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
        public async static Task<ContentDialogResult> ShowDialog(object content, string title, string primaryButtonText = null, string secondButtonText = null, bool? isPrimaryDefault = true, bool isExitButtonVisible = false, string closeButtonText = null, bool ahead = true)
        {
            ContentDialog dialog = new();
            if (primaryButtonText != null)
            {
                dialog.PrimaryButtonText = primaryButtonText;
            }
            if (primaryButtonText == null && secondButtonText == null && closeButtonText == null && !isExitButtonVisible)
            {
                isExitButtonVisible = true;
            }

            if (content is string str)
            {
                content = new TextBlock()
                {
                    Text = str,
                    TextWrapping = TextWrapping.Wrap
                };
            }
            var contentGrid = new Grid();
            contentGrid.Children.Add(content as UIElement);

            var titleGrid = new Grid()
            {
                Width = 200,
            };
            titleGrid.Children.Add(new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Center,
                Text = title ?? ""
            });
            if (isExitButtonVisible)
            {
                var exitButton = new Button()
                {
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Right,
                    Style = ResourcesHelper.GetResource<Style>("LightButtonStyle"),
                    Content = new SymbolIcon(Symbol.Cancel),
                    Foreground = new SolidColorBrush(Colors.Red)
                };
                titleGrid.Children.Add(exitButton);
                exitButton.Click += (ss, ee) =>
                {
                    dialog.Hide();
                };
            }
            contentGrid.SizeChanged += (ss, ee) =>
            {
                titleGrid.Width = ee.NewSize.Width;
            };


            dialog.Title = titleGrid;
            dialog.Content = contentGrid;

            if (string.IsNullOrEmpty(secondButtonText))
            {
                dialog.DefaultButton = ContentDialogButton.Primary;
            }
            else
            {
                dialog.SecondaryButtonText = secondButtonText;

                if (!string.IsNullOrEmpty(closeButtonText))
                {
                    dialog.CloseButtonText = closeButtonText;
                }

                switch (isPrimaryDefault)
                {
                    case null:
                        dialog.DefaultButton = ContentDialogButton.Close;
                        break;
                    case true:
                        dialog.DefaultButton = ContentDialogButton.Primary;
                        break;
                    case false:
                        dialog.DefaultButton = ContentDialogButton.Secondary;
                        break;
                }
            }

            return await dialog.QueueAsync(ahead);
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