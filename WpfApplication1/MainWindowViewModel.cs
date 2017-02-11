using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfApplication1.Plumbing;

namespace WpfApplication1
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
            
        }
    }
}
