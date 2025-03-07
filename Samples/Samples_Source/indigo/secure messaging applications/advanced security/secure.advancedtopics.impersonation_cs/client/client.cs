using System;
using System.MessageBus;
using System.MessageBus.Services;
// the namespace of the imported service interface
using www_tempuri_org.quickstarts;

[assembly:CLSCompliant(true)]
[assembly:System.Runtime.InteropServices.ComVisible(true)]

namespace Microsoft.Samples.MessageBus.QuickStarts 
{
    class Client 
    {
        public static void Main(string[] args) 
        {
            string name = "Client";
            if (args.Length > 0)
                name = args[0];

            // Load the default service environment, called "main".
            ServiceEnvironment se = ServiceEnvironment.Load();

            // Retrieve the ServiceManager from the default environment
            ServiceManager manager = se[typeof(ServiceManager)] as ServiceManager;
            if (manager == null)
            {
                throw new ApplicationException("The ServiceManager is not available in the service se for some reason."	);
            }
            // Start the service environment.
            se.Open();

            // Create a proxy channel that points to the service to call.
            Uri uri = new Uri("soap.tcp://localhost:46000/ImpersonationTestService/");
            IImpersonationTestChannel channel = (IImpersonationTestChannel)manager.CreateChannel(typeof(IImpersonationTestChannel), uri);

            try 
            {
                ResponseMessage response = channel.DoTest(new StartMessage(name));

                Console.WriteLine(response.Response);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                se.Close();
            }
        }

    }
}
