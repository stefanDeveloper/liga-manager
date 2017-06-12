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
        private MainWindow _main;

        public void Initialize()
        {
            _view = new LoginWindow();
            _bettorClient = new BettorClientServiceClient();
            _loginWindowViewModel = new LoginWindowViewModel
            {
                SignInCommand = new RelayCommand(ExecuteSignInCommand),
                CloseCommand = new RelayCommand(ExecuteCloseCommand)
            };

            _view.DataContext = _loginWindowViewModel;

            _main = new MainWindow
            {
                Content = _view,
                Height = 300,
                Width = 315,
                ResizeMode = ResizeMode.NoResize
            };
            _main.Show();
        }

        private void ExecuteSignInCommand(object obj)
        {
            var nickname = _loginWindowViewModel.Nickname;
            if (nickname == null) return;
            var isSuccess = _bettorClient.Login(nickname);
            if (isSuccess)
            {
                var menu = new MenuWindowController()
                {
                    MainWindow = _main
                };
                menu.Initialize();
            }
            else
            {
                MessageBox.Show("Der Benutzer ist nicht vorhanden!", "Anmeldung fehlgeschlagen",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ExecuteCloseCommand(object obj)
        {
            _main.Close();
        }
    }
}