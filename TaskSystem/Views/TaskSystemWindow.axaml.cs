using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace TaskSystem.Views;

public partial class TaskSystemWindow : Window
{
    public TaskSystemWindow()
    {
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