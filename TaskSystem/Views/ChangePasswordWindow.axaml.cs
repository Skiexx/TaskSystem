using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using TaskSystem.ViewModels;

namespace TaskSystem.Views;

public partial class ChangePasswordWindow : Window
{
    public ChangePasswordWindow()
    {
        DataContext = new ChangePasswordViewModel();
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