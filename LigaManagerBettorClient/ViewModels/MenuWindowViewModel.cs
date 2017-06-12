using System.Collections.Generic;
using System.Windows.Input;
using LigaManagerBettorClient.Frameworks;
using LigaManagerServer.Models;

namespace LigaManagerBettorClient.ViewModels
{
    public class MenuWindowViewModel : ViewModelBase
    {
        public Bettor Bettor { get; set; }
        public List<Season> Seasons { get; set; }
        public Season SelecetedSeason { get; set; }
        public ICommand MatchesCommand { get; set; }
        public ICommand TeamsCommand { get; set; }
        public ICommand BettorRankingCommand { get; set; }  
    }
}