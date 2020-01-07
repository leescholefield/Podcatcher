using System.Collections.Generic;
using System.Windows.Input;
using Podcatcher.Models;
using Podcatcher.ViewModels.Commands;

namespace Podcatcher.ViewModels
{
    /// <summary>
    /// ViewModel for displaying a list of <see cref="Podcast"/>
    /// </summary>
    public class PodcastListViewModel : BaseViewModel
    {

        #region Properties

        public List<Podcast> _podcasts;
        public List<Podcast> Podcasts
        {
            get
            {
                return _podcasts;
            }
            set
            {
                _podcasts = value;
                OnPropertyChanged("Podcasts");
            }
        }

        /// <summary>
        /// Opens a <see cref="PodcastViewModel"/> with the passed Podcast.
        /// </summary>
        public ICommand DisplayPodcastCommand { get; set; }

        /// <summary>
        /// Adds the given Podcast to list of Subscriptions />
        /// </summary>
        public ICommand SubscribeToPodcast { get; set; }

        #endregion

        protected override void InitializeCommands()
        {
            DisplayPodcastCommand = new RelayCommand<Podcast>(DisplayPodcastCommand_Execute);
            SubscribeToPodcast = new SubscribeCommand();
        }

        private void DisplayPodcastCommand_Execute(Podcast podcast)
        {

        }

    }
}
