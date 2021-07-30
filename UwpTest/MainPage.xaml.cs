using HTools;
using HTools.Uwp.Helpers;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using UwpTest.Models;
using UwpTest.Views;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace UwpTest
{
    public sealed partial class MainPage : Page
    {
        public ObservableCollection<PageMenuItem> Pages
        {
            get { return (ObservableCollection<PageMenuItem>)GetValue(PagesProperty); }
            set { SetValue(PagesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Pages.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PagesProperty =
            DependencyProperty.Register("Pages", typeof(ObservableCollection<PageMenuItem>), typeof(MainPage), new PropertyMetadata(null));


        public MainPage()
        {
            this.InitializeComponent();

            InitPages();
            Loaded += MainPage_Loaded;
        }

        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            contentFrame.Navigate(typeof(HomePage));
        }

        private void InitPages()
        {
            Pages = new ObservableCollection<PageMenuItem>
            {
                new PageMenuItem()
                {
                    Name = nameof(HomePage),
                    PageType = typeof(HomePage),
                    Glyph = Symbol.Home
                },
                new PageMenuItem()
                {
                    Name = nameof(ColorSelecterPage),
                    PageType = typeof(ColorSelecterPage),
                    Glyph = Symbol.FontColor
                },
                new PageMenuItem()
                {
                    Name = nameof(DialogPage),
                    PageType = typeof(DialogPage),
                    Glyph = Symbol.NewWindow
                },
                new PageMenuItem()
                {
                    Name = nameof(ThemePage),
                    PageType = typeof(ThemePage),
                    Glyph = Symbol.FontColor
                },
                new PageMenuItem()
                {
                    Name = nameof(SettingPage),
                    PageType = typeof(SettingPage),
                    Glyph = Symbol.Setting
                },
                new PageMenuItem()
                {
                    Name = nameof(TeachingTipPage),
                    PageType = typeof(TeachingTipPage),
                    Glyph = Symbol.SetTile
                }
            };
        }

        private void NavigationView_ItemInvoked(Microsoft.UI.Xaml.Controls.NavigationView sender, Microsoft.UI.Xaml.Controls.NavigationViewItemInvokedEventArgs args)
        {
            if (args.InvokedItem is not string name) return;

            var pageType = Pages.Where(p => p.Name == name).Select(p => p.PageType).FirstOrDefault();
            if (pageType == default) return;
            if (contentFrame.Content != null && pageType == contentFrame.Content.GetType()) return;

            contentFrame.Navigate(pageType);
        }
    }
}
