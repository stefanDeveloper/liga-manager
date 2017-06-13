using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using LigaManagerBettorClient.Frameworks;
using LigaManagerBettorClient.Models;

namespace LigaManagerBettorClient.ViewModels
{
    public class TeamRankingWindowViewModel : ViewModelBase
    {
        public List<RankedTeam> Teams { get; set; }
        public event EventHandler<string> InnerButtonClick;
        private string _selectedMatchDay;
        public string SelectedMatchDay
        {
            get => _selectedMatchDay;
            set
            {
                if (_selectedMatchDay != value)
                {
                    _selectedMatchDay = value;
                    InnerButtonClick?.Invoke(this, _selectedMatchDay);
                }
            }
        }
        public ObservableCollection<string> MatchDays { get; set; }
    }
}