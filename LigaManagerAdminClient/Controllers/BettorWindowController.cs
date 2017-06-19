using System;
using System.Linq;
using System.Windows;
using LigaManagerAdminClient.AdminClientService;
using LigaManagerAdminClient.Framework;
using LigaManagerAdminClient.ViewModels;
using LigaManagerAdminClient.Views;
using LigaManagerBettorClient.Frameworks;
using Bettor = LigaManagerServer.Models.Bettor;

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
            // add bettor
            var addBettorAsync = await _adminClient.AddBettorAsync(showBettor);

            if (addBettorAsync)
            {

            }
            else
            {
                
            }
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
            // add bettor
            var addBettorAsync = await _adminClient.UpdateBettorAsync(showBettor);

            if (addBettorAsync)
            {

            }
            else
            {

            }
        }

        private void ExecuteDeleteCommand(object obj)
        {
            // Check if the user really want to delete the bettor
            var messageBoxResult = MessageBox.Show("Sind Sie sicher, dass der Benutzer gelöscht werden soll!", "Benutzer löschen",
                MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (messageBoxResult != MessageBoxResult.Yes) return;

            var allBets = _adminClient.GetAllBets();
            var findAll = allBets.ToList().FindAll(x => x.DateTime > DateTime.Now && x.Bettor.Equals(_viewModel.SelectedBettor));
            // if the user has any current bets, it's not possible to delete him
            if (findAll.Count != 0)
            {
                MessageBox.Show("Der Benutzer konnte nicht gelöscht werden, da noch Tipps vorhanden sind!", "Benutzer löschen",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            } 
            _adminClient.DeleteBettor(_viewModel.SelectedBettor);
        }
        #endregion
    }
}