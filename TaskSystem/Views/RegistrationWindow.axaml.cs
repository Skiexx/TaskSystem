using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using TaskSystem.ViewModels;

namespace TaskSystem.Views;

public partial class RegistrationWindow : Window
{
    public RegistrationWindow()
    {
        DataContext = new RegistrationViewModel();
        InitializeComponent();
#if DEBUG
        this.AttachDevTools();
#endif
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}