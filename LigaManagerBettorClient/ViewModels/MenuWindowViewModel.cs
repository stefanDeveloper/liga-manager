using System.Windows.Input;
using LigaManagerBettorClient.Frameworks;

namespace LigaManagerBettorClient.ViewModels
{
    public class MenuWindowViewModel : ViewModelBase
    {
        public ICommand MatchesCommand { get; set; }
        public ICommand TeamsCommand { get; set; }
        public ICommand BettorRankingCommand { get; set; }  
    }
}