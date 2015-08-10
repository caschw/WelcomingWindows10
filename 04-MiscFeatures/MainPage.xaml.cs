using System;
using System.Linq;
using Windows.ApplicationModel.DataTransfer;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Media.Imaging;
using Windows.Storage;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace _04_MiscFeatures
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainViewModel Vm => (MainViewModel)DataContext;

        public MainPage()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
        }

        private void Copy_Click(object sender, RoutedEventArgs e)
        {
            var data = new DataPackage();
            data.SetText(FirstTextBox.Text);
            Clipboard.SetContent(data);
        }

        private void Paste_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SecondTextBox.Text = Clipboard.GetContent().GetTextAsync().GetResults();
            }
            catch (Exception ex)
            {
                var t = ex;
            }
        }

        private async void PasteImage_Click(object sender, RoutedEventArgs e)
        {
            try {
                var data = Clipboard.GetContent();
                var results = data.GetBitmapAsync().GetResults();
                var stream = await results.OpenReadAsync();
                var bi = new BitmapImage();
                bi.SetSource(stream);

                SecondImage.Source = bi;
            }
            catch (Exception ex)
            {
                var t = ex;
            }
        }

        private void CopyImage_Click(object sender, RoutedEventArgs e)
        {
            var data = new DataPackage();
            var bi = FirstImage.Source as BitmapImage;
            var uri = bi.UriSource;
            data.SetBitmap(RandomAccessStreamReference.CreateFromUri(uri));
            Clipboard.SetContent(data);
        }

        private void ClearImage_Click(object sender, RoutedEventArgs e)
        {
            SecondImage.Source = null;
        }

        private async void Grid_Drop(object sender, DragEventArgs e)
        {
            if (e.DataView.Contains(StandardDataFormats.StorageItems))
            {
                var items = await e.DataView.GetStorageItemsAsync();

                if (items.Any())
                {
                    var storageFile = items[0] as StorageFile;
                    var bitmapImage = new BitmapImage();

                    bitmapImage.SetSource(await storageFile.OpenAsync(FileAccessMode.Read));

                    dropImage.Source = bitmapImage;
                }
            }
        }

        private void Grid_DragOver(object sender, DragEventArgs e)
        {
            e.AcceptedOperation = DataPackageOperation.Copy;
        }
    }
}
