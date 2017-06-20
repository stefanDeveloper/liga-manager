using System.Collections.Generic;
using System.Windows.Input;
using LigaManagerAdminClient.Framework;
using LigaManagerAdminClient.Models;
using LigaManagerServer.Models;

namespace LigaManagerAdminClient.ViewModels
{
    public class AddTeamWindowViewModel : ViewModelBase
    {
        public List<SeasonCheckBox> Seasons { get; set; }
        public Team Team { get; set; }
        public ICommand OkCommand { get; set; }
        public ICommand CancelCommand { get; set; }
    }
}