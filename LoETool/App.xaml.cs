using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace LoETool;
/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        // make textboxes auto-select when they get focus

        EventManager.RegisterClassHandler(typeof(TextBox), UIElement.PreviewMouseLeftButtonDownEvent,
            new MouseButtonEventHandler(MouseHandler<TextBox>));
        EventManager.RegisterClassHandler(typeof(TextBox), UIElement.GotKeyboardFocusEvent,
            new RoutedEventHandler(TextBoxSelectText));
        EventManager.RegisterClassHandler(typeof(TextBox), Control.MouseDoubleClickEvent,
            new RoutedEventHandler(TextBoxSelectText));
    }

    private void MouseHandler<T>(object sender, MouseButtonEventArgs e) where T : UIElement
    {
        DependencyObject? parent = e.OriginalSource as UIElement;
        while (parent is not null and not T)
        {
            parent = VisualTreeHelper.GetParent(parent);
        }
        if (parent is not null)
        {
            if (parent is T control)
            {
                if (!control.IsKeyboardFocusWithin)
                {
                    control.Focus();
                    e.Handled = true;
                }
            }
        }
    }

    private void TextBoxSelectText(object sender, RoutedEventArgs e)
    {
        if (e.OriginalSource is TextBox tb)
        {
            tb.SelectAll();
        }
    }
}
