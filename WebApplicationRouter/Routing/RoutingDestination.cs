using System;
using System.Text.RegularExpressions;

namespace Marx.Net
{
	public class RoutingDestination
	{
		public String IPAddress { get; set; }

		public Int32 Port { get; set; }

		public String DomainPattern { get; set; }

		public RoutingDestination ()
		{
		}

		public bool IsDomainMatch (String domain)
		{
			return Regex.Match (domain, DomainPattern).Success;
		}
	}
}

