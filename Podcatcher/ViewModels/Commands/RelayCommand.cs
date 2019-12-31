using System;
using System.Windows.Input;

namespace Podcatcher.ViewModels.Commands
{
    public class RelayCommand<T> : ICommand
    {
        #region Fields

        readonly Action<T> _execute = null;
        readonly Predicate<T> _canExecute = null;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new instance with the predicate set to null.
        /// </summary>
        /// <param name="action">Delegate to execute when Execute is called on this command. </param>
        /// <remarks> <seealso cref="CanExecute"/> will always return true. </remarks>
        public RelayCommand(Action<T> action) : this(action, null)
        {
        }

        public RelayCommand(Action<T> action, Predicate<T> predicate)
        {
            _execute = action ?? throw new ArgumentException("action must not be null");
            _canExecute = predicate;
        }

        #endregion

        #region ICommand Members

        /// <summary>
        /// Determines whether the command can execute with the given paramater.
        /// </summary>
        /// <param name="parameter">Data used by the command. If the command does not require any data this object can be set to null. </param>
        /// <returns> true if this command can be executed, otherwise false. </returns>
        public bool CanExecute(object parameter)
        {
            return _canExecute == null ? true : _canExecute((T)parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public void Execute(object parameter)
        {
            _execute((T)parameter);
        }

        #endregion
    }
}
