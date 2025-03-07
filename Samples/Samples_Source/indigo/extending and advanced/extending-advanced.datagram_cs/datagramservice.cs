//
//  DatagramService.cs
//
//  Copyright (c) Microsoft Corporation.  All rights reserved.
//

using System;
using System.Collections;
using System.MessageBus;
using System.Xml;

namespace Microsoft.Samples.MessageBus.Quickstarts.Datagram
{
	/// <summary>
	/// The DatagramService class implements a simple service.  It listens
	/// for SOAP messages from DatagramClients and prints the bodies of 
	/// those messages to the console.  
	/// </summary>
	public class DatagramService
	{
		/// <summary>
		/// The service environment for the DatagramService.  Holds references
		/// to our Port and other interesting things.
		/// </summary>
		private ServiceEnvironment environment;

		/// <summary>
		/// A dispatcher that holds mappings from message actions to
		/// message handlers.  This is used to "route" Messages to 
		/// MessageHandlers based upon the Message's Action. 
		/// </summary>
		private MessageDispatcher dispatcher;

		/// <summary>
		/// The entry-point for the DatagramService.  Starts up an instance of the service.
		/// </summary>
		[STAThread]
		public static void Main()
		{	
			DatagramService service = new DatagramService();
			
			try
			{
				service.Run();
			}
			catch (PortIOException)
			{
				Console.WriteLine("Error: Could not open the Port.");
				Environment.Exit(-1);
			}
		}

		/// <summary>
		/// Instantiates an instance of the DatagramService.  This loads our service 
		/// environment from config, configures our Port, and populates our dispatcher.
		/// </summary>
		public DatagramService()
		{
			// Load our ServiceEnvironment ("DatagramService" -- see DatagramService.exe.config)
			this.environment = ServiceEnvironment.Load("DatagramService");

			// Get a reference to our ServiceEnvironment's port
			Port port = (Port) environment[typeof(Port)];

			// Create a MessageDispatcher.  The MessageDispatcher will be registered with the
			// ReceiveChannel and will handle dispatching Messages to IMessageHandlers based
			// upon Message.Action.  We need to do three things:
			//     + Create a MessageDispatcher
			//     + Create a Filter for each type of Message we're interested in.
			//     + Hook-up a MessageHandler for each Filter.
			
			// Create and hook-up our MessageDispatcher
			this.dispatcher = new MessageDispatcher();
			port.ReceiveChannel.Handler = this.dispatcher;

			// Construct the Filtering code necessary to switch based upon Message.Action.
	        XmlNamespaceManager namespaceManager = new XmlNamespaceManager(new NameTable());
			namespaceManager.AddNamespace("env", "http://www.w3.org/2001/12/soap-envelope");
			namespaceManager.AddNamespace("wsa", "http://schemas.xmlsoap.org/ws/2002/12/addressing");

			// Register our handlers with the MessageDispatcher        
			XPathFilter datagramMessageFilter = new XPathFilter("/env:Envelope/env:Header/wsa:Action='" + DatagramMessageHandler.Action + "'", namespaceManager);
			this.dispatcher.Add(datagramMessageFilter, new DatagramMessageHandler(port));
		}

		/// <summary>
		/// Start our application.  This will open the service environment and start
		/// listening for messages.  This method does not actually process messages --
		/// that task is handled by MessageDispatcher and the IMessageHandlers.
		/// </summary>
		public void Run()
		{
			try
			{
				// Open the environment.  This causes us to start listening for messages.
				this.environment.Open();
			}
			catch (AggregateException e)
			{
				Exception[] exceptions = e.GetExceptions();
				throw exceptions[0];
			}

			// Start listening for requests.
			Console.WriteLine("DatagramService now listening on {0}.", ((Port) this.environment[typeof(Port)]).IdentityRole);
			Console.WriteLine("Press <ENTER> to exit.");
			Console.ReadLine();

			// Close our port, since we no longer need it.
			this.environment.Close();
		}

		/// <summary>
		/// Message handler for our app-level messages.  If a message arrives w/
		/// the DatagramMessageHandler.Action action, it will be processed by this
		/// handler.
		/// </summary>
		private class DatagramMessageHandler : SyncMessageHandler
		{
			/// <summary>
			/// Reference to the handler's port.
			/// </summary>
			private Port port;

			/// <summary>
			/// Constructs an instance of the app's echo request message handler.  base(false) 
			/// indicates that ProcessMessage will not block on processing the message.  This
			/// is a performance hint to the message dispatching code.
			/// </summary>
			/// <param name="enviroment">Reference to the handler's port.</param>
			public DatagramMessageHandler(Port port) : base(false)
			{
				this.port = port;
			}

			/// <summary>
			/// The SOAP action upon which the DatagramService acts.
			/// </summary>
			public static Uri Action
			{
				get
				{
					return new Uri("http://schemas.microsoft.com/MB/2003/06/Samples/CoreMessaging/Datagram");
				}
			}

			/// <summary>
			/// This is the message handler for our app messages.  We read the body of 
			/// this message and print it out to the console.
			/// </summary>
			/// <param name="message">The message that was received.</param>
			/// <returns>Returns true because we've finished processing the message.</returns>
			public override bool ProcessMessage(Message message)
			{
				// Read the message body as a string.
				string text = (string) message.Content.GetObject(typeof(string));
	            
				// Write it out to the console.
				Console.WriteLine(text);

				// Return "true," indicating that we're finished looking at the message.
				return true;
			}
		}
	}
}