#nullable disable
using System;
using System.Windows.Input;

namespace LoETool.Infrastructure;

// this class based on the Command classes from .Net MAUI

public sealed class Command<T> : Command
{
    public Command(Action<T> execute)
        : base(x =>
        {
            if (IsValidParameter(x))
            {
                execute((T)x);
            }
        }) => ArgumentNullException.ThrowIfNull(execute, nameof(execute));

    public Command(Action<T> execute, Func<T, bool> canExecute)
        : base(x =>
        {
            if (IsValidParameter(x))
            {
                execute((T)x);
            }
        }, x => IsValidParameter(x) && canExecute((T)x))
    {
        ArgumentNullException.ThrowIfNull(execute, nameof(execute));
        ArgumentNullException.ThrowIfNull(canExecute, nameof(canExecute));
    }

    static bool IsValidParameter(object obj)
    {
        if (obj is not null)
        {
            return obj is T;
        }
        var t = typeof(T);
        // the parameter is null, is T nullable?
        if (Nullable.GetUnderlyingType(t) is not null)
        {
            return true;
        }
        // not a nullable, if it's a value type then null is not valid
        return !t.IsValueType;
    }
}

public class Command : ICommand
{
    readonly Func<object, bool> _canExecute;
    readonly Action<object> _execute;
    public event EventHandler CanExecuteChanged
    {
        add => CommandManager.RequerySuggested += value;
        remove => CommandManager.RequerySuggested -= value;
    }

    public Command()
    {
        _canExecute = null;
        _execute = null;
    }

    public Command(Action<object> execute) : this()
    {
        ArgumentNullException.ThrowIfNull(execute, nameof(execute));
        _execute = execute;
    }

    public Command(Action execute) : this(x => execute())
    {
        ArgumentNullException.ThrowIfNull(_execute, nameof(execute));
    }

    public Command(Action<object> execute, Func<object, bool> canExecute) : this(execute)
    {
        ArgumentNullException.ThrowIfNull(canExecute, nameof(canExecute));
        _canExecute = canExecute;
    }

    public Command(Action execute, Func<bool> canExecute) : this(x => execute(), x => canExecute())
    {
        ArgumentNullException.ThrowIfNull(execute, nameof(execute));
        ArgumentNullException.ThrowIfNull(canExecute, nameof(canExecute));
    }

    public bool CanExecute(object parm)
    {
        if (_canExecute is not null)
        {
            return _canExecute(parm!);
        }
        return true;
    }

    public void Execute(object parm) => _execute(parm);
}