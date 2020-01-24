using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Podcatcher.Models.Playback
{
    public class MediaItem : INotifyPropertyChanged
    {
        private Episode episodeValue = null;

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public Episode Episode
        {
            get
            {
                return episodeValue;
            }
            set
            {
                episodeValue = value;
                NotifyPropertyChanged();
            }
        }
    }
}
