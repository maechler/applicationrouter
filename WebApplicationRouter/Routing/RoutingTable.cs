using System;
using System.Linq;
using System.Collections.Generic;

namespace Marx.Net
{
	public class RoutingTable : List<RoutingDestination>
	{
		public RoutingTable ()
		{
		}

		public RoutingDestination FindMatchingDomain (String domain)
		{
			return this.Single (d => d.IsDomainMatch (domain));
		}
	}
}

