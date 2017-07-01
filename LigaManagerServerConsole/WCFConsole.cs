using System;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Web.Services.Description;
using LigaManagerServer.Contracts;
using LigaManagerServer.Services;

namespace LigaManagerServerConsole
{
    public class WcfConsole
    {
        public static void Main(string[] args)
        {
            var bettorUrl = new Uri("http://localhost:80/BettorClientService/");
            var adminUrl = new Uri("http://localhost:80/AdminClientService/");
            // Create the ServiceHost.
            using (var bettorHost = new ServiceHost(typeof(BettorClientService), bettorUrl))
            using (var adminHost = new ServiceHost(typeof(BettorClientService), adminUrl))
            {
                // Enable metadata publishing.
                var smb = new ServiceMetadataBehavior
                {
                    HttpGetEnabled = true,
                    MetadataExporter = { PolicyVersion = PolicyVersion.Policy15 }
                };
                bettorHost.Description.Behaviors.Add(smb);
                adminHost.Description.Behaviors.Add(smb);


                // Open the ServiceHost to start listening for messages. Since
                // no endpoints are explicitly configured, the runtime will create
                // one endpoint per base address for each service contract implemented
                // by the service.
                bettorHost.Open();
                adminHost.Open();

                Console.WriteLine("The service is ready at {0}", bettorUrl);
                Console.WriteLine("The service is ready at {0}", adminUrl);
                Console.WriteLine("Press <Enter> to stop the service.");
                Console.ReadLine();

                // Close the ServiceHost.
                bettorHost.Close();
                adminHost.Open();
            }
        }
    }
}
