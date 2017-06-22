using System;
using System.Collections.Generic;
using System.Windows.Input;
using LigaManagerAdminClient.AdminClientService;
using LigaManagerAdminClient.Framework;

namespace LigaManagerAdminClient.ViewModels
{
    public class AddMatchWindowViewModel : ViewModelBase
    {
        public Match SelectedMatch { get; set; }
        public int Hour { get; set; }
        public int Minute { get; set; }
        public List<Team> AwayTeams { get; set; }
        public Team SelectedAwayTeam { get; set; }
        public List<Team> HomeTeams { get; set; }
        public Team SelectedHomeTeam { get; set; }
        public ICommand OkCommand { get; set; }
        public ICommand CancelCommand { get; set; }
    }
}   