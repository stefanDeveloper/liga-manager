using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Xml;
using System.Xml.Linq;
using LigaManagerAdminClient.AdminClientService;
using LigaManagerAdminClient.Framework;
using LigaManagerAdminClient.ViewModels;
using LigaManagerAdminClient.Views;
using LigaManagerBettorClient.Frameworks;
using Microsoft.Win32;

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
            // Check if service is available
            if (!await AdminClientHelper.IsAvailable(_adminClient)) return;
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
                GenerateMatchesCommand = new RelayCommand(ExecuteGenerateCommand),
                LoadMatchDayCommand = new RelayCommand(ExecuteLoadMatchCommand)
            };
            _view.DataContext = _viewModel;
            _viewModel.SelectionSeasonChanged += UpdateSeason;
            #endregion

            _view.MatchesDataGrid.Loaded += SetMinWidths;
            MainWindow.Content = _view;
        }

        public void SetMinWidths(object source, EventArgs e)
        {
            foreach (var column in _view.MatchesDataGrid.Columns)
            {
                column.MinWidth = column.ActualWidth;
                column.Width = new DataGridLength(1, DataGridLengthUnitType.Star);
            }
        }

        private async void ExecuteLoadMatchCommand(object obj)
        {
            // Create OpenFileDialog
            var dlg = new OpenFileDialog
            {
                DefaultExt = ".xml",
                Filter = "XML File (.xml)|*.xml",
                Multiselect = false
            };

            // Display OpenFileDialog by calling ShowDialog method
            bool? result = dlg.ShowDialog();

            // Get the selected file name and display in a TextBox
            if (result == true)
            {
                var setMatchDay = new SetMatchDayWindowController();
                var matchDay = setMatchDay.SetMatchDay();

                if (matchDay == -1) return;


                var isAdded = false;
                try
                {
                    // Open document
                    string filename = dlg.FileName;
                    XmlDocument booksFromFile = new XmlDocument();
                    booksFromFile.Load(dlg.InitialDirectory + dlg.FileName);
                    isAdded = await _adminClient.AddMatchDayAsync(
                        GetXElementFromXmlElement(booksFromFile.DocumentElement),
                        _viewModel.SelectedSeason, matchDay);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
                finally
                {
                    UpdateModels(isAdded, "Spieltag konnte nicht hinzugefügt werden! Die Datei entspricht nicht dem vorgegebenen Format!",
                        "Hinzufügen fehlgeschlagen");
                }
            }
        }

        public XElement GetXElementFromXmlElement(XmlElement xmlElement)
        {
            return XElement.Load(xmlElement.CreateNavigator().ReadSubtree());
        }

        private void UpdateSeason(object sender, Season e)
        {
            ReloadModels();
        }

        protected override async void ExecuteAddCommand(object obj)
        {
            var teams = await _adminClient.GetAllTeamsAsync();
            var addBettorWindow = new AddMatchWindowController
            {
                Match = new Match
                {
                    Season = _viewModel.SelectedSeason,
                    DateTime = DateTime.Now
                },
                AwayTeams = teams.ToList(),
                HomeTeams = teams.ToList(),
                
            };

            var showMatch = addBettorWindow.ShowMatch();
            // it could be possible that the bettor is null
            if (showMatch == null) return;
            // Check if service is available
            if (!await AdminClientHelper.IsAvailable(_adminClient)) return;
            // add bettor
            var isAdded = await _adminClient.AddMatchAsync(showMatch);
            UpdateModels(isAdded, "Spiel konnte nicht hinzugefügt werden!",
                "Hinzufügen fehlgeschlagen");
        }

        protected override async void ExecuteChangeCommand(object obj)
        {
            if (_viewModel.SelectedMatch == null)
            {
                MessageBox.Show("Bitte wählen Sie ein Spiel aus!", "Kein Spiel ausgewählt",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            // Check if service is available
            if (!await AdminClientHelper.IsAvailable(_adminClient)) return;
            var teams = await _adminClient.GetAllTeamsAsync();

            var addBettorWindow = new AddMatchWindowController
            {
                Match = _viewModel.SelectedMatch,
                AwayTeams = teams.ToList(),
                HomeTeams = teams.ToList()
            };

            var showMatch = addBettorWindow.ShowMatch();
            // it could be possible that the bettor is null
            if (showMatch == null)
            {
                ReloadModels();
                return;
            }
            // Check if service is available
            if (!await AdminClientHelper.IsAvailable(_adminClient)) return;
            // add bettor
            var isUpdated = await _adminClient.UpdateMatchAsync(showMatch);
            UpdateModels(isUpdated, "Das Spiel konnte nicht geändert werden!", "Änderung fehlgeschlagen");
        }

        protected override async void ExecuteDeleteCommand(object obj)
        {
            if (_viewModel.SelectedMatch == null)
            {
                MessageBox.Show("Bitte wählen Sie ein Spiel aus!", "Kein Spiel ausgewählt",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            // Check if the user really want to delete the bettor
            var messageBoxResult = MessageBox.Show("Sind Sie sicher, dass das Spiel gelöscht werden soll!",
                "Benutzer löschen",
                MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (messageBoxResult != MessageBoxResult.Yes) return;
            // Check if service is available
            if (!await AdminClientHelper.IsAvailable(_adminClient)) return;
            // delete bettor
            var isDeleted = _adminClient.DeleteMatch(_viewModel.SelectedMatch);
            UpdateModels(isDeleted, "Spiel konnte nicht gelöscht werden!", "Löschen fehlgeschlagen");
        }

        private async void ExecuteGenerateCommand(object obj)
        {
            var generateMatchesWindowController = new GenerateMatchesWindowController
            {
                Season = _viewModel.SelectedSeason,
                EndDate = DateTime.Now.AddDays(30),
                BeginDate = DateTime.Now
            };
            var isChoose = generateMatchesWindowController.ChooseDateIntervall();
            if (!isChoose) return;
            var isSuccess = await _adminClient.GenerateMatchesAsync(_viewModel.SelectedSeason, generateMatchesWindowController.BeginDate,
                generateMatchesWindowController.EndDate);
            if (isSuccess)
            {
                MessageBox.Show("Spiele wurden erfolgreich erstellt!", "Erfolgreich",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                ReloadModels();
            }
            else
            {
                MessageBox.Show("Ein Fehler ist aufgetreten, Spiele konnten nicht erstellt werden!", "Fehlgeschlagen",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                ReloadModels();
            }
        }

        protected override async void ReloadModels()
        {
            var matches = await _adminClient.GetMatchesAsync(_viewModel.SelectedSeason);
            _viewModel.Matches = matches.ToList();
        }
    }
}