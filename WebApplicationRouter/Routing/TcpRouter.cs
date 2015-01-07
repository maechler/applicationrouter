using System;
using System.Net.Sockets;
using System.Text;

namespace Marx.Net
{
	public class TcpRouter
	{
		TcpClient externalClient;

		TcpRoutingServer routingServer;

		delegate byte[] ManipulateBuffer (byte[] buffer);

		public TcpRouter (TcpRoutingServer routingServer, TcpClient externalClient)
		{
			this.externalClient = externalClient;
			this.routingServer = routingServer;
		}

		public void StartRouter ()
		{
			var internalClient = new TcpClient ();

			internalClient.Connect ("localhost", 8000);

			internalClient.ReceiveBufferSize = externalClient.SendBufferSize;
			externalClient.ReceiveBufferSize = internalClient.SendBufferSize;

			var externalStream = externalClient.GetStream ();
			var internalStream = internalClient.GetStream ();

			var inputBuffer = new byte[externalClient.SendBufferSize];
			var outputBuffer = new byte[internalClient.SendBufferSize];

			while (externalClient.Connected && internalClient.Connected) {
				if (externalStream.DataAvailable) {
					CopyStream (externalStream, internalStream, inputBuffer, SpoofHostAddress);
				}

				if (internalStream.DataAvailable) {
					CopyStream (internalStream, externalStream, outputBuffer);
				}
			}

			internalClient.Close ();
			externalClient.Close ();
		}

		void CopyStream (NetworkStream input, NetworkStream output, byte[] buffer)
		{
			CopyStream (input, output, buffer, null);
		}

		void CopyStream (NetworkStream input, NetworkStream output, byte[] buffer, ManipulateBuffer manipulationMethod)
		{
			input.Read (buffer, 0, buffer.Length);
			input.Flush ();

			if (manipulationMethod != null)
				buffer = manipulationMethod (buffer);

			output.Write (buffer, 0, buffer.Length);
			output.Flush ();
		}

		byte[] SpoofHostAddress (byte[] buffer)
		{
			String content = Encoding.UTF8.GetString (buffer);
			content = content.Replace ("8080", "8000");

			return Encoding.UTF8.GetBytes (content);
		}
	}
}

