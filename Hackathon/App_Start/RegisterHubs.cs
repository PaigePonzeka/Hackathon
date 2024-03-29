using System.Web;
using System.Web.Routing;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hosting.AspNet;

[assembly: PreApplicationStartMethod(typeof(Hackathon.RegisterHubs), "Start")]

namespace Hackathon
{
	public static class RegisterHubs
	{
		public static void Start()
		{
			// Register the default hubs route: ~/signalr/hubs
			RouteTable.Routes.MapHubs();
		}
	}
}
