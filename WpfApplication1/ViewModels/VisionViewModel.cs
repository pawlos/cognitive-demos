using System.Windows.Input;
using WpfApplication1.Plumbing;

namespace WpfApplication1.ViewModels
{
    class VisionViewModel
    {
        public VisionViewModel()
        {
            AnalyzeCommand = new RelayCommand(p=>true, Analyze);
        }

        private void Analyze(object obj)
        {
            //get a frame
            //analyze it
            //display information
        }

        public ICommand AnalyzeCommand { get; set; }
    }
}
