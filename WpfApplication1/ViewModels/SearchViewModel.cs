using System.Windows.Input;
using PropertyChanged;
using WpfApplication1.Engine;
using WpfApplication1.Plumbing;

namespace WpfApplication1.ViewModels
{
    [ImplementPropertyChanged]
    class SearchViewModel
    {
        public SearchViewModel()
        {
            SuggestCommand = new RelayCommand(p=>true, Suggest);
        }

        public async void Suggest(object arg)
        {
            var result = await Api.GetAutosuggestResult(SearchPhrase);
            AnalysisResult = JsonHelper.FormatJson(await result.Content.ReadAsStringAsync());
        }

        public string AnalysisResult { get; set; }
        public ICommand SuggestCommand { get; set; }
        public string SearchPhrase { get; set; }

    }
}
