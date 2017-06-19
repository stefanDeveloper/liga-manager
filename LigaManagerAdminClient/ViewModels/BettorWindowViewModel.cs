using System.Collections.Generic;
using System.Windows.Input;
using LigaManagerBettorClient.Frameworks;
using LigaManagerServer.Models;

namespace LigaManagerAdminClient.ViewModels
{
    public class BettorWindowViewModel : ViewModelBase
    {
        public List<Bettor> Bettors { get; set; }
        public Bettor SelectedBettor { get; set; }
        public ICommand BackCommand { get; set; }
        public ICommand AddCommand { get; set; }
        public ICommand ChangeCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
    }   
}