using System.Linq;
using System.Reactive;
using Avalonia.Controls;
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
                    .GetMessageBoxStandardWindow("Ошибка", "Пользователь не найден, повторите попытку.").Show();
                return;
            }

            AuthorizedUser = authUser;
            TaskSystemWindow taskSystemWindow = new();
            taskSystemWindow.Show();
            window.Close();
        }

        private void RegistrationImpl(Window window)
        {
            RegistrationWindow registrationWindow = new();
            registrationWindow.Show();
            window.Close();
        }

        #region [Propetries]

        public static string? Login { get; set; } = null;
        public static User? AuthorizedUser { get; set; } = null;
        public string? Password { get; set; } = null;

        #endregion
    }
}