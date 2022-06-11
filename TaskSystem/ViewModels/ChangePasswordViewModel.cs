using System;
using System.Reactive;
using Avalonia.Controls;
using MessageBox.Avalonia;
using MessageBox.Avalonia.Enums;
using ReactiveUI;
using TaskSystem.Core;
using TaskSystem.Views;

namespace TaskSystem.ViewModels;

public class ChangePasswordViewModel : ViewModelBase
{
    public ChangePasswordViewModel()
    {
        SavePassword = ReactiveCommand.Create<Window>(SavePasswordImpl);
        Exit = ReactiveCommand.Create<Window>(ExitImpl);
    }
    public string? OldPassword { get; set; }
    public string? NewPassword { get; set; }
    public string? RepeatNewPassword { get; set; }
    
    public ReactiveCommand<Window, Unit> SavePassword { get; }
    public ReactiveCommand<Window, Unit> Exit { get; }

    private void SavePasswordImpl(Window window)
    {
        if (AuthorizationWindowViewModel.AuthorizedUser!.Password != OldPassword)
        {
            MessageBoxManager.GetMessageBoxStandardWindow("Ошибка",
                    "Неверный старый пароль ",
                    ButtonEnum.Ok,
                    Icon.Error)
                .Show();
            return;
        }
        else if (NewPassword != RepeatNewPassword)
        {
            MessageBoxManager.GetMessageBoxStandardWindow("Ошибка",
                    "Пароли не совпадают ",
                    ButtonEnum.Ok,
                    Icon.Error)
                .Show();
            return;
        }
        AuthorizationWindowViewModel.AuthorizedUser!.Password = NewPassword;
        try
        {
            Connector.GetContext().Users.Update(AuthorizationWindowViewModel.AuthorizedUser);
            Connector.GetContext().SaveChanges();
            MessageBoxManager.GetMessageBoxStandardWindow("Успешно",
                    "Пароль успешно изменён, войдите заново.",
                    ButtonEnum.Ok,
                    Icon.Success)
                .Show();
            new AuthorizationWindow().Show();
            window.Close();
        }
        catch (Exception e)
        {
            MessageBoxManager.GetMessageBoxStandardWindow("Ошибка",
                    "Пароль не был сохранён ",
                    ButtonEnum.Ok,
                    Icon.Error)
                .Show();
            Connector.ReloadContext();
        }
    }

    private void ExitImpl(Window window)
    {
        new ProfileWindow().Show();
        window.Close();
    }
}