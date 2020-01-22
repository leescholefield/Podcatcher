using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Podcatcher.Models.Playback
{
    public interface IPlayer
    {

        void Load(MediaItem item);

        void Play();

        void Pause();

        void Stop();

        void SkipForward(int durationSeconds);

        void SkipBack(int durationSeconds);
    }
}
