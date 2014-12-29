using System;
using System.Net;
using System.Web;
using System.IO;
using Marx.Net;

namespace WebApplicationRouter
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			Console.WriteLine ("WebApp Router");
			var server = new HttpRoutingServer ();

			Console.WriteLine ("running server...");
			server.Start ();
		}
	}
}