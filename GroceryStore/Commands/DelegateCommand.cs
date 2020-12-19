using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace GroceryStore.Commands
{
    class DelegateCommand : ICommand
    {
        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }
            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }

        private Action<object> execute;
        private Predicate<object> canExecute;
        public DelegateCommand(Action<object> e, Predicate<object> c)
        {
            this.execute = e;
            this.canExecute = c;
        }

        public bool CanExecute(object parameter)
        {
            return this.canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            this.execute(parameter);
        }
    }
}
