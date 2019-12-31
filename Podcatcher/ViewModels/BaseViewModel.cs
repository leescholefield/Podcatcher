using Podcatcher.ViewModels.Commands;
using System.ComponentModel;
using System.Windows.Input;

namespace Podcatcher.ViewModels
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand WindowClosing { get; set; }

        public BaseViewModel()
        {
            WindowClosing = new RelayCommand<object>(WindowClosing_Execute);
            InitializeCommands();
        }

        protected void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        private void WindowClosing_Execute(object _)
        {
            OnClose();
        }

        public virtual void OnClose() { }

        protected virtual void InitializeCommands() { }
    }
}
