using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using Avalonia.Controls;
using DynamicData;
using MessageBox.Avalonia;
using MessageBox.Avalonia.Enums;
using Microsoft.EntityFrameworkCore;
using ReactiveUI;
using TaskSystem.Core;
using TaskSystem.Models;
using TaskSystem.Views;

namespace TaskSystem.ViewModels;

public class MyTasksViewModel : ViewModelBase
{
    private Status? _selectedStatus;

    private Task? _selectedTask;

    private ObservableCollection<Status> _statuses = new(Connector.GetContext().Statuses);

    private ObservableCollection<Task> _taskBox = new(Connector.GetContext()
        .Tasks.Where(t =>
            t.CustomerId == AuthorizationWindowViewModel.AuthorizedUser!.Id && t.Status.Title != "Готово")
        .Include(t => t.Performer));

    public MyTasksViewModel()
    {
        SaveChanges = ReactiveCommand.Create<CheckBox>(SaveChangesImpl);
        Exit = ReactiveCommand.Create<Window>(ExitImpl);
        CheckButton = ReactiveCommand.Create<CheckBox>(CheckButtonImpl);
    }

    public ObservableCollection<Task> TaskBox
    {
        get => _taskBox;
        set => this.RaiseAndSetIfChanged(ref _taskBox, value);
    }

    public Task? SelectedTask
    {
        get
        {
            if (_selectedTask != null)
            {
                SelectedStatus = _selectedTask.Status;
            }

            return _selectedTask;
        }
        set => this.RaiseAndSetIfChanged(ref _selectedTask, value);
    }

    public ObservableCollection<Status> Statuses
    {
        get => _statuses;
        set => this.RaiseAndSetIfChanged(ref _statuses, value);
    }

    public Status? SelectedStatus
    {
        get => _selectedStatus;
        set => this.RaiseAndSetIfChanged(ref _selectedStatus, value);
    }

    public ReactiveCommand<CheckBox, Unit> SaveChanges { get; }
    public ReactiveCommand<Window, Unit> Exit { get; }

    public ReactiveCommand<CheckBox, Unit> CheckButton { get; }

    private void SaveChangesImpl(CheckBox checkBox)
    {
        if (_selectedTask == null) return;
        // var msg = MessageBoxManager.GetMessageBoxStandardWindow("Внимание",
        //     "Вы уверены что хотите сохранить изменения?", ButtonEnum.YesNo, Icon.Info);
        // if (msg.Result == ButtonResult.No) return;
        try
        {
            _selectedTask!.Status = SelectedStatus!;
            if (SelectedStatus!.Id == 1)
            {
                _selectedTask.Performer = null;
            }

            Connector.GetContext().Tasks.Update(_selectedTask);
            Connector.GetContext().SaveChanges();
            MessageBoxManager.GetMessageBoxStandardWindow("Успешно",
                    "Задача успешно сохранена ",
                    ButtonEnum.Ok,
                    Icon.Success)
                .Show();
            CheckButtonImpl(checkBox);
        }
        catch (Exception e)
        {
            MessageBoxManager.GetMessageBoxStandardWindow("Ошибка",
                    "Задача не была обновлена",
                    ButtonEnum.Ok,
                    Icon.Error)
                .Show();
            Connector.ReloadContext();
        }
    }

    private void ExitImpl(Window window)
    {
        new MainMenuWindow().Show();
        window.Close();
    }

    private void CheckButtonImpl(CheckBox checkBox)
    {
        switch (checkBox.IsChecked)
        {
            case false:
                TaskBox.RemoveMany(new ObservableCollection<Task>(Connector.GetContext()
                    .Tasks.Where(t => t.Status.Title == "Готово").Include(t => t.Performer)));
                break;
            case true:
                TaskBox = new ObservableCollection<Task>(Connector.GetContext()
                    .Tasks.Where(t => t.CustomerId == AuthorizationWindowViewModel.AuthorizedUser!.Id)
                    .Include(t => t.Performer));
                break;
        }

        if (SelectedTask == null)
        {
            SelectedStatus = null;
        }
    }
}