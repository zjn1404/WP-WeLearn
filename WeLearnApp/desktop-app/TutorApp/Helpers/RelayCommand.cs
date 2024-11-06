using System.Windows.Input;

using System;

/// <summary>
/// Represents a command that can be executed and has an optional condition for enabling or disabling it.
/// This class is used to create commands that are bound to UI elements in MVVM (Model-View-ViewModel) architecture.
/// </summary>
public class RelayCommand : ICommand
{
    private readonly Action<object> _execute;
    private readonly Func<object, bool> _canExecute;

    /// <summary>
    /// Initializes a new instance of the <see cref="RelayCommand"/> class.
    /// </summary>
    /// <param name="execute">The action to be executed when the command is invoked.</param>
    /// <param name="canExecute">The function to determine if the command can be executed. Optional, defaults to always executable.</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="execute"/> is null.</exception>
    public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
    {
        _execute = execute ?? throw new ArgumentNullException(nameof(execute));
        _canExecute = canExecute;
    }

    /// <summary>
    /// Occurs when the <see cref="CanExecute"/> value changes, indicating whether the command can be executed.
    /// </summary>
    public event EventHandler CanExecuteChanged;

    /// <summary>
    /// Determines whether the command can execute based on the current conditions.
    /// </summary>
    /// <param name="parameter">An optional parameter to be passed to the <see cref="CanExecute"/> method.</param>
    /// <returns>True if the command can be executed; otherwise, false.</returns>
    public bool CanExecute(object parameter) => _canExecute == null || _canExecute(parameter);

    /// <summary>
    /// Executes the command.
    /// </summary>
    /// <param name="parameter">An optional parameter to be passed to the <see cref="Execute"/> method.</param>
    public void Execute(object parameter) => _execute(parameter);

    /// <summary>
    /// Raises the <see cref="CanExecuteChanged"/> event to notify listeners that the result of <see cref="CanExecute"/> has changed.
    /// </summary>
    public void RaiseCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
}
