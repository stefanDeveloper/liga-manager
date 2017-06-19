using System;
using System.ServiceModel;
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
        private MainWindow _mainWindow;

        public void Initialize()
        {
            _view = new LoginWindow();
            _bettorClient = new BettorClientServiceClient();

            #region View and ViewModel
            _loginWindowViewModel = new LoginWindowViewModel
            {
                SignInCommand = new RelayCommand(ExecuteSignInCommand),
                CloseCommand = new RelayCommand(ExecuteCloseCommand)
            };

            _view.DataContext = _loginWindowViewModel;
            #endregion

            _mainWindow = new MainWindow
            {
                Content = _view,
                Height = 300,
                Width = 315,
                ResizeMode = ResizeMode.NoResize
            };
            _mainWindow.Show();
        }

        private async void ExecuteSignInCommand(object obj)
        {
            // Check if service is available
            if (!await BettorClientHelper.IsAvailable(_bettorClient)) return;
             
            // Check if nickname exists.
            var nickname = _loginWindowViewModel.Nickname;
            if (nickname == null) return;
            var isSuccess = await  _bettorClient.IsValidNicknameAsync(nickname);
            if (isSuccess)
            {
                var bettor = await _bettorClient.GetBettorAsync(nickname);
                var menu = new MenuWindowController();
                menu.Initialize(_mainWindow, bettor);
            }
            else
            {
                MessageBox.Show("Der Benutzer ist nicht vorhanden!", "Anmeldung fehlgeschlagen",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ExecuteCloseCommand(object obj)
        {
            _mainWindow.Close();
        }
    }
}