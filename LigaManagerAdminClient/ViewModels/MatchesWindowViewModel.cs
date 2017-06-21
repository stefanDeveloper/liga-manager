using System.Collections.Generic;
using System.Windows.Input;
using LigaManagerAdminClient.AdminClientService;
using Match = System.Text.RegularExpressions.Match;

namespace LigaManagerAdminClient.ViewModels
{
    public class MatchesWindowViewModel
    {
        public List<Match> Matches { get; set; }
        public Match SelectedMatch { get; set; }
        public Season SelectedSeason { get; set; }
        public List<Season> Seasons { get; set; }
        public ICommand BackCommand { get; set; }
        public ICommand AddCommand { get; set; }
        public ICommand ChangeCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
    }
}