using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Avalonia.Threading;
using CollectionTracker.ViewModels;
using CollectionTracker.Views;

namespace CollectionTracker;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow
            {
                DataContext = new MainWindowViewModel(),
                WindowStartupLocation = WindowStartupLocation.CenterScreen,
                Position = new PixelPoint(0, 1000)
            };

            Dispatcher.UIThread.InvokeAsync(() =>
            {
                desktop.MainWindow.WindowState = WindowState.Maximized;
            }, DispatcherPriority.Background);
        }

        base.OnFrameworkInitializationCompleted();
    }
}
