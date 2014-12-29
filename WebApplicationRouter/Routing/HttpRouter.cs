using System;
using System.Threading;
using System.Net;

namespace Marx.Net
{
	class HttpRouter
	{
		internal void HandleExternalRequest (IAsyncResult result)
		{
			var routingServer = (HttpRoutingServer)result.AsyncState;
			var listener = routingServer.Listener;

			var context = listener.EndGetContext (result);
			var request = context.Request;
			var response = context.Response;

			Console.WriteLine ("Call from: " + request.RemoteEndPoint.ToString ());

			byte[] responsebyte = System.Text.Encoding.UTF8.GetBytes ("Hello World from router");
			System.IO.Stream output = response.OutputStream;
			output.Write (responsebyte, 0, responsebyte.Length);
			output.Close ();
		}
	}
}

