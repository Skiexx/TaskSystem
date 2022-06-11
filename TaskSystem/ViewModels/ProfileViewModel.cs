using System;
using System.Reactive;
using Avalonia.Controls;
using MessageBox.Avalonia;
using MessageBox.Avalonia.Enums;
using ReactiveUI;
using TaskSystem.Core;
using TaskSystem.Models;
using TaskSystem.Views;

namespace TaskSystem.ViewModels;

public class ProfileViewModel : ViewModelBase
{
    public ProfileViewModel()
    {
        Exit = ReactiveCommand.Create<Window>(ExitImpl);
        SaveChanges = ReactiveCommand.Create<Window>(SaveChangesImpl);
        ChangePassword = ReactiveCommand.Create<Window>(ChangePasswordImpl);
    }
    public ReactiveCommand<Window,Unit> Exit { get; }
    public ReactiveCommand<Window,Unit> ChangePassword { get; }
    public ReactiveCommand<Window,Unit> SaveChanges { get; }

    private void ExitImpl(Window window)
    {
        new MainMenuWindow().Show();
        window.Close();
    }

    private User _currentUser = AuthorizationWindowViewModel.AuthorizedUser!;

    public User CurrentUser
    {
        get => _currentUser;
        set => this.RaiseAndSetIfChanged(ref _currentUser, value);
    }

    private void SaveChangesImpl(Window window)
    {
        try
        {
            Connector.GetContext().Users.Update(CurrentUser);
            Connector.GetContext().SaveChanges();
            MessageBoxManager
                .GetMessageBoxStandardWindow("Успешно",
                    "Изменения успешно сохранены, войдите заново.",
                    ButtonEnum.Ok,
                    Icon.Success)
                .Show();
            AuthorizationWindowViewModel.Login = CurrentUser.Login;
            new AuthorizationWindow().Show();
            window.Close();
        }
        catch (Exception e)
        {
            MessageBoxManager.GetMessageBoxStandardWindow("Ошибка",
                    "Не удалось сохранить изменения",
                    ButtonEnum.Ok,
                    Icon.Error)
                .Show();
            Connector.ReloadContext();
        }
    }

    private void ChangePasswordImpl(Window window)
    {
        new ChangePasswordWindow().Show();
        window.Close();
    }
}