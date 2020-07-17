using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace SampleApp.Core
{
    public class RelayCommand : ICommand 
    {
        private readonly Action<object> _action;
        private readonly Predicate<object> _canExecute;

        public RelayCommand(Action action) : this(_ => action()) { }
        public RelayCommand(Action<object> action, Predicate<object> canExecute = null)
        {
            _action = action;
            _canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute?.Invoke(parameter) ?? false;
        }

        public void Execute(object parameter)
        {
            _action(parameter);
        }
    }

    public class RelayCommand<T> : RelayCommand where T : class
    {
        public RelayCommand(Action<T> action, Predicate<T> canExecute = null) 
            : base(x => action(x as T), x => canExecute?.Invoke(x as T) ?? true)
        {
        }
    }
}
