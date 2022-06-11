using Avalonia.Controls;
using TaskSystem.ViewModels;

namespace TaskSystem.Views
{
    public partial class AuthorizationWindow : Window
    {
        public AuthorizationWindow()
        {
            DataContext = new AuthorizationWindowViewModel();
            InitializeComponent();
        }
    }
}