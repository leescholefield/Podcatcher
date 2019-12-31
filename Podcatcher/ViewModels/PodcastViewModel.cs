using Podcatcher.Models;

namespace Podcatcher.ViewModels
{
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
                OnPropertyChanged("Podcast");
            }
        }

        #endregion
    }
}
