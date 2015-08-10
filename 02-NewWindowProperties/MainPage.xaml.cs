using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Core;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Core;
using System.Threading.Tasks;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace _02_NewWindowProperties
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        ApplicationView appView;
        public MainPage()
        {
            this.InitializeComponent();
            appView = ApplicationView.GetForCurrentView();
            
            appView.TryResizeView(new Size(400, 200));
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            appView.TryEnterFullScreenMode();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            appView.ExitFullScreenMode();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            appView.Title = string.IsNullOrEmpty(CustomTitleText.Text) ? string.Empty : CustomTitleText.Text ;
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            var titlebar = appView.TitleBar;
            titlebar.BackgroundColor = titlebar.BackgroundColor == Colors.LightYellow ? Colors.LightGray : Colors.LightYellow;
            titlebar.ForegroundColor = titlebar.ForegroundColor == Colors.Gray ? Colors.Yellow : Colors.Gray;
            
            titlebar.ButtonForegroundColor = titlebar.ButtonForegroundColor == Colors.LightYellow ? Colors.LightGray : Colors.LightYellow;
            titlebar.ButtonBackgroundColor = titlebar.ButtonBackgroundColor == Colors.Gray ? Colors.Yellow : Colors.Gray;

            titlebar.ButtonHoverBackgroundColor = titlebar.ButtonHoverBackgroundColor == Colors.Lavender ? Colors.Coral : Colors.Lavender;
            titlebar.ButtonHoverForegroundColor = titlebar.ButtonHoverForegroundColor == Colors.Orange ? Colors.Purple : Colors.Orange;

            titlebar.ButtonPressedBackgroundColor = titlebar.ButtonPressedBackgroundColor == Colors.Orange ? Colors.Purple : Colors.Orange;
            titlebar.ButtonPressedForegroundColor = titlebar.ButtonPressedForegroundColor == Colors.Purple ? Colors.Orange : Colors.Purple;
        }

        private async void Button_Click_4(object sender, RoutedEventArgs e)
        {
            await CreateNewWindow();


        }

        public static async Task CreateNewWindow()
        {
            var newCoreAppView = CoreApplication.CreateNewView();
            var appView = ApplicationView.GetForCurrentView();
            await newCoreAppView.Dispatcher.RunAsync(CoreDispatcherPriority.Low, async () =>
            {
                var window = Window.Current;
                var newAppView = ApplicationView.GetForCurrentView();

#if WINDOWS_UAP
                newAppView.SetPreferredMinSize(new Size(400, 300));
#endif
                var frame = new Frame();
                window.Content = frame;
                frame.Navigate(typeof(PopupPage));
                window.Activate();

                await ApplicationViewSwitcher.TryShowAsStandaloneAsync(newAppView.Id, ViewSizePreference.UseMore, appView.Id, ViewSizePreference.Default);

#if WINDOWS_UAP
                var success = newAppView.TryResizeView(new Size(400, 400));
#endif
            });
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            var daSize = appView.VisibleBounds.Height == 500 ? new Size(500, 600) : new Size(600, 500);
            var success = ApplicationView.GetForCurrentView().TryResizeView(daSize);
        }
    }
}
