using System;
using System.Threading;
using System.Net;

namespace Marx.Net
{
	class HttpRouter
	{
		HttpRoutingServer routingServer;

		internal HttpRouter (HttpRoutingServer server)
		{
			routingServer = server;
		}

		internal void HandleExternalRequest (HttpListenerContext context)
		{
			var externalRequest = context.Request;
			var externalResponse = context.Response;

			var destination = routingServer.RoutingTable.FindMatchingDomain (externalRequest.UserHostName);
			//CallDestination (destination, externalRequest);

			Console.WriteLine ("Call from: " + externalRequest.RemoteEndPoint);

			byte[] responsebyte = System.Text.Encoding.UTF8.GetBytes ("Hello World from router");
			System.IO.Stream output = externalResponse.OutputStream;
			output.Write (responsebyte, 0, responsebyte.Length);
			output.Close ();
			externalResponse.Close ();
		}
	}
}

