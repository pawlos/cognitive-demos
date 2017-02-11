using System.Windows.Input;
using WpfApplication1.Plumbing;
using WpfApplication1.Views;

namespace WpfApplication1.ViewModels
{
    class MainWindowViewModel
    {
        public MainWindowViewModel()
        {
            ExecuteView = new RelayCommand(p => true, Execute);
        }
        public ICommand ExecuteView { get; set; }

        private void Execute(object parameter)
        {
            if ((string) parameter == "Vision")
            {
                var window = new VisionWindow();
                window.ShowDialog();
            }
            else if ((string) parameter == "Speech")
            {
                
            }
        }
    }
}
