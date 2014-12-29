using System;

namespace Marx.Net.CLI
{
	public class CommandLineInterpreter
	{

		public void Execute (String[] args)
		{
			if (args.Length == 0) {
				Write ("No input parameters provided.");
				return;
			}

			switch (args [0]) {
			case "--version":
				Write ("Marx WebApplicationRouter, version 0.1.0");
				break;
			case "routes":
				HandleRoutes (args);
				break;
			default:
				Write ("Your input does not match a command!");
				break;
			}
		}

		protected void HandleRoutes (String[] args)
		{
			if (args.Length < 2) {
				Write ("Additional arguments needed.");
				return;
			}

			switch (args [1]) {
			case "list":
				Write ("show list of routes");
				break;
			case "add":
				Write ("add a new route");
				break;
			case "remove":
				Write ("remove a route");
				break;
			default:
				Write ("No valid argument supplied. Try routes list, routes add, routes delete");
				break;
			}
		}

		protected void Write (String s)
		{
			Console.WriteLine (s);
		}

	}
}

