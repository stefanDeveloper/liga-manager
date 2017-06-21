using System.Windows.Input;
using LigaManagerAdminClient.Framework;

namespace LigaManagerAdminClient.ViewModels
{
    public class MenuWindowViewModel : ViewModelBase
    {
        public ICommand BettorCommand { get; set; }
        public ICommand TeamCommand { get; set; }
        public ICommand SeasonCommand { get; set; }
        public ICommand MatchesCommand { get; set; }     
    }
}
    