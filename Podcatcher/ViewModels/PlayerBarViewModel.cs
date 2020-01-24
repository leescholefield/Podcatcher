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

        private bool _isPlaying = false;
        public bool IsPlaying
        {
            get
            {
                return _isPlaying;
            }
            set
            {
                _isPlaying = value;
                OnPropertyChanged("IsPlaying");
            }
        }

        private static int SKIP_DURATION_SECONDS = 10;

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

            RegisterMediaPlayerEvents();
        }

        protected override void InitializeCommands()
        {
            TogglePlayback = new RelayCommand<object>(TogglePlayback_Execute);
            SkipBack = new RelayCommand<object>(SkipBack_Execute);
            SkipForward = new RelayCommand<object>(SkipForward_Execute);
        }

        private void RegisterMediaPlayerEvents()
        {
            MediaPlayer.PlaybackStarted += (s, a) =>
            {
                IsPlaying = true;
            };
            MediaPlayer.PlaybackPaused += (s, a) =>
            {
                IsPlaying = false;
            };
            MediaPlayer.PlaybackStopped += (s, a) =>
            {
                IsPlaying = false;
            };
        }

        private void TogglePlayback_Execute(object _)
        {
            if (IsPlaying)
                MediaPlayer.Pause();
            else
                MediaPlayer.Play();
        }

        private void SkipForward_Execute(object _)
        {
            MediaPlayer.SkipForward(SKIP_DURATION_SECONDS);
        }

        private void SkipBack_Execute(object _)
        {
            MediaPlayer.SkipBack(SKIP_DURATION_SECONDS);
        }
    }
}
