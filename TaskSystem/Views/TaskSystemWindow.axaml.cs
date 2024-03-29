using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using TaskSystem.ViewModels;

namespace TaskSystem.Views;

public partial class TaskSystemWindow : Window
{
    public TaskSystemWindow()
    {
        DataContext = new TaskSystemViewModel();
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