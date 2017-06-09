using System.Windows;
using LigaManagerBettorClient.BettorClientService;
using LigaManagerBettorClient.Frameworks;
using LigaManagerBettorClient.ViewModels;
using LigaManagerBettorClient.Views;

namespace LigaManagerBettorClient.Controllers
{
    public class LoginWindowController
    {
        private LoginWindowViewModel _loginWindowViewModel;
        private BettorClientServiceClient _bettorClient;
        private LoginWindow _view;

        public void Initialize()
        {
            _view = new LoginWindow();
            _bettorClient = new BettorClientServiceClient();
            _loginWindowViewModel = new LoginWindowViewModel
            {
                SignInCommand = new RelayCommand(ExecuteSignInCommand)
            };

            _view.DataContext = _loginWindowViewModel;
            _view.ShowDialog();
        }

        private void ExecuteSignInCommand(object obj)
        {
            var nickname = _loginWindowViewModel.Nickname;
            var isSuccess = _bettorClient.Login(nickname);
            if (isSuccess)
            {

            }
            else
            {
                MessageBox.Show("Der Benutzer ist nicht vorhanden!", "Anmeldung fehlgeschlagen",MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}