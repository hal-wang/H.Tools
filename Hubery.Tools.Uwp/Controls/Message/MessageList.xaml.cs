using System.Collections.ObjectModel;
using Windows.UI.Xaml;

namespace Hubery.Tools.Uwp.Controls.Message
{
    internal sealed partial class MessageList : PopupLayout
    {
        public MessageList()
        {
            this.InitializeComponent();

            Messages.CollectionChanged += Messages_CollectionChanged;
        }



        public HorizontalAlignment MessageHorizontalAlignment
        {
            get { return (HorizontalAlignment)GetValue(MessageHorizontalAlignmentProperty); }
            set { SetValue(MessageHorizontalAlignmentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MessageHorizontalAlignment.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MessageHorizontalAlignmentProperty =
            DependencyProperty.Register("MessageHorizontalAlignment", typeof(HorizontalAlignment), typeof(MessageList), new PropertyMetadata(HorizontalAlignment.Center));



        public VerticalAlignment MessageVerticalAlignment
        {
            get { return (VerticalAlignment)GetValue(MessageVerticalAlignmentProperty); }
            set { SetValue(MessageVerticalAlignmentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MessageVerticalAlignment.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MessageVerticalAlignmentProperty =
            DependencyProperty.Register("MessageVerticalAlignment", typeof(VerticalAlignment), typeof(MessageList), new PropertyMetadata(VerticalAlignment.Top));




        public Thickness ContainerMargin
        {
            get { return (Thickness)GetValue(ContainerMarginProperty); }
            set { SetValue(ContainerMarginProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ContainerMargin.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ContainerMarginProperty =
            DependencyProperty.Register("ContainerMargin", typeof(Thickness), typeof(MessageList), new PropertyMetadata(new Thickness(0, 10, 0, 0)));



        public double MessageWidth
        {
            get { return (double)GetValue(MessageWidthProperty); }
            set { SetValue(MessageWidthProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MessageWidth.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MessageWidthProperty =
            DependencyProperty.Register("MessageWidth", typeof(double), typeof(MessageList), new PropertyMetadata(300.0));



        public Thickness MessageCornerRadius
        {
            get { return (Thickness)GetValue(MessageCornerRadiusProperty); }
            set { SetValue(MessageCornerRadiusProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MessageCornerRadius.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MessageCornerRadiusProperty =
            DependencyProperty.Register("MessageCornerRadius", typeof(Thickness), typeof(MessageList), new PropertyMetadata(new Thickness(3)));




        private void Messages_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (Messages.Count > 0)
            {
                IsOpen = true;
            }
            else
            {
                IsOpen = false;
            }
        }

        public ObservableCollection<MessageItem> Messages { get; } = new ObservableCollection<MessageItem>();

        public void ShowMessage(string content, MessageType messageType, int duration)
        {
            var item = new MessageItem(content, messageType, duration);
            Messages.Add(item);

            if (duration > 0)
            {
                DispatcherTimer timer = new DispatcherTimer()
                {
                    Interval = item.Duration
                };
                timer.Tick += (ss, ee) =>
                {
                    if (Messages.Contains(item))
                    {
                        Messages.Remove(item);
                    }
                    timer.Stop();
                };
                timer.Start();
            }
        }

        private void Hide_Click(object sender, RoutedEventArgs e)
        {
            MessageItem item = (sender as FrameworkElement)?.DataContext as MessageItem;
            if (item == null)
            {
                return;
            }

            if (Messages.Contains(item))
            {
                Messages.Remove(item);
            }
        }
    }
}
