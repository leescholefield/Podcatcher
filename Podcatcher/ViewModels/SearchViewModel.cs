using Podcatcher.Models;
using Podcatcher.ViewModels.Commands;
using Podcatcher.ViewModels.Services;
using System.Collections.Generic;
using System.Windows.Input;

namespace Podcatcher.ViewModels
{
    public class SearchViewModel : BaseViewModel
    {

        #region Properties

        public ICommand SearchCommand { get; set; }

        public ICommand DisplayPodcastCommand { get; set; }

        public string SearchTerm { get; set; }

        private List<Podcast> _results = new List<Podcast>();
        public List<Podcast> Results
        {
            get
            {
                return _results;
            }
            set
            {
                _results = value;
                OnPropertyChanged("Results");
            }
        }


        #endregion

        protected override void InitializeCommands()
        {
            SearchCommand = new RelayCommand<string>(SearchCommand_Execute);
            DisplayPodcastCommand = new RelayCommand<Podcast>(DisplayPodcastCommand_Execute);
        }

        private void SearchCommand_Execute(string term)
        {
            var ser = ServiceLocator.Instance.GetService<IItunesSearchService>();
            Results =  ser.Search(term);
        }

        private void DisplayPodcastCommand_Execute(Podcast podcast)
        {
            ServiceLocator.Instance.GetService<INavigationService>().NavigateTo<PodcastViewModel>(podcast);
        }

        private bool SearchCommand_CanExecute(string term)
        {
            return (term != null && term != "");
        }
    }
}
