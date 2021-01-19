using CW.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace CW.Commands
{
    public class TapCardCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private Action<object> _action;

        public TapCardCommand(Action<object> action)
        {
            _action = action;
        }

        public bool CanExecute(object parameter)
        {
            return (parameter as BankCard).IsWorked;
        }

        public void Execute(object parameter)
        {
            if (CanExecute(parameter))
            {
                _action.Invoke(parameter);
            }
        }
    }
}
