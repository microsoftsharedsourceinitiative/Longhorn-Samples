using System;
using System.MessageBus;
using System.MessageBus.Services;
using System.MessageBus.Security;

[assembly:CLSCompliant(true)]
[assembly:System.Runtime.InteropServices.ComVisible(true)]

namespace Microsoft.Samples.MessageBus.QuickStarts 
{
    [DatagramPortType(Name="Hello",Namespace="http://www.tempuri.org/quickstarts")]
    public class Hello
    {
        [ServiceSecurityAttribute(Name = "StandardScope", Role = "mbse:AnonymousUser", Confidentiality = false)]
        [ServiceMethod]
        public string Greeting(string name)
        {	
            Console.WriteLine("Called by a client with the name {0}", name);
            return String.Format("Hello, {0}!", name);
        }

    }
}
