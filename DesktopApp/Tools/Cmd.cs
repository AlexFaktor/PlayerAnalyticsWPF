using System.Windows.Input;

namespace DesktopApp.Tools;
internal class RelayCommand : ICommand
{
    private readonly Action? _execute;
    private readonly Func<bool>? _canExecute;

    public event EventHandler? CanExecuteChanged;

    public RelayCommand(Action execute, Func<bool>? canExecute = null)
    {
        _execute = execute;
        _canExecute = canExecute;
    }

    public bool CanExecute(object? parameter) // змінено на object?
    {
        return _canExecute == null || _canExecute();
    }

    public void Execute(object? parameter) // змінено на object?
    {
        _execute?.Invoke();
    }

    public void RaiseCanExecuteChanged()
    {
        CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
}
