using Podcatcher.Models;
using Podcatcher.Models.Playback;
using System;

namespace Podcatcher.ViewModels.Services
{
    /// <summary>
    /// Service to request MediaItems be played.
    /// </summary>
    public interface IPlaybackService
    {

        IPlayer Player {get;}

        event EventHandler<MediaItemChangedEventArgs> NowPlayingChanged;

        void Load(Episode episode, bool beginPlaybackOnLoaded = true);

        void Play();

        void Pause();

        void SkipForward(int durationSecons);

        void SkipBack(int durationSeconds);
    }

    public class MediaItemChangedEventArgs : EventArgs
    {
        public MediaItem MediaItem { get; private set; }

        public MediaItemChangedEventArgs(MediaItem item)
        {
            MediaItem = item;
        }
    }
}
