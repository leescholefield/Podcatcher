using Podcatcher.Models.Playback;
using Podcatcher.ViewModels.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Podcatcher.ViewModels
{
    public class PlayerBarViewModel : BaseViewModel
    {

        private IPlayer MediaPlayer;

        private MediaItem _playing;
        public MediaItem Playing
        {
            get
            {
                return _playing;
            }
            set
            {
                _playing = value;
                OnPropertyChanged("Playing");
            }
        }

        public ICommand TogglePlayback { get; set; }

        public ICommand SkipForward { get; set; }

        public ICommand SkipBack { get; set; }

        public PlayerBarViewModel()
        {
            MediaPlayer = Player.Instance;
            // testing
            var item = new MediaItem
            {
                Episode = new Models.Episode
                {
                    StreamUrl = "http://traffic.libsyn.com/revolutionspodcast/10.26-_The_Far_East_Master.mp3?dest-id=159998"
                }
            };
            MediaPlayer.Load(item);
        }

        protected override void InitializeCommands()
        {
            TogglePlayback = new RelayCommand<object>(TogglePlayback_Execute);
        }

        private void TogglePlayback_Execute(object _)
        {
            MediaPlayer.Play();
        }
    }
}
