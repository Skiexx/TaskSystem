﻿using System.Linq;
using System.Reactive;
using Avalonia.Controls;
using MessageBox.Avalonia.Enums;
using ReactiveUI;
using TaskSystem.Core;
using TaskSystem.Models;
using TaskSystem.Views;

namespace TaskSystem.ViewModels
{
    public class AuthorizationWindowViewModel : ViewModelBase
    {
        public AuthorizationWindowViewModel()
        {
            SignIn = ReactiveCommand.Create<Window>(SignInImpl);
            Registration = ReactiveCommand.Create<Window>(RegistrationImpl);
        }

        public ReactiveCommand<Window, Unit> SignIn { get; }

        public ReactiveCommand<Window, Unit> Registration { get; }

        private void SignInImpl(Window window)
        {
            var authUser = Connector.GetContext().Users
                .FirstOrDefault(u => u.Login == Login && u.Password == Password);
            if (authUser == null)
            {
                MessageBox.Avalonia.MessageBoxManager
                    .GetMessageBoxStandardWindow("Ошибка",
                        "Пользователь не найден, повторите попытку.",
                        ButtonEnum.Ok,
                        Icon.Error)
                    .Show();
                return;
            }

            AuthorizedUser = authUser;
            new MainMenuWindow().Show();
            window.Close();
        }

        private void RegistrationImpl(Window window)
        {
            new RegistrationWindow().Show();
            window.Close();
        }

        #region [Propetries]

        public static string? Login { get; set; }
        public static User? AuthorizedUser { get; set; } = null;
        public string? Password { get; set; } = null;

        #endregion
    }
}