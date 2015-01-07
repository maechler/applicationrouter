using System;
using System.Net.Sockets;
using System.Threading;

namespace Marx.Net
{
	public class TcpRoutingServer
	{
		TcpListener listener;

		public bool IsRunning { get; private set; }

		public RoutingTable RoutingTable { get; set; }

		public TcpRoutingServer (int port)
		{
			listener = new TcpListener (port);
			RoutingTable = new RoutingTable ();
		}

		public void Start ()
		{
			listener.Start ();
			IsRunning = true;


			while (IsRunning) {
				var tcpRouter = new TcpRouter (this, listener.AcceptTcpClient ());
				var thread = new Thread (new ThreadStart (tcpRouter.StartRouter));
				thread.Start ();
			}

			listener.Stop ();
		}

		public void Stop ()
		{
			IsRunning = false;
		}
	}
}

