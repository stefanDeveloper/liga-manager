using System;
using System.Windows.Input;
using LigaManagerAdminClient.AdminClientService;
using LigaManagerAdminClient.Framework;

namespace LigaManagerAdminClient.ViewModels
{
    public class GenerateMatchesWindowViewModel : ViewModelBase
    {
        public DateTime SelectedBeginDate { get; set; }
        public DateTime SelectedEndDate { get; set; }
        public Season SelectedSeason { get; set; }
        public ICommand OkCommand { get; set; }
        public ICommand CancelCommand { get; set; }
    }
}