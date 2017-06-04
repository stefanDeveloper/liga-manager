using System;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Web.Services.Description;

namespace LigaManagerServerConsole
{
    class WCFConsole
    {
        static void Main(string[] args)
        {
            var baseAddress = new Uri("http://localhost:80/LigaManagerServer/");
            // Create the ServiceHost.
            using (var serviceHost = new ServiceHost(typeof(Service), baseAddress))
            {
                // Enable metadata publishing.
                var smb = new ServiceMetadataBehavior
                {
                    HttpGetEnabled = true,
                    MetadataExporter = {PolicyVersion = PolicyVersion.Policy15}
                };
                serviceHost.Description.Behaviors.Add(smb);

                // Open the ServiceHost to start listening for messages. Since
                // no endpoints are explicitly configured, the runtime will create
                // one endpoint per base address for each service contract implemented
                // by the service.
                serviceHost.Open();

                Console.WriteLine("The service is ready at {0}", baseAddress);
                Console.WriteLine("Press <Enter> to stop the service.");
                Console.ReadLine();

                // Close the ServiceHost.
                serviceHost.Close();
            }
        }
    }
}
