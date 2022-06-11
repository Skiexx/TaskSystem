using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using Microsoft.EntityFrameworkCore;
using ReactiveUI;
using TaskSystem.Core;
using TaskSystem.Models;

namespace TaskSystem.ViewModels;

public class TaskSystemViewModel : ViewModelBase
{
    public TaskSystemViewModel()
    {
        GetTask = ReactiveCommand.Create(GetTaskImpl);
    }

    #region [Buttons]

    public ReactiveCommand<Unit, Unit> GetTask { get; }

    #endregion

    #region [Methods]

    private void GetTaskImpl()
    {
        if (SelectedTask == null || SelectedTask.StatusId == 2) return;

        SelectedTask.StatusId = 2;
        SelectedTask.Performer = AuthorizationWindowViewModel.AuthorizedUser;
        Connector.GetContext().Tasks.Update(SelectedTask);
        Connector.GetContext().SaveChanges();
        TaskBox.Remove(SelectedTask);
    }

    #endregion

    #region [Variables]

    private ObservableCollection<Task> _taskBox = new(Connector.GetContext()
        .Tasks.Where(t => t.StatusId == 1 && t.CustomerId != AuthorizationWindowViewModel.AuthorizedUser.Id)
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