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
    public class SeasonWindowController
    {
        private SeasonWindow _view;
        private SeasonWindowViewModel _viewModel;
        private AdminClientServiceClient _adminClient;
        private MainWindow _mainWindow;


        public async void Initialize(MainWindow mainWindow)
        {
            _mainWindow = mainWindow;
            _adminClient = new AdminClientServiceClient();

            #region View And ViewModel
            _view = new SeasonWindow();
            var seasons = await  _adminClient.GetSeasonsAsync();
            _viewModel = new SeasonWindowViewModel
            {
                BackCommand = new RelayCommand(ExecuteBackCommand),
                AddCommand = new RelayCommand(ExecuteAddCommand),
                DeleteCommand = new RelayCommand(ExecuteDeleteCommand),
                ChangeCommand = new RelayCommand(ExecuteChangeCommand),
                Seasons = seasons.ToList(),
                SelectedSeason = seasons.ToList().FirstOrDefault()
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
            var addSeasonWindow = new AddSeasonWindowController
            {
                Season = new Season()
            };
            var showSeason = addSeasonWindow.ShowSeason();
            // it could be possible that the bettor is null
            if (showSeason == null) return;
            // Check if service is available
            if (!await AdminClientHelper.IsAvailable(_adminClient)) return;
            // add bettor
            var addBettorAsync = await _adminClient.AddSeasonAsync(showSeason);

            if (addBettorAsync)
            {
                ReloadSeason();
            }
            else
            {
                MessageBox.Show("Saison konnte nicht hinzugefügt werden, da der Name schon vergeben ist!", "Hinzufügen fehlgeschlagen",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void ReloadSeason()
        {
            var seasons = await _adminClient.GetSeasonsAsync();
            _viewModel.Seasons = seasons.ToList();
        }

        private async void ExecuteChangeCommand(object obj)
        {
            if (_viewModel.SelectedSeason == null)
            {
                MessageBox.Show("Bitte wählen Sie eine Saison aus!", "Keine Saison ausgewählt",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            var addSeasonWindow = new AddSeasonWindowController
            {
                Season = _viewModel.SelectedSeason
            };

            var showSeason = addSeasonWindow.ShowSeason();
            // it could be possible that the bettor is null
            if (showSeason == null) return;
            // Check if service is available
            if (!await AdminClientHelper.IsAvailable(_adminClient)) return;
            // add bettor
            var isUpdated = await _adminClient.UpdateSeasonAsync(showSeason);

            if (isUpdated)
            {
                ReloadSeason();
            }
            else
            {
                MessageBox.Show("Die Saison konnte nicht geändert werden!", "Änderung fehlgeschlagen",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void ExecuteDeleteCommand(object obj)
        {
            if (_viewModel.SelectedSeason == null)
            {
                MessageBox.Show("Bitte wählen Sie eine Saison aus!", "Keine Saison ausgewählt",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            // Check if the user really want to delete the bettor
            var messageBoxResult = MessageBox.Show("Sind Sie sicher, dass die Saison gelöscht werden soll!", "Benutzer löschen",
                MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (messageBoxResult != MessageBoxResult.Yes) return;
            // Check if service is available
            if (!await AdminClientHelper.IsAvailable(_adminClient)) return;
            // delete bettor
            var isDeleted = await  _adminClient.DeleteSeasonAsync(_viewModel.SelectedSeason);
            if (isDeleted)
            {
                ReloadSeason();
            }
            else
            {
                MessageBox.Show("Saison konnte nicht gelöscht werden!", "Löschen fehlgeschlagen",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion
    }
}