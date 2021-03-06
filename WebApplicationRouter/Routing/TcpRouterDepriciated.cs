﻿using System;
using System.Threading;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Text;
using System.IO;

namespace Marx.Net
{
	public class TcpRouterDepriciated
	{
		TcpRoutingServer routingServer;
		TcpClient externalClient;
		TcpClient internalClient;

		public TcpRouterDepriciated (TcpRoutingServer routingServer, TcpClient externalClient)
		{
			this.externalClient = externalClient;
			this.routingServer = routingServer;
			internalClient = new TcpClient ();
			internalClient.ReceiveBufferSize = externalClient.SendBufferSize;
			internalClient.SendBufferSize = externalClient.ReceiveBufferSize;
		}

		public async void StartRouter ()
		{
			Byte[] externalBuffer = new Byte[externalClient.ReceiveBufferSize];
			Byte[] externalBufferCopy = new Byte[externalClient.ReceiveBufferSize];

			externalClient.Client.Receive (externalBuffer);

			//copy buffer
			for (int i = 0; i < externalBuffer.Length; i++) {
				externalBufferCopy [i] = externalBuffer [i];
			}
			
			//call internal tcp server
			var internalResult = await Task.Factory.StartNew (() => CallInternalTcpServer ("localhost", 3000, externalBufferCopy));

			var endData = new Byte[externalClient.Client.SendBufferSize];

			Array.Copy (internalResult, endData, internalResult.Length);

			externalClient.Client.Send (endData);

			Console.WriteLine ("connection closed");


			externalClient.Close ();
		}

		Byte[] CallInternalTcpServer (String address, int port, Byte[] data)
		{ 
			//var internalClient = new TcpClient ();
			internalClient.Connect (address, port);

			Byte[] internalBuffer = new Byte[internalClient.ReceiveBufferSize];

			var datacopy = new Byte[internalClient.SendBufferSize];

			int i = 0;
			while (i < datacopy.Length) {
				datacopy [i] = data [i];
				i++;
			}

			//var fakeData = Encoding.UTF8.GetBytes ("GET /sys/login HTTP/1.1\r\nHost: localhost:8080\r\nAccept: text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8\r\nAccept-Language: en-us\r\nConnection: keep-alive\r\nAccept-Encoding: gzip, deflate\r\nUser-Agent: Mozilla/5.0 (Macintosh; Intel Mac OS X 10_10_1) AppleWebKit/600.2.5 (KHTML, like Gecko) Version/8.0.2 Safari/600.2.5\r\n\r\n");
			//internalClient.Client.Send (System.Text.Encoding.UTF8.GetBytes ("GET / HTTP/1.1\r\nHost: localhost:8080\r\nAccept: text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8\r\nAccept-Language: en-us\r\nConnection: keep-alive\r\nAccept-Encoding: gzip, deflate\r\nUser-Agent: Mozilla/5.0 (Macintosh; Intel Mac OS X 10_10_1) AppleWebKit/600.2.5 (KHTML, like Gecko) Version/8.0.2 Safari/600.2.5\r\n\r\n"));
			internalClient.Client.Send (datacopy);
			 
			internalClient.Client.Receive (internalBuffer);
			internalClient.Close ();

			//var internalMessage = System.Text.Encoding.UTF8.GetString (internalBuffer);

			return internalBuffer;

		}

	}
}

