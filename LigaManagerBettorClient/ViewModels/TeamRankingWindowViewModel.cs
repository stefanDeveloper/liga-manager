using System.Collections.Generic;
using LigaManagerBettorClient.Frameworks;
using LigaManagerBettorClient.Models;

namespace LigaManagerBettorClient.ViewModels
{
    public class TeamRankingWindowViewModel : ViewModelBase
    {
        public List<RankedTeam> Teams { get; set; }
    }
}