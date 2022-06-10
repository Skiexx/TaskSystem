using System.Linq;
using System.Reactive;
using Avalonia.Controls;
using ReactiveUI;
using TaskSystem.Core;
using TaskSystem.Models;

namespace TaskSystem.ViewModels
{
    public class AuthorizationWindowViewModel : ViewModelBase
    {
        public AuthorizationWindowViewModel()
        {
            SignIn = ReactiveCommand.Create<TextBox>(_signIn);
        }

        public ReactiveCommand<TextBox, Unit> SignIn { get; }

        private void _signIn(TextBox passwordBox)
        {
            var authUser = Connector.GetContext().Users
                .FirstOrDefault(u => u.Login == Login && u.Password == passwordBox.Text);
            if (authUser == null)
            {
                MessageBox.Avalonia.MessageBoxManager
                    .GetMessageBoxStandardWindow("Ошибка", "Пользователь не найден, повторите попытку.").Show();
                return;
            }

            SetUser = authUser;
        }

        #region [Propetries]

        public string? Login { get; set; } = null;
        public static User? SetUser { get; set; } = null;
        public string? Password { get; set; } = null;

        #endregion
    }
}