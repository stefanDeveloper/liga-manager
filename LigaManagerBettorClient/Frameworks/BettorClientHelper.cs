using System;
using System.ServiceModel;
using System.Threading.Tasks;
using System.Windows;
using LigaManagerBettorClient.BettorClientService;

namespace LigaManagerBettorClient.Frameworks
{
    public class BettorClientHelper
    {
        public static async Task<bool> IsAvailable(BettorClientServiceClient bettorClient)
        {
            try
            {
                await bettorClient.IsOpenAsync();
                return true;
            }
            catch (EndpointNotFoundException e)
            {
                Console.WriteLine(e);
                MessageBox.Show("Konnte keine Verbindung mit dem Service herstellen!", "Verbindung fehlgeschlagen",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }
    }
}