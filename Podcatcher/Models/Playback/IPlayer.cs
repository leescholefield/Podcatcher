using System;

namespace Podcatcher.Models.Playback
{
    public interface IPlayer
    {

        event EventHandler<PlayerStateChangedEventArgs> PlaybackStarted;

        event EventHandler<PlayerStateChangedEventArgs> PlaybackPaused;

        event EventHandler<PlayerStateChangedEventArgs> PlaybackStopped;

        event EventHandler<PlayerStateChangedEventArgs> PlaybackPositionUpdated;

        void Load(MediaItem item);

        void Play();

        void Pause();

        void Stop();

        void SkipForward(int durationSeconds);

        void SkipBack(int durationSeconds);
    }

    public class PlayerStateChangedEventArgs : EventArgs
    {
        public PlayerState NewState { get; protected set; }

        public TimeSpan Position { get; set; }

        public PlayerStateChangedEventArgs(PlayerState newState)
        {
            NewState = newState;
        }

        public PlayerStateChangedEventArgs(PlayerState newState, TimeSpan position) : this(newState)
        {
            Position = position;
        }
    }

    public enum PlayerState
    {
        Playing, Paused, Stopped_On_Error, Stopped_On_Request
    }
}
