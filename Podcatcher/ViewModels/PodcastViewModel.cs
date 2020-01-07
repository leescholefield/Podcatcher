using Podcatcher.Models;
using Podcatcher.ViewModels.Commands;
using System.Windows.Input;

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
                OnPropertyChanged("Podcast");
            }
        }

        public ICommand PlayCommand { get; set; }

        #endregion

        protected override void InitializeCommands()
        {
            PlayCommand = new RelayCommand<Episode>(PlayCommand_Execute);
        }

        private void PlayCommand_Execute(Episode episode)
        {

        }
    }
}
