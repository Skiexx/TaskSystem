using System.Reactive;
using Avalonia.Controls;
using ReactiveUI;
using TaskSystem.Views;

namespace TaskSystem.ViewModels;

public class MainMenuViewModel : ViewModelBase
{
    public MainMenuViewModel()
    {
        Profile = ReactiveCommand.Create<Window>(ProfileImpl);
        TaskSystem = ReactiveCommand.Create<Window>(TaskSystemImpl);
        TakenTasks = ReactiveCommand.Create<Window>(TakenTasksImpl);
        CreateTask = ReactiveCommand.Create<Window>(CreateTaskImpl);
        Exit = ReactiveCommand.Create<Window>(ExitImpl);
        MyTasks = ReactiveCommand.Create<Window>(MyTasksImpl);
    }
    
    public ReactiveCommand<Window, Unit> Profile { get; }
    public ReactiveCommand<Window, Unit> TaskSystem { get; }
    public ReactiveCommand<Window, Unit> TakenTasks { get; }
    public ReactiveCommand<Window, Unit> CreateTask { get; }
    public ReactiveCommand<Window, Unit> Exit { get; }
    public ReactiveCommand<Window, Unit> MyTasks { get; }

    private void ProfileImpl(Window window)
    {
        new ProfileWindow().Show();
        window.Close();
    }
    private void TaskSystemImpl(Window window)
    {
        new TaskSystemWindow().Show();
        window.Close();
    }
    private void TakenTasksImpl(Window window)
    {
        new TakenTasksWindow().Show();
        window.Close();
    }
    private void CreateTaskImpl(Window window)
    {
        
    }
    private void ExitImpl(Window window)
    {
        new AuthorizationWindow().Show();
        window.Close();
    }
    private void MyTasksImpl(Window window)
    {
        
    }
}