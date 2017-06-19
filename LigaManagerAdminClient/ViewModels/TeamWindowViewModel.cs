using System.Collections.Generic;
using System.Windows.Input;
using LigaManagerServer.Models;

namespace LigaManagerAdminClient.ViewModels
{
    public class TeamWindowViewModel
    {
        public List<SeasonToTeamRelation> Teams { get; set; }
        public SeasonToTeamRelation SelectedTeam { get; set; }
        public ICommand BackCommand { get; set; }
        public ICommand AddCommand { get; set; }
        public ICommand ChangeCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
    }
}