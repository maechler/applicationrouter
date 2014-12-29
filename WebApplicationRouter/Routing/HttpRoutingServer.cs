using System;
using System.Threading;
using System.Collections.Generic;
using System.Net;

namespace Marx.Net
{
	public class HttpRoutingServer
	{
		internal HttpListener Listener { get; set; }

		public RoutingTable RoutingTable { get; set; }

		public HttpRoutingServer () : this (new RoutingTable ())
		{
		}

		public HttpRoutingServer (RoutingTable routingTable)
		{
			Listener = new HttpListener ();
			Listener.Prefixes.Add ("http://*:8080/");
			RoutingTable = routingTable;
		}

		public void Start ()
		{
			Listener.Start ();
			while (Listener.IsListening) {
				var router = new HttpRouter ();
				Listener.BeginGetContext (router.HandleExternalRequest, this);
			}
		}

		public void Stop ()
		{
			Listener.Stop ();
		}
	}
}

