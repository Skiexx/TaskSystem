using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using TaskSystem.ViewModels;

namespace TaskSystem.Views;

public partial class CreateTaskWindow : Window
{
    public CreateTaskWindow()
    {
        DataContext = new CreateTaskViewModel();
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