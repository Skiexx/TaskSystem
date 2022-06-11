using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using Avalonia.Controls;
using MessageBox.Avalonia;
using MessageBox.Avalonia.Enums;
using Microsoft.EntityFrameworkCore;
using ReactiveUI;
using TaskSystem.Core;
using TaskSystem.Models;
using TaskSystem.Views;

namespace TaskSystem.ViewModels;

public class TakenTasksViewModel : ViewModelBase
{
    public TakenTasksViewModel()
    {
        Exit = ReactiveCommand.Create<Window>(ExitImpl);
        CancelTask = ReactiveCommand.Create(CancelTaskImpl);
    }
    private ObservableCollection<Task> _taskBox = new(Connector.GetContext()
        .Tasks.Where(t => t.PerformerId == AuthorizationWindowViewModel.AuthorizedUser!.Id)
        .Include(t => t.Customer));

    public ObservableCollection<Task> TaskBox
    {
        get => _taskBox;
        set => this.RaiseAndSetIfChanged(ref _taskBox, value);
    }

    private Task? _selectedTask;

    public Task? SelectedTask
    {
        get => _selectedTask;
        set => this.RaiseAndSetIfChanged(ref _selectedTask, value);
    }
    
    public ReactiveCommand<Window, Unit> Exit { get; }
    public ReactiveCommand<Unit, Unit> CancelTask { get; }
    
    private void ExitImpl(Window window)
    {
        new MainMenuWindow().Show();
        window.Close();
    }

    private void CancelTaskImpl()
    {
        if (SelectedTask == null || SelectedTask.StatusId is 1 or 3) return;
        
        SelectedTask.StatusId = 1;
        SelectedTask.Performer = null;
        try
        {
            Connector.GetContext().Tasks.Update(SelectedTask);
            Connector.GetContext().SaveChanges();
            MessageBoxManager.GetMessageBoxStandardWindow("Успешно",
                    "Задание успешно отменено. ",
                    ButtonEnum.Ok,
                    Icon.Success)
                .Show();
            TaskBox.Remove(SelectedTask);
        }
        catch (Exception e)
        {
            MessageBoxManager.GetMessageBoxStandardWindow("Ошибка",
                    "Задание не было возвращено. ",
                    ButtonEnum.Ok,
                    Icon.Success)
                .Show();
            Connector.ReloadContext();
        }
    }
}