using System.Linq;
using System.Windows;
using LigaManagerAdminClient.AdminClientService;
using LigaManagerAdminClient.Framework;
using LigaManagerAdminClient.ViewModels;
using LigaManagerAdminClient.Views;
using LigaManagerBettorClient.Frameworks;

namespace LigaManagerAdminClient.Controllers
{
    public class MatchesWindowController : AbstractListWindowController
    {
        private MatchesWindow _view;
        private MatchesWindowViewModel _viewModel;
        private AdminClientServiceClient _adminClient;
        
        public override async void Initialize(MainWindow mainWindow)
        {
            _adminClient = new AdminClientServiceClient();
            MainWindow = mainWindow;

            #region View And ViewModel
            _view = new MatchesWindow();
            var seasons = await _adminClient.GetSeasonsAsync();
            var matches = await _adminClient.GetMatchesAsync(seasons.FirstOrDefault());
            _viewModel = new MatchesWindowViewModel
            {
                Seasons = seasons.ToList(),
                SelectedSeason = seasons.FirstOrDefault(),
                Matches = matches.ToList(),
                BackCommand = new RelayCommand(ExecuteBackCommand),
                AddCommand = new RelayCommand(ExecuteAddCommand),
                DeleteCommand = new RelayCommand(ExecuteDeleteCommand),
                ChangeCommand = new RelayCommand(ExecuteChangeCommand),
            };
            _view.DataContext = _viewModel;
            #endregion

            MainWindow.Content = _view;
        }

        protected override async void ExecuteAddCommand(object obj)
        {
            var addBettorWindow = new AddBettorWindowController
            {
                Bettor = new Bettor()
            };

            var showBettor = addBettorWindow.ShowBettor();
            // it could be possible that the bettor is null
            if (showBettor == null) return;
            // Check if service is available
            if (!await AdminClientHelper.IsAvailable(_adminClient)) return;
            // add bettor
            var isAdded = await _adminClient.AddBettorAsync(showBettor);
            UpdateModels(isAdded, "Tipper konnte nicht hinzugefügt werden, da der Nickname schon vergeben ist!",
                "Hinzufügen fehlgeschlagen");
        }

        protected override async void ExecuteChangeCommand(object obj)
        {
            if (_viewModel.SelectedMatch == null)
            {
                MessageBox.Show("Bitte wählen Sie einen Tipper aus!", "Kein Tipper ausgewählt",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var addBettorWindow = new AddBettorWindowController
            {
                Bettor = _viewModel.SelectedBettor
            };

            var showBettor = addBettorWindow.ShowBettor();
            // it could be possible that the bettor is null
            if (showBettor == null) return;
            // Check if service is available
            if (!await AdminClientHelper.IsAvailable(_adminClient)) return;
            // add bettor
            var isUpdated = await _adminClient.UpdateBettorAsync(showBettor);
            UpdateModels(isUpdated, "Der Benutzer konnte nicht geändert werden!", "Änderung fehlgeschlagen");
        }

        protected override async void ExecuteDeleteCommand(object obj)
        {
            if (_viewModel.SelectedMatch == null)
            {
                MessageBox.Show("Bitte wählen Sie einen Tipper aus!", "Kein Tipper ausgewählt",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            // Check if the user really want to delete the bettor
            var messageBoxResult = MessageBox.Show("Sind Sie sicher, dass der Benutzer gelöscht werden soll!",
                "Benutzer löschen",
                MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (messageBoxResult != MessageBoxResult.Yes) return;
            // Check if service is available
            if (!await AdminClientHelper.IsAvailable(_adminClient)) return;
            // delete bettor
            var isDeleted = _adminClient.DeleteBettor(_viewModel.SelectedBettor);
            UpdateModels(isDeleted, "Tipper konnte nicht gelöscht werden!", "Löschen fehlgeschlagen");
        }

        protected override async void ReloadModels()
        {
            var matches = await _adminClient.GetMatchesAsync(_viewModel.SelectedSeason);
            _viewModel.Matches = matches.ToList();
        }
    }
}