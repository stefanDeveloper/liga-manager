using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using LigaManagerBettorClient.BettorClientService;
using LigaManagerBettorClient.Frameworks;

namespace LigaManagerBettorClient.ViewModels
{
    public class MenuWindowViewModel : ViewModelBase
    {
        public Bettor Bettor { get; set; }
        public ICommand MatchesCommand { get; set; }
        public ICommand TeamsCommand { get; set; }
        public ICommand BettorRankingCommand { get; set; }
        

        public event EventHandler<Season> SelectionMatchDayChanged;
        private Season _selectedSeason;
        public Season SelectedSeason
        {
            get { return _selectedSeason; }
            set
            {
                if (_selectedSeason != value)
                {
                    _selectedSeason = value;
                    SelectionMatchDayChanged?.Invoke(this, _selectedSeason);
                }
            }
        }

        public ObservableCollection<Season> Seasons { get; set; }
    }
}