using System.Windows.Input;

namespace LoETool.Infrastructure;

public abstract class ViewModelBase : NotifyBase
{
    public ICommand CancelCommand { get; private set; }
    public ICommand OkCommand { get; private set; }

    public ViewModelBase()
    {
        CancelCommand = new Command(
            execute: () => Cancel(),
            canExecute: () => CancelCanExecute()
        );
        OkCommand = new Command(
            execute: () => Ok(),
            canExecute: () => OkCanExecute()
        );
    }

    public static bool AlwaysCanExecute() => true;

    public virtual void Cancel() { }
    public virtual void Ok() { }

    public virtual bool OkCanExecute() => true;
    public virtual bool CancelCanExecute() => true;
}
