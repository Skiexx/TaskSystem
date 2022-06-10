using System.Reactive;
using Avalonia.Controls;
using ReactiveUI;

namespace TaskSystem.ViewModels
{
    public class AuthorizationWindowViewModel : ViewModelBase
    {
        public AuthorizationWindowViewModel()
        {
            SignIn = ReactiveCommand.Create<TextBox>(_signIn);
        }

        #region [Propetries]

        public string? Login { get; set; } = null;

        #endregion

        public ReactiveCommand<TextBox, Unit> SignIn { get; }

        private void _signIn(TextBox passwordBox)
        {
        }
    }
}