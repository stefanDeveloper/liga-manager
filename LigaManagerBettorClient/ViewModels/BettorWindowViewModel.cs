using System.Collections.Generic;
using LigaManagerBettorClient.Frameworks;
using LigaManagerServer.Models;

namespace LigaManagerBettorClient.ViewModels
{
    public class BettorWindowViewModel : ViewModelBase
    {
        public List<Bettor> Bettors { get; set; }
    }
}