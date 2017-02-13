using System.Windows;
using WpfApplication1.ViewModels;

namespace WpfApplication1.Views
{
    /// <summary>
    /// Interaction logic for SpeechWindow.xaml
    /// </summary>
    public partial class SpeechWindow : Window
    {
        public SpeechWindow()
        {
            InitializeComponent();
            var vm = new SpeechViewModel();
            DataContext = vm;
        }
    }
}
