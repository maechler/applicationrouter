using System;
using System.Net;
using System.Web;
using System.IO;
using Marx.Net;
using Marx.Net.CLI;

namespace WebApplicationRouter
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			var interpreter = new CommandLineInterpreter ();
			interpreter.Execute (args);

			//Console.WriteLine ("WebApp Router");
			//Console.WriteLine (args [0]);
			//var server = new HttpRoutingServer ();

			//Console.WriteLine ("running server...");
			//server.Start ();
		}
	}
}