using System;
using System.ServiceModel;
using System.Threading.Tasks;
using System.Windows;
using LigaManagerAdminClient.AdminClientService;

namespace LigaManagerAdminClient.Framework
{
    public class AdminClientHelper
    {
        public static async Task<bool> IsAvailable(AdminClientServiceClient bettorClient)
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