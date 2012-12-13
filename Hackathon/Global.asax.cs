using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using GroupCommerce;
using GroupCommerce.Models.MessageQueue;
using StructureMap;

namespace Hackathon
{
	// Note: For instructions on enabling IIS6 or IIS7 classic mode, 
	// visit http://go.microsoft.com/?LinkId=9394801
	public class MvcApplication : System.Web.HttpApplication
	{
		private static bool _shuttingDown = false;
		protected void Application_Start()
		{
			AreaRegistration.RegisterAllAreas();

			WebApiConfig.Register(GlobalConfiguration.Configuration);
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
			RouteConfig.RegisterRoutes(RouteTable.Routes);

			ObjectFactory.Initialize(fact =>
			{
				MessageQueueIocRegistration.Register(fact, () => _shuttingDown);
				fact.For<IFileSystem>().Singleton().Use(() => new DiskDriveFileSystem(AppDomain.CurrentDomain.BaseDirectory));
			});

			ObjectFactory.GetInstance<IServiceBus>().RegisterConsumer(new BpmJobStatusConsumer());
		}

		protected void Application_End()
		{
			_shuttingDown = true;
		}
	}

	[QueueBinding(MessageExchanges.BackgroundProcessManager, QueueBehaviors.PublicFanout)]
	public class BpmJobStatusConsumer : IMessageConsumer<BpmJobStatusEvent>
	{
		private static readonly object _lock = new object();
		private static List<BpmJobStatusEvent> _events = new List<BpmJobStatusEvent>();

		bool IMessageConsumer<BpmJobStatusEvent>.ProcessMessage(BpmJobStatusEvent message)
		{
			lock (_lock)
				_events.Add(message);

			return true;
		}

		public static List<BpmJobStatusEvent> GetAndReset()
		{
			lock (_lock)
			{
				var l = _events;
				_events = new List<BpmJobStatusEvent>();
				return l;
			}
		}
	}

}