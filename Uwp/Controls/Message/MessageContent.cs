using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace HTools.Uwp.Controls.Message
{
    /// <summary>
    /// 
    /// </summary>
    public partial class MessageContent : Control, IMessage
    {
        /// <summary>
        /// 
        /// </summary>
        public MessageContent()
        {
            this.DefaultStyleKey = typeof(MessageContent);
            DefaultStyleResourceUri = new System.Uri("ms-appx:///HTools/Themes/uap_generic.xaml");
        }

        /// <summary>
        /// 
        /// </summary>
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(MessageContent), new PropertyMetadata(string.Empty));


        /// <summary>
        /// 
        /// </summary>
        public MessageType MessageType
        {
            get { return (MessageType)GetValue(MessageTypeProperty); }
            set { SetValue(MessageTypeProperty, value); }
        }

        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty MessageTypeProperty =
            DependencyProperty.Register("MessageType", typeof(MessageType), typeof(MessageContent), new PropertyMetadata(MessageType.Info));


        /// <summary>
        /// 
        /// </summary>
        public double BackgroundOpacity
        {
            get { return (double)GetValue(BackgroundOpacityProperty); }
            set { SetValue(BackgroundOpacityProperty, value); }
        }

        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty BackgroundOpacityProperty =
            DependencyProperty.Register("BackgroundOpacity", typeof(double), typeof(MessageContent), new PropertyMetadata(0.8));


        /// <summary>
        /// 
        /// </summary>
        public double ForegroundOpacity
        {
            get { return (double)GetValue(ForegroundOpacityProperty); }
            set { SetValue(ForegroundOpacityProperty, value); }
        }

        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty ForegroundOpacityProperty =
            DependencyProperty.Register("ForegroundOpacity", typeof(double), typeof(MessageContent), new PropertyMetadata(0.9));


        /// <summary>
        /// 
        /// </summary>
        public Thickness TextMargin
        {
            get { return (Thickness)GetValue(TextMarginProperty); }
            set { SetValue(TextMarginProperty, value); }
        }

        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty TextMarginProperty =
            DependencyProperty.Register("TextMargin", typeof(Thickness), typeof(MessageContent), new PropertyMetadata(new Thickness(22, 14, 22, 14)));
    }

}
