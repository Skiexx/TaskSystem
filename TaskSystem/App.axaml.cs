using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using TaskSystem.ViewModels;
using TaskSystem.Views;

namespace TaskSystem
{
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
                desktop.MainWindow = new AuthorizationWindow
                {
                    DataContext = new AuthorizationWindowViewModel(),
                };
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}