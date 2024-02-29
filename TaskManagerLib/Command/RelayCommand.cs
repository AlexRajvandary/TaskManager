using System.Windows.Input;

namespace TaskManagerLib.Command
{
    public class RelayCommand<T> : ICommand
    {
        private readonly Action<T> action;

        public RelayCommand(Action<T> action)
        {
            this.action = action;
        }

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            throw new NotImplementedException();
        }

        public void Execute(T parameter)
        {
            action?.Invoke(parameter);
        }

        public void Execute(object? parameter)
        {
            action?.Invoke((T)parameter);
        }
    }

    public class RelayCommand : ICommand
    {
        private readonly Action<object?> action;
        private readonly Action parameterlessAction;

        public RelayCommand(Action<object?> action)
        {
            this.action = action;
        }

        public RelayCommand(Action parameterlessAction)
        {
            this.parameterlessAction = parameterlessAction;
        }

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            throw new NotImplementedException();
        }

        public void Execute(object? parameter)
        {
            if(parameterlessAction != null)
            {
                action?.Invoke(parameterlessAction);
            }
            else
            {
                action?.Invoke(parameter);
            }
        }
    }
}
