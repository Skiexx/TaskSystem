using System;
using System.Linq;
using System.Reactive;
using Avalonia.Controls;
using MessageBox.Avalonia;
using MessageBox.Avalonia.Enums;
using ReactiveUI;
using TaskSystem.Core;
using TaskSystem.Models;
using TaskSystem.Views;

namespace TaskSystem.ViewModels;

public class CreateTaskViewModel : ViewModelBase
{
    public CreateTaskViewModel()
    {
        CreateTask = ReactiveCommand.Create<Window>(CreateTaskImpl);
        Exit = ReactiveCommand.Create<Window>(ExitImpl);
    }

    public string? Label { get; set; }
    public string? Description { get; set; }

    public ReactiveCommand<Window, Unit> CreateTask { get; }
    public ReactiveCommand<Window, Unit> Exit { get; }

    private void CreateTaskImpl(Window window)
    {
        if (Label == null || Description == null)
        {
            MessageBoxManager.GetMessageBoxStandardWindow("Ошибка",
                    "Не все поля были заполнены",
                    ButtonEnum.Ok,
                    Icon.Error)
                .Show();
            return;
        }

        Task task = new()
        {
            Customer = AuthorizationWindowViewModel.AuthorizedUser!,
            Description = Description!,
            Status = Connector.GetContext().Statuses.FirstOrDefault(s => s.Title == "Не готово")!,
            Title = Label!
        };
        try
        {
            Connector.GetContext().Tasks.Add(task);
            Connector.GetContext().SaveChanges();
            MessageBoxManager.GetMessageBoxStandardWindow("Успешно",
                    "Задача успешно была добавлена ",
                    ButtonEnum.Ok,
                    Icon.Success)
                .Show();
            new MainMenuWindow().Show();
            window.Close();
        }
        catch (Exception e)
        {
            MessageBoxManager.GetMessageBoxStandardWindow("Ошибка",
                    "Задача не была добавлена ",
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
}