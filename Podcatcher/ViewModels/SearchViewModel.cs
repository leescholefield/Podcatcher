using Podcatcher.Models;
using Podcatcher.ViewModels.Commands;
using Podcatcher.ViewModels.Services;
using System.Collections.Generic;
using System.Windows.Input;

namespace Podcatcher.ViewModels
{
    /// <summary>
    /// ViewModel for displaying an Itunes Search query and its response.
    /// </summary>
    public class SearchViewModel : BaseViewModel
    {

        #region Properties

        public ICommand SearchCommand { get; set; }

        public ICommand DisplayPodcastCommand { get; set; }

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

        private bool _displayNoResultsFoundMessage = false;
        public bool DisplayNoResultsFoundMessage
        {
            get
            {
                return _displayNoResultsFoundMessage;
            }
            set
            {
                _displayNoResultsFoundMessage = value;
                OnPropertyChanged("DisplayNoResultsFoundMessage");
            }
        }


        #endregion

        protected override void InitializeCommands()
        {
            SearchCommand = new RelayCommand<string>(SearchCommand_Execute, SearchCommand_CanExecute);
            DisplayPodcastCommand = new RelayCommand<Podcast>(DisplayPodcastCommand_Execute);
        }

        private void SearchCommand_Execute(string term)
        {
            var ser = ServiceLocator.Instance.GetService<IItunesSearchService>();
            Results =  ser.Search(term);

            if (Results.Count == 0)
                DisplayNoResultsFoundMessage = true;
            else
                DisplayNoResultsFoundMessage = false;
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
