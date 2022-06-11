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

public class TaskSystemViewModel : ViewModelBase
{
    public TaskSystemViewModel()
    {
        GetTask = ReactiveCommand.Create(GetTaskImpl);
        Exit = ReactiveCommand.Create<Window>(ExitImpl);
    }

    #region [Buttons]

    public ReactiveCommand<Unit, Unit> GetTask { get; }
    
    public ReactiveCommand<Window, Unit> Exit { get; }

    #endregion

    #region [Methods]

    private void GetTaskImpl()
    {
        if (SelectedTask == null || SelectedTask.StatusId == 2) return;

        SelectedTask.StatusId = 2;
        SelectedTask.Performer = AuthorizationWindowViewModel.AuthorizedUser;
        try
        {
            Connector.GetContext().Tasks.Update(SelectedTask);
            Connector.GetContext().SaveChanges();
            MessageBoxManager.GetMessageBoxStandardWindow("Успешно",
                    "Задание успешно взято. ",
                    ButtonEnum.Ok,
                    Icon.Success)
                .Show();
            TaskBox.Remove(SelectedTask);
        }
        catch (Exception e)
        {
            MessageBoxManager.GetMessageBoxStandardWindow("Ошибка",
                    "Задание не было взято. ",
                    ButtonEnum.Ok,
                    Icon.Success)
                .Show();
            Connector.ReloadContext();
        }
    }

    private void ExitImpl(Window window)
    {
        new MainMenuWindow().Show();
        window.Close();
    }

    #endregion

    #region [Variables]

    private ObservableCollection<Task> _taskBox = new(Connector.GetContext()
        .Tasks.Where(t =>
            t.StatusId == 1 && t.CustomerId != AuthorizationWindowViewModel.AuthorizedUser!.Id && t.PerformerId == null)
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

    #endregion
}