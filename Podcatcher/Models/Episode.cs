using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Podcatcher.Models
{
    public class Episode : INotifyPropertyChanged
    {

        private string titleValue = string.Empty;
        private string authorValue = string.Empty;
        private string streamUrlValue = null;
        private string descriptionValue = string.Empty;

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public string Title
        {
            get
            {
                return titleValue;
            }
            set
            {
                titleValue = value;
                NotifyPropertyChanged();
            }
        }

        public string Author
        {
            get
            {
                return authorValue;
            }
            set
            {
                authorValue = value;
                NotifyPropertyChanged();
            }
        }

        public string StreamUrl
        {
            get
            {
                return streamUrlValue;
            }
            set
            {
                streamUrlValue = value;
                NotifyPropertyChanged();
            }
        }

        public string Description
        {
            get
            {
                return descriptionValue;
            }
            set
            {
                descriptionValue = value;
                NotifyPropertyChanged();
            }
        }

    }
}
