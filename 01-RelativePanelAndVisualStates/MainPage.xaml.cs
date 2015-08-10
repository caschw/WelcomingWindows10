using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Threading;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace App2
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        Timer timer;

        public MainPage()
        {
            this.InitializeComponent();
        }

        private void myButton_Click(object sender, RoutedEventArgs e)
        {
            myLabel.Text = "You pushed it real good!";
            myLabel.Foreground = new SolidColorBrush(Colors.Red);
            TimerCallback callback = resetLabel;
            timer = new Timer(callback, null, 2000, Timeout.Infinite);
        }

        private async void resetLabel(object stateInfo)
        {
            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, ()=>{
                myLabel.Text = "Push It!";
                myLabel.Foreground = new SolidColorBrush(Colors.Green);
                timer = null;
            });

           
        }
    }
}
