using Podcatcher.Models;
using Podcatcher.ViewModels.Commands;
using Podcatcher.ViewModels.Services;
using System.Windows.Input;
using System;

namespace Podcatcher.ViewModels
{
    /// <summary>
    /// ViewModel for displaying a single <see cref="Podcast"/> object. 
    /// </summary>
    public class PodcastViewModel : BaseViewModel
    {

        #region Properties

        private Podcast _podcast;
        public Podcast Podcast
        {
            get
            {
                return _podcast;
            }
            set
            {
                _podcast = value;
                if (value.Episodes.Count == 0)
                    DeserializeEpisodes();
                OnPropertyChanged("Podcast");
            }
        }

        public ICommand PlayCommand { get; set; }

        public ICommand SubscribeCommand { get; set; }

        #endregion

        #region Initialization


        protected override void InitializeCommands()
        {
            PlayCommand = new RelayCommand<Episode>(PlayCommand_Execute);
            SubscribeCommand = new SubscribeCommand();
        }

        #endregion

        private void PlayCommand_Execute(Episode episode)
        {
            var service = ServiceLocator.Instance.GetService<IPlaybackService>();
            service.Load(episode);
        }

        private void DeserializeEpisodes()
        {
            if (Podcast.FeedUrl == null || Podcast.FeedUrl.Equals(""))
            {
                throw new InvalidOperationException("Podcast does not a a FeedUrl set");
            }

            var eps = ServiceLocator.Instance.GetService<IRssDeserializer>().DeserializeEpisodes(Podcast.FeedUrl);
            Podcast.Episodes = eps;
        }
    }
}
