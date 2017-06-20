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
    public class BettorWindowController
    {
        private BettorWindow _view;
        private BettorWindowViewModel _viewModel;
        private MainWindow _mainWindow;
        private AdminClientServiceClient _adminClient;

        public async void Initialize(MainWindow mainWindow)
        {
            _mainWindow = mainWindow;
            _adminClient = new AdminClientServiceClient();
            // Check if service is available
            if (!await AdminClientHelper.IsAvailable(_adminClient)) return;
            // Get all bettors
            var bettors = _adminClient.GetBettors();

            #region View and ViewModel
            _view = new BettorWindow();
            _viewModel = new BettorWindowViewModel
            {
                BackCommand = new RelayCommand(ExecuteBackCommand),
                AddCommand = new RelayCommand(ExecuteAddCommand),
                DeleteCommand = new RelayCommand(ExecuteDeleteCommand),
                ChangeCommand = new RelayCommand(ExecuteChangeCommand),
                Bettors = bettors.ToList()
            };

            _view.DataContext = _viewModel;
            #endregion

            mainWindow.Content = _view;
        }
        #region ExecuteCommands
        private void ExecuteBackCommand(object obj)
        {
            var menuWindow = new MenuWindowController();
            menuWindow.Initialize(_mainWindow);
        }

        private async void ExecuteAddCommand(object obj)
        {
            var addBettorWindow = new AddBettorWindowController
            {
                Bettor =  new Bettor()
            };

            var showBettor = addBettorWindow.ShowBettor();
            // it could be possible that the bettor is null
            if (showBettor == null) return;
            // Check if service is available
            if (!await AdminClientHelper.IsAvailable(_adminClient)) return;
            // add bettor
            var addBettorAsync = await _adminClient.AddBettorAsync(showBettor);

            if (addBettorAsync)
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
            var bettors = await _adminClient.GetBettorsAsync();
            _viewModel.Bettors = bettors.ToList();
        }

        private async void ExecuteChangeCommand(object obj)
        {
            if (_viewModel.SelectedBettor == null)
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
            var addBettorAsync = await _adminClient.UpdateBettorAsync(showBettor);

            if (addBettorAsync)
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
            if (_viewModel.SelectedBettor == null)
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
            var isDeleted = _adminClient.DeleteBettor(_viewModel.SelectedBettor);
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