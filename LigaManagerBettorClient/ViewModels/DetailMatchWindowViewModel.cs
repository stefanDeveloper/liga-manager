using LigaManagerBettorClient.BettorClientService;
using LigaManagerBettorClient.Frameworks;

namespace LigaManagerBettorClient.ViewModels
{
    public class DetailMatchWindowViewModel : ViewModelBase
    {
        public Match Match { get; set; }    
        public RelayCommand BetCommand { get; set; }
        public RelayCommand CancelCommand { get; set; }
        public Bet Bet { get; set; }
    }
}