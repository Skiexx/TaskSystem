using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using TaskSystem.ViewModels;

namespace TaskSystem.Views;

public partial class MyTasksWindow : Window
{
    public MyTasksWindow()
    {
        DataContext = new MyTasksViewModel();
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