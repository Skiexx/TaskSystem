using System;
using System.Reactive;
using Avalonia.Controls;
using MessageBox.Avalonia.Enums;
using ReactiveUI;
using TaskSystem.Core;
using TaskSystem.Models;
using TaskSystem.Views;

namespace TaskSystem.ViewModels;

public class RegistrationViewModel : ViewModelBase
{
    public RegistrationViewModel()
    {
        Registration = ReactiveCommand.Create<Window>(RegistrationImpl);
        Cancel = ReactiveCommand.Create<Window>(CancelImpl);
    }
    
    #region [Variables]

    public string? Login { get; set; }
    public string? Password { get; set; }
    public string? LastName { get; set; }
    public string? FirstName { get; set; }
    public string? MiddleName { get; set; }
    public string? PhoneNumber { get; set; }

    #endregion
    
    public ReactiveCommand<Window,Unit> Registration { get; }
    
    public ReactiveCommand<Window,Unit> Cancel { get; }

    private void RegistrationImpl(Window window)
    {
        try
        {
            User user = new()
            {
                FirstName = this.FirstName!,
                LastName = this.LastName!,
                Login = this.Login!,
                MiddleName = this.MiddleName!,
                Password = this.Password!,
                PhoneNumber = this.PhoneNumber!
            };
            Connector.GetContext().Users.Add(user);
            Connector.GetContext().SaveChanges();
            MessageBox.Avalonia.MessageBoxManager
                .GetMessageBoxStandardWindow("Успешно", "Успешно добавленно", ButtonEnum.Ok, Icon.Success).Show();
            AuthorizationWindowViewModel.Login = this.Login;
            new AuthorizationWindow().Show();
            window.Close();
        }
        catch (Exception e)
        {
            MessageBox.Avalonia.MessageBoxManager
                .GetMessageBoxStandardWindow("Ошибка",
                    $"{e.Message}\nСоздание аккаунта не выполнено!",
                    ButtonEnum.Ok,
                    Icon.Error)
                .Show();
            Connector.ReloadContext();
        }
    }

    private void CancelImpl(Window window)
    {
        new AuthorizationWindow().Show();
        window.Close();
    }
}