using System;
using System.Linq;
using System.Windows;
using LigaManagerAdminClient.AdminClientService;
using LigaManagerAdminClient.Framework;
using LigaManagerAdminClient.ViewModels;
using LigaManagerAdminClient.Views;
using LigaManagerBettorClient.Frameworks;

namespace LigaManagerAdminClient.Controllers
{
    public class TeamWindowController
    {
        private TeamWindow _view;
        private TeamWindowViewModel _viewModel;
        private MainWindow _mainWindow;
        private AdminClientServiceClient _adminClient;

        public async void Initialize(MainWindow mainWindow)
        {
            _mainWindow = mainWindow;
            _view = new TeamWindow();
            _adminClient = new AdminClientServiceClient();

            #region View and ViewModel
            // Check if service is available
            if (!await AdminClientHelper.IsAvailable(_adminClient)) return;
            var teams = _adminClient.GetAllTeams().ToList();
            _viewModel = new TeamWindowViewModel
            {
                Teams = teams,
                SelectedTeam = teams.First(),
                BackCommand = new RelayCommand(ExecuteBackCommand)
            };

            _view.DataContext = _viewModel;
            #endregion

            _mainWindow.Content = _view;
        }
        #region ExecuteCommands
        private void ExecuteBackCommand(object obj)
        {
            var menuWindow = new MenuWindowController();
            menuWindow.Initialize(_mainWindow);
        }

        private void ExecuteAddCommand(object obj)
        {

        }

        private void ExecuteChangeCommand(object obj)
        {

        }

        private void ExecuteDeleteCommand(object obj)
        {
            // Check if the user really want to delete the team
            var messageBoxResult = MessageBox.Show("Sind Sie sicher, dass der Benutzer gelöscht werden soll!", "Benutzer löschen",
                MessageBoxButton.YesNo, MessageBoxImage.Warning);
           
        }
        #endregion
    }
}