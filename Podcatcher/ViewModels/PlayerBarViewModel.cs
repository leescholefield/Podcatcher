using Podcatcher.Models.Playback;
using Podcatcher.ViewModels.Commands;
using Podcatcher.ViewModels.Services;
using System;
using System.Windows.Input;

namespace Podcatcher.ViewModels
{
    public class PlayerBarViewModel : BaseViewModel
    {

        #region Properties

        private IPlaybackService PlaybackService { get; set; }

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

        private static readonly int SKIP_DURATION_SECONDS = 10;

        public ICommand TogglePlayback { get; set; }

        public ICommand SkipForward { get; set; }

        public ICommand SkipBack { get; set; }

        #endregion

        #region Instantiation

        public PlayerBarViewModel() : this(ServiceLocator.Instance.GetService<IPlaybackService>())
        {
        }

        public PlayerBarViewModel(IPlaybackService playbackService)
        {
            PlaybackService = playbackService;
            PlaybackService.NowPlayingChanged += (s, a) =>
            {
                Playing = a.MediaItem;
            };
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
            PlaybackService.PlaybackStateChanged += (s, a) =>
            {
                switch(a.State)
                {
                    case PlaybackState.PLAYING:
                        IsPlaying = true;
                        break;
                    case PlaybackState.PAUSED:
                    case PlaybackState.STOPPED:
                        IsPlaying = false;
                        break;
                    default:
                        throw new ArgumentException("No case registered for the PlaybackState " + a.State);
                }
            };
        }

        #endregion

        private void TogglePlayback_Execute(object _)
        {
            if (IsPlaying)
                PlaybackService.Pause();
            else
                PlaybackService.Play();
        }

        private void SkipForward_Execute(object _)
        {
            PlaybackService.SkipForward(SKIP_DURATION_SECONDS);
        }

        private void SkipBack_Execute(object _)
        {
            PlaybackService.SkipBack(SKIP_DURATION_SECONDS);
        }
    }
}
