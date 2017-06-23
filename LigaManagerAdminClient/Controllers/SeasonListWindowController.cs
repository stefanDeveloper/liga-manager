using System.Linq;
using System.Windows;
using LigaManagerAdminClient.AdminClientService;
using LigaManagerAdminClient.Framework;
using LigaManagerAdminClient.ViewModels;
using LigaManagerAdminClient.Views;
using LigaManagerBettorClient.Frameworks;

namespace LigaManagerAdminClient.Controllers
{
    public class SeasonListWindowController : AbstractListWindowController
    {
        private SeasonWindow _view;
        private SeasonWindowViewModel _viewModel;
        private AdminClientServiceClient _adminClient;


        public override async void Initialize(MainWindow mainWindow)
        {
            MainWindow = mainWindow;
            _adminClient = new AdminClientServiceClient();

            #region View And ViewModel
            // Check if service is available
            if (!await AdminClientHelper.IsAvailable(_adminClient)) return;
            _view = new SeasonWindow();
            var seasons = await _adminClient.GetSeasonsAsync();
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

            MainWindow.Content = _view;
        }

        #region ExecuteCommands
        protected override async void ExecuteAddCommand(object obj)
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
            var isAdded = await _adminClient.AddSeasonAsync(showSeason);
            UpdateModels(isAdded, "Saison konnte nicht hinzugefügt werden, da der Name schon vergeben ist!",
                "Hinzufügen fehlgeschlagen");
        }

        protected override async void ReloadModels()
        {
            var seasons = await _adminClient.GetSeasonsAsync();
            _viewModel.Seasons = seasons.ToList();
        }

        protected override async void ExecuteChangeCommand(object obj)
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
            if (showSeason == null)
            {
                ReloadModels();
                return;
            }
            // Check if service is available
            if (!await AdminClientHelper.IsAvailable(_adminClient)) return;
            // add bettor
            var isUpdated = await _adminClient.UpdateSeasonAsync(showSeason);
            UpdateModels(isUpdated, "Die Saison konnte nicht geändert werden!", "Änderung fehlgeschlagen");
        }


        protected override async void ExecuteDeleteCommand(object obj)
        {
            if (_viewModel.SelectedSeason == null)
            {
                MessageBox.Show("Bitte wählen Sie eine Saison aus!", "Keine Saison ausgewählt",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            // Check if the user really want to delete the bettor
            var messageBoxResult = MessageBox.Show("Sind Sie sicher, dass die Saison gelöscht werden soll!",
                "Benutzer löschen",
                MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (messageBoxResult != MessageBoxResult.Yes) return;
            // Check if service is available
            if (!await AdminClientHelper.IsAvailable(_adminClient)) return;
            // delete bettor
            var isDeleted = await _adminClient.DeleteSeasonAsync(_viewModel.SelectedSeason);
            UpdateModels(isDeleted, "Saison konnte nicht gelöscht werden!", "Löschen fehlgeschlagen");
        }
        #endregion
    }
}