using System.Windows;
using System.Windows.Input;

namespace LoETool.Infrastructure;

public class Behaviors
{
    // window loaded

    public static readonly DependencyProperty WindowLoadedBehaviorProperty =
        DependencyProperty.RegisterAttached("WindowLoadedBehavior", typeof(ICommand),
            typeof(Behaviors), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.None,
                OnWindowLoadedBehaviorChanged));

    public static ICommand GetWindowLoadedBehavior(DependencyObject d) =>
        (ICommand)d.GetValue(WindowLoadedBehaviorProperty);
    public static void SetWindowLoadedBehavior(DependencyObject d, ICommand value) =>
        d.SetValue(WindowLoadedBehaviorProperty, value);
    public static void OnWindowLoadedBehaviorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is Window w)
        {
            w.Loaded += (s, a) =>
            {
                var command = GetWindowLoadedBehavior(w);
                if (command is not null)
                {
                    if (command.CanExecute(a))
                    {
                        command.Execute(a);
                    }
                }
            };
        }
    }

    // window closed

    public static readonly DependencyProperty WindowClosedBehaviorProperty =
        DependencyProperty.RegisterAttached("WindowClosedBehavior", typeof(ICommand),
            typeof(Behaviors), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.None,
                OnWindowClosedBehaviorChanged));

    public static ICommand GetWindowClosedBehavior(DependencyObject d) =>
        (ICommand)d.GetValue(WindowClosedBehaviorProperty);
    public static void SetWindowClosedBehavior(DependencyObject d, ICommand value) =>
        d.SetValue(WindowClosedBehaviorProperty, value);
    public static void OnWindowClosedBehaviorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is Window w)
        {
            w.Closed += (s, a) =>
            {
                var command = GetWindowClosedBehavior(w);
                if (command is not null)
                {
                    if (command.CanExecute(a))
                    {
                        command.Execute(a);
                    }
                }
            };
        }
    }
}
