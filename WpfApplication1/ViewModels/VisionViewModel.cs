using System;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using PropertyChanged;
using WpfApplication1.Engine;
using WpfApplication1.Plumbing;
using WPFMediaKit.DirectShow.Controls;
using Microsoft.Win32;

namespace WpfApplication1.ViewModels
{
    [ImplementPropertyChanged]
    class VisionViewModel
    {
        public VisionViewModel()
        {
            AnalyzeCommand = new RelayCommand(p=>true, Analyze);
            CaptureCommand = new RelayCommand(p=>true, Capture);
            LoadImageCommand = new RelayCommand(p=>true, LoadImage);

            VideoVisible = true;
        }

        private void LoadImage(object obj)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() ?? false)
            {
                ImageSource = new BitmapImage(new Uri(ofd.FileName));
                CapturedData = ImageToBytes(ImageSource);
                VideoVisible = false;
            }
            AnalisysResult = "Loaded";
        }

        private async void Analyze(object obj)
        {
            if (CapturedData == null || CapturedData.Length == 0)
            {
                AnalisysResult = "Missing captured data";
                return;
            }
            AnalisysResult = "Analyzing...";
            //analyze it
            var analysis = Api.RequestImageAnalisys(CapturedData);
            //display information
            var result = await analysis;
            AnalisysResult = JsonHelper.FormatJson(await result.Content.ReadAsStringAsync());
        }

        private void Capture(object obj)
        {
            var videoImageProperty = typeof(VideoCaptureElement).GetProperty("VideoImage", BindingFlags.NonPublic | BindingFlags.Instance);
            var value = (Image)videoImageProperty.GetValue(VideoCapture);
            CapturedData = ImageToBytes(value.Source);
            AnalisysResult = "Captured";
        }

        public ImageSource ImageSource { get; set; }

        public static byte[] ImageToBytes(ImageSource imageSource)
        {
            var bitmapSource = imageSource as BitmapSource;
            if (bitmapSource == null)
            {
                var width = (int)imageSource.Width;
                var height = (int)imageSource.Height;
                var dv = new DrawingVisual();
                using (var dc = dv.RenderOpen())
                {
                    dc.DrawImage(imageSource, new Rect(0, 0, width, height));
                }
                var rtb = new RenderTargetBitmap(width, height, 96, 96, PixelFormats.Pbgra32);
                rtb.Render(dv);
                bitmapSource = BitmapFrame.Create(rtb);
            }

            byte[] data;
            var encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(bitmapSource));
            using (var ms = new MemoryStream())
            {
                encoder.Save(ms);
                data = ms.ToArray();
            }
            return data;
        }

        public bool VideoVisible { get; set; }
        public bool ImageVisible { get; set; }

        private byte[] CapturedData { get; set; }
        public VideoCaptureElement VideoCapture { get; set; }
        public string AnalisysResult { get; set; }

        public ICommand AnalyzeCommand { get; set; }

        public ICommand CaptureCommand { get; set; }

        public ICommand LoadImageCommand { get; set; }
    }
}
