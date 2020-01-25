using System;
using Podcatcher.Models;
using Podcatcher.Models.Playback;

namespace Podcatcher.ViewModels.Services
{
    public class PlaybackService : IPlaybackService
    {

        #region Properties

        private IPlayer Player { get; set; }
        private MediaItem NowPlaying;

        public event EventHandler<MediaItemChangedEventArgs> NowPlayingChanged;
        public event EventHandler<PlaybackStateChangedEventArgs> PlaybackStateChanged;

        #endregion

        #region Instantiation

        public PlaybackService() : this(Models.Playback.Player.Instance) { }

        public PlaybackService(IPlayer player)
        {
            Player = player;
            RegisterEventCallbacks();
        }

        private void RegisterEventCallbacks()
        {
            Player.PlaybackPaused += (s, a) => {
                PlaybackStateChanged?.Invoke(this, new PlaybackStateChangedEventArgs(PlaybackState.PAUSED));
            };
            Player.PlaybackStarted += (s, a) => {
                PlaybackStateChanged?.Invoke(this, new PlaybackStateChangedEventArgs(PlaybackState.PLAYING));
            };
            Player.PlaybackStopped += (s, a) =>
            {
                PlaybackStateChanged?.Invoke(this, new PlaybackStateChangedEventArgs(PlaybackState.STOPPED));
            };
        }

        #endregion

        #region IPlaybackService Implementation

        public void Load(Episode episode, bool beginPlaybackOnLoaded = true)
        {
            if (episode.StreamUrl == null)
                throw new ArgumentException("Epsiode has a null StreamUrl");

            NowPlaying = new MediaItem
            {
                Episode = episode
            };

            Player.Load(NowPlaying);
            NowPlayingChanged?.Invoke(this, new MediaItemChangedEventArgs(NowPlaying));

            if (beginPlaybackOnLoaded)
                Player.Play();
        }

        public void Pause()
        {
            if (NowPlaying != null)
                Player.Pause();
        }

        public void Play()
        {
            if (NowPlaying != null)
                Player.Play();
        }

        public void SkipBack(int durationSeconds)
        {
            if (NowPlaying != null)
                Player.SkipBack(durationSeconds);
        }

        public void SkipForward(int durationSeconds)
        {
            if (NowPlaying != null)
                Player.SkipForward(durationSeconds);
        }

        #endregion
    }
}
