using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Library.UI.Common
{
    public class CommandBase : ICommand
    {
        private readonly Action<object> execute = null;
        private readonly Predicate<object> canExecute = null;

        public CommandBase(Action<object> execute)
            : this(execute, null) { }

        public CommandBase(Action<object> execute, Predicate<object> canExecute)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return canExecute != null ? canExecute(parameter) : true;
        }

        public void Execute(object parameter)
        {
            if (execute != null)
                execute(parameter);
        }

        public void OnCanExecuteChanged()
        {
            CanExecuteChanged(this, EventArgs.Empty);
        }
    }
}
