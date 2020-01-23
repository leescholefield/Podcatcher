using System;
using System.Windows.Media;

namespace Podcatcher.Models.Playback
{
    public class Player : IPlayer
    {

        #region Properties

        private MediaPlayer MediaPlayer;

        private MediaItem CurrentlyPlaying;

        public event EventHandler<PlayerStateChangedEventArgs> PlaybackStarted;
        public event EventHandler<PlayerStateChangedEventArgs> PlaybackPaused;
        public event EventHandler<PlayerStateChangedEventArgs> PlaybackStopped;
        public event EventHandler<PlayerStateChangedEventArgs> PlaybackPositionUpdated;

        #endregion

        #region Instantiation

        private static Player _instance;

        public static Player Instance {
            get
            {
                if (_instance == null)
                {
                    _instance = new Player();
                }
                return _instance;
            }
        }

        /// <summary>
        /// Private constructor to prevent instantiation. Use <see cref="Instance"/> to obtain a reference.
        /// </summary>
        private Player()
        {

        }

        #endregion

        #region IPlayer Implementation

        public void Load(MediaItem item)
        {
            if (item.Episode.StreamUrl == null || item.Episode.StreamUrl == "")
                throw new ArgumentException("MediaItem Episode does not have a StreamUrl set");

            CurrentlyPlaying = item;

            Uri uri = new Uri(item.Episode.StreamUrl);
            MediaPlayer = new MediaPlayer();
            MediaPlayer.Open(uri);
        }

        public void Pause()
        {
            if (MediaPlayer == null)
                throw new InvalidOperationException("Must call Load on the MediaPlayer before attempting to pause");

            if (MediaPlayer.CanPause)
                MediaPlayer.Pause();
            PlaybackPaused?.Invoke(this, new PlayerStateChangedEventArgs(PlayerState.Paused));
        }

        public void Play()
        {
            if (MediaPlayer == null)
                throw new InvalidOperationException("Must call Load on the MediaPlayer before attempting to play");

            MediaPlayer.Play();
            PlaybackStarted?.Invoke(this, new PlayerStateChangedEventArgs(PlayerState.Playing));
        }

        public void SkipBack(int seconds)
        {
            if (MediaPlayer == null)
                throw new InvalidOperationException("Must call Load on the MediaPlayer before attempting to skip");

            var newPos = MediaPlayer.Position.Add(new TimeSpan(0, 0, 0, seconds, 0));
            MediaPlayer.Position = newPos;
            PlaybackPositionUpdated?.Invoke(this, new PlayerStateChangedEventArgs(PlayerState.Playing, newPos));
        }

        public void SkipForward(int seconds)
        {
            if (MediaPlayer == null)
                throw new InvalidOperationException("Must call Load on the MediaPlayer before attempting to skip");

            var newPos = MediaPlayer.Position.Subtract(new TimeSpan(0, 0, 0, seconds, 0));
            PlaybackPositionUpdated?.Invoke(this, new PlayerStateChangedEventArgs(PlayerState.Playing, newPos));
            MediaPlayer.Position = newPos;
        }

        public void Stop()
        {
            if (MediaPlayer == null)
                throw new InvalidOperationException("Must call Load on the MediaPlayer before attempting to stop");

            MediaPlayer.Stop();
            MediaPlayer.Close();
            PlaybackStarted?.Invoke(this, new PlayerStateChangedEventArgs(PlayerState.Stopped_On_Request));
        }

        #endregion
    }
}
