using System.Collections.Generic;
using System.Windows.Data;
using System.Windows.Input;
using LigaManagerBettorClient.Frameworks;
using LigaManagerServer.Models;

namespace LigaManagerBettorClient.ViewModels
{
    public class MatchesWindowViewModel : ViewModelBase
    {
        public Season SelectedSeason { get; set; }  
        public ListCollectionView Matches { get; set; }
        public Match SelectedMatch { get; set; }
        public ICommand SelectedMatchCommand { get; set; }  
    }
}