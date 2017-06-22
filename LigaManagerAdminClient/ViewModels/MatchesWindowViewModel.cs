using System;
using System.Collections.Generic;
using System.Windows.Input;
using LigaManagerAdminClient.AdminClientService;
using LigaManagerAdminClient.Framework;
using LigaManagerBettorClient.Frameworks;

namespace LigaManagerAdminClient.ViewModels
{
    public class MatchesWindowViewModel : ViewModelBase
    {
        public List<Match> Matches { get; set; }
        public Match SelectedMatch { get; set; }
        public List<Season> Seasons { get; set; }
        public ICommand BackCommand { get; set; }
        public ICommand AddCommand { get; set; }
        public ICommand ChangeCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        private Season _selectedSeason;
        public event EventHandler<Season> SelectionSeasonChanged;
        public Season SelectedSeason
        {
            get
            {
                return _selectedSeason;
            }
            set
            {
                if (_selectedSeason != value)
                {
                    _selectedSeason = value;
                    SelectionSeasonChanged?.Invoke(this, _selectedSeason);
                }
            }
        }

        public ICommand GenerateMatchesCommand { get; set; }
    }
}