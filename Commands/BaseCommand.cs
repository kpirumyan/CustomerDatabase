using System;
using System.Windows.Input;

namespace CustomerDatabase.Commands
{
  public class BaseCommand : ICommand
  {
    private readonly Action<object> _execute;
    private readonly Predicate<object> _canExecute;

    public event EventHandler CanExecuteChanged
    {
      add { CommandManager.RequerySuggested += value; }
      remove { CommandManager.RequerySuggested -= value; }
    }

    public BaseCommand(Action<object> execute)
    {
      _execute = execute;
    }

    public BaseCommand(Action<object> execute, Predicate<object> canExecute)
    {
      _execute = execute;
      _canExecute = canExecute;
    }

    public bool CanExecute(object parameter)
    {
      return _canExecute != null ? _canExecute(parameter) : true;
    }

    public void Execute(object parameter)
    {
      _execute(parameter);
    }
  }
}
