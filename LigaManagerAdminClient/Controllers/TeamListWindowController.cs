using System.Collections.Generic;
using System.Linq;
using System.Windows;
using LigaManagerAdminClient.AdminClientService;
using LigaManagerAdminClient.Framework;
using LigaManagerAdminClient.Models;
using LigaManagerAdminClient.ViewModels;
using LigaManagerAdminClient.Views;
using LigaManagerBettorClient.Frameworks;

namespace LigaManagerAdminClient.Controllers
{
    public class TeamListWindowController : AbstractListWindowController
    {
        private TeamWindow _view;
        private TeamWindowViewModel _viewModel;
        private AdminClientServiceClient _adminClient;

        public override async void Initialize(MainWindow mainWindow)
        {
            MainWindow = mainWindow;
            _view = new TeamWindow();
            _adminClient = new AdminClientServiceClient();

            #region View and ViewModel
            // Check if service is available
            if (!await AdminClientHelper.IsAvailable(_adminClient)) return;

            var teams = await _adminClient.GetAllTeamsAsync();
            _viewModel = new TeamWindowViewModel
            {
                Teams = teams.ToList(),
                BackCommand = new RelayCommand(ExecuteBackCommand),
                AddCommand = new RelayCommand(ExecuteAddCommand),
                DeleteCommand = new RelayCommand(ExecuteDeleteCommand),
                ChangeCommand = new RelayCommand(ExecuteChangeCommand)
            };

            _view.DataContext = _viewModel;
            #endregion

            MainWindow.Content = _view;
        }

        #region ExecuteCommands
        protected override async void ExecuteAddCommand(object obj)
        {
            // Add Season to choose
            var seasons = await _adminClient.GetSeasonsAsync();
            var result = new List<SeasonCheckBox>();
            seasons.ToList().ForEach(x =>
            {
                result.Add(new SeasonCheckBox
                {
                    Season = x,
                    // Default is null
                    IsAdded = false
                });
            });
            var addBettorWindow = new AddTeamWindowController
            {
                Team = new Team(),
                Seasons = result
            };

            var showTeam = addBettorWindow.ShowTeam();
            // it could be possible that the bettor is null
            if (showTeam == null) return;
            // Check if service is available
            if (!await AdminClientHelper.IsAvailable(_adminClient)) return;
            // add bettor
            var isAdded = await _adminClient.AddTeamAsync(showTeam);
            var addedSeasons = addBettorWindow.Seasons.FindAll(x => x.IsAdded);
            var allTeamsAsync = await _adminClient.GetAllTeamsAsync();
            var team = allTeamsAsync.ToList().Find(x => x.Name.ToUpper().Equals(showTeam.Name.ToUpper()));
            if (addedSeasons.Any())
            {
                addedSeasons.ToList().ForEach(x => _adminClient.AddSeasonToTeamRelation(new SeasonToTeamRelation { Team = team, Season = x.Season }));
            }
            UpdateModels(isAdded, "Mannschaft konnte nicht hinzugefügt werden, da der Name schon vergeben ist!", "Hinzufügen fehlgeschlagen");
        }

        protected override async void ExecuteChangeCommand(object obj)
        {
            if (_viewModel.SelectedTeam == null)
            {
                MessageBox.Show("Bitte wählen Sie eine Mannschaft aus!", "Keine Mannschaft ausgewählt",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            // Check if service is available
            if (!await AdminClientHelper.IsAvailable(_adminClient)) return;
            var seasons = await _adminClient.GetSeasonsAsync();
            var seasonToTeamRelation = await _adminClient.GetAllSeasonToTeamRelationAsync();
            var result = new List<SeasonCheckBox>();
            seasons.ToList().ForEach(x =>
            {
                result.Add(new SeasonCheckBox
                {
                    Season = x,
                    IsAdded = seasonToTeamRelation.Any(y => y.Season.Name.ToUpper().Equals(x.Name.ToUpper()) && y.Team.Name.ToUpper().Equals(_viewModel.SelectedTeam.Name.ToUpper()) )
                });
            });
            var addBettorWindow = new AddTeamWindowController
            {
                Team = _viewModel.SelectedTeam,
                Seasons = result
            };

            var showTeam = addBettorWindow.ShowTeam();
            // it could be possible that the bettor is null
            if (showTeam == null)
            {
                ReloadModels();
                return;
            }
            // Check if service is available
            if (!await AdminClientHelper.IsAvailable(_adminClient)) return;
            // add bettor
            var isAdded = await _adminClient.UpdateTeamAsync(showTeam);
            var allTeamsAsync = await _adminClient.GetAllTeamsAsync();
            var team = allTeamsAsync.ToList().Find(x => x.Name.ToUpper().Equals(showTeam.Name.ToUpper()));
            addBettorWindow.Seasons.ForEach(x =>
            {
                if (x.IsAdded)
                {
                    _adminClient.AddSeasonToTeamRelation(new SeasonToTeamRelation { Team = team, Season = x.Season });
                }
                
                else
                {
                    _adminClient.DeleteSeasonToTeamRelation(new SeasonToTeamRelation { Team = team, Season = x.Season });
                }
            });
            UpdateModels(isAdded, "Die Mannschaft konnte nicht geändert werden!", "Änderung fehlgeschlagen");
        }

        protected override async void ExecuteDeleteCommand(object obj)
        {
            if (_viewModel.SelectedTeam == null)
            {
                MessageBox.Show("Bitte wählen Sie eine Mannschaft aus!", "Keine Mannschaft ausgewählt",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            // Check if the user really want to delete the bettor
            var messageBoxResult = MessageBox.Show("Sind Sie sicher, dass die Mannschaft gelöscht werden soll!", "Mannschaft löschen",
                MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (messageBoxResult != MessageBoxResult.Yes) return;
            // Check if service is available
            if (!await AdminClientHelper.IsAvailable(_adminClient)) return;
            // delete bettor
            var isDeleted = _adminClient.DeleteTeam(_viewModel.SelectedTeam);
            UpdateModels(isDeleted, "Mannschaft konnte nicht gelöscht werden!", "Löschen fehlgeschlagen");
        }
        #endregion

        protected override async void ReloadModels()
        {
            var teams = await _adminClient.GetAllTeamsAsync();
            _viewModel.Teams = teams.ToList();
        }
    }
}