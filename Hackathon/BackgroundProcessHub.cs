using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GroupCommerce.Models.MessageQueue;
using Microsoft.AspNet.SignalR.Hubs;

namespace Hackathon
{
	public class BackgroundProcessHub : Hub
	{
		public override System.Threading.Tasks.Task OnConnected()
		{
			var m = new List<JobMessageEntry>();

			lock (Notifier.theLock)
			{
				m = Notifier.Messages.ToList();
			}
			
			foreach (var item in m.Where(x => x.Status == JobMessageEntry.JobMessageEntryStatus.Active))
			{
				switch (item.Item.Status)
				{
					case JobStatus.AllDead:
						this.Clients.Caller.clearAllJobs();
						break;
					case JobStatus.Finishing:
						this.Clients.Caller.removeJob(item.Item);
						break;
					case JobStatus.Starting:
						this.Clients.Caller.addJob(item.Item);
						break;
				}
			}
			
			return base.OnConnected();
		}
	}
}