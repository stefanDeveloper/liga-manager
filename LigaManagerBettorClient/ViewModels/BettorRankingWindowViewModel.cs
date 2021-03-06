﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using LigaManagerBettorClient.BettorClientService;
using LigaManagerBettorClient.Frameworks;

namespace LigaManagerBettorClient.ViewModels
{
    public class BettorRankingWindowViewModel : ViewModelBase
    {
        public List<RankedBettor> Bettors { get; set; }
        public ICommand BackCommand { get; set; }
        public ObservableCollection<string> MatchDays { get; set; }
        
        private string _selectedMatchDay;
        public string SelectedMatchDay
        {
            get { return _selectedMatchDay; }
            set
            {
                if (_selectedMatchDay != value)
                {
                    _selectedMatchDay = value;
                    SelectionMatchDayChanged?.Invoke(this, _selectedMatchDay);
                }
            }
        }
        public event EventHandler<string> SelectionMatchDayChanged;

    }
}