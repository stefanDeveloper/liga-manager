using System;
using System.Linq;
using System.Windows;
using LigaManagerAdminClient.AdminClientService;
using LigaManagerAdminClient.Framework;
using LigaManagerAdminClient.ViewModels;
using LigaManagerAdminClient.Views;
using LigaManagerBettorClient.Frameworks;
using LigaManagerServer.Models;

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
                SelectedTeam = teams.FirstOrDefault(),
                BackCommand = new RelayCommand(ExecuteBackCommand),
                AddCommand = new RelayCommand(ExecuteAddCommand),
                DeleteCommand = new RelayCommand(ExecuteDeleteCommand),
                ChangeCommand = new RelayCommand(ExecuteChangeCommand)
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

        private async void ExecuteAddCommand(object obj)
        {
            var addBettorWindow = new AddTeamWindowController
            {
                Team = new Team()
            };

            var showTeam = addBettorWindow.ShowTeam();
            // it could be possible that the bettor is null
            if (showTeam == null) return;
            // Check if service is available
            if (!await AdminClientHelper.IsAvailable(_adminClient)) return;
            // add bettor
            var isAdded = await _adminClient.AddTeamAsync(showTeam);

            if (isAdded)
            {
                ReloadBettors();
            }
            else
            {
                MessageBox.Show("Tipper konnte nicht hinzugefügt werden, da der Nickname schon vergeben ist!", "Hinzufügen fehlgeschlagen",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void ReloadBettors()
        {
            
        }

        private async void ExecuteChangeCommand(object obj)
        {
            if (_viewModel.SelectedTeam == null)
            {
                MessageBox.Show("Bitte wählen Sie einen Tipper aus!", "Kein Tipper ausgewählt",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            var addBettorWindow = new AddTeamWindowController
            {
                /*Team = _viewModel.SelectedTeam*/
            };

            var showTeam = addBettorWindow.ShowTeam();
            // it could be possible that the bettor is null
            if (showTeam == null) return;
            // Check if service is available
            if (!await AdminClientHelper.IsAvailable(_adminClient)) return;
            // add bettor
            var isAdded = await _adminClient.UpdateTeamAsync(showTeam);

            if (isAdded)
            {
                ReloadBettors();
            }
            else
            {
                MessageBox.Show("Der Benutzer konnte nicht geändert werden!", "Änderung fehlgeschlagen",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void ExecuteDeleteCommand(object obj)
        {
            if (_viewModel.SelectedTeam == null)
            {
                MessageBox.Show("Bitte wählen Sie einen Tipper aus!", "Kein Tipper ausgewählt",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            // Check if the user really want to delete the bettor
            var messageBoxResult = MessageBox.Show("Sind Sie sicher, dass der Benutzer gelöscht werden soll!", "Benutzer löschen",
                MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (messageBoxResult != MessageBoxResult.Yes) return;
            // Check if service is available
            if (!await AdminClientHelper.IsAvailable(_adminClient)) return;
            // delete bettor
            var isDeleted = false; /*_adminClient.DeleteTeam(_viewModel.SelectedTeam);*/
            if (isDeleted)
            {
                ReloadBettors();
            }
            else
            {
                MessageBox.Show("Tipper konnte nicht gelöscht werden!", "Löschen fehlgeschlagen",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion
    }
}