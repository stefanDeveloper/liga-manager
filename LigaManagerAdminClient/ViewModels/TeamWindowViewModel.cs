using System.Collections.Generic;
using System.Windows.Input;
using LigaManagerAdminClient.Framework;
using LigaManagerServer.Models;

namespace LigaManagerAdminClient.ViewModels
{
    public class TeamWindowViewModel : ViewModelBase
    {
        public List<Team> Teams { get; set; }
        public Team SelectedTeam { get; set; }
        public ICommand BackCommand { get; set; }
        public ICommand AddCommand { get; set; }
        public ICommand ChangeCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
    }
}