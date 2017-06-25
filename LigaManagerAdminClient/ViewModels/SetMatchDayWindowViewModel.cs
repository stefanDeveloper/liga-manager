using System.Windows.Input;
using LigaManagerAdminClient.Framework;

namespace LigaManagerAdminClient.ViewModels
{
    public class SetMatchDayWindowViewModel : ViewModelBase
    {
        public int MatchDay { get; set; }
        public ICommand OkCommand { get; set; }
        public ICommand CancelCommand { get; set; }
    }
}