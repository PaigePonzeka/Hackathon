using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using GroupCommerce.Models;
using GroupCommerce.Models.MessageQueue;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using WebActivator;
using System.Linq;

[assembly: PostApplicationStartMethod(typeof(Hackathon.Notifier), "Init")]

namespace Hackathon
{
	public class JobMessageEntry
	{
		public enum JobMessageEntryStatus
		{
			Active,
			Completed
		}

		private BpmJobStatusEvent _theEvent;
		private JobMessageEntryStatus _status;

		public JobMessageEntry(BpmJobStatusEvent theEvent)
		{
			_theEvent = theEvent;
			_status = JobMessageEntryStatus.Active;
		}

		public string Key { get { return _theEvent.JobInstanceId; } }

		public DateTime CreatedAt { get { return _theEvent.EventTime; } }

		public JobMessageEntryStatus Status
		{
			get { return _status; }
			set { _status = value; }
		}

		public BpmJobStatusEvent Item {get { return _theEvent; }}
	}

	public static class Notifier
	{
		public static readonly object theLock = new object();
		public static List<JobMessageEntry> Messages = new List<JobMessageEntry>();

		private static Timer _timer;

		public static void Init()
		{
			_timer = new Timer(_ => Sweep(GlobalHost.DependencyResolver), null, 1000, Timeout.Infinite);
		}

		private static void Sweep(IDependencyResolver resolver)
		{
			try
			{
				SendMessages(resolver);
			}
			catch (Exception ex)
			{
				// do something...
			}
			finally
			{
				_timer.Change(1000, Timeout.Infinite);
			}
		}

		private static void SendMessages(IDependencyResolver resolver)
		{
			var connectionManager = resolver.Resolve<IConnectionManager>();
			var hubContext = connectionManager.GetHubContext<BackgroundProcessHub>();
			
			var messages = BpmJobStatusConsumer.GetAndReset();

			foreach (var bpmJobStatusEvent in messages)
			{
				if (bpmJobStatusEvent.Status == JobStatus.AllDead)
				{
					lock (theLock)
					{
						Messages = new List<JobMessageEntry>();
					}

					Transmit(hubContext, bpmJobStatusEvent);

					continue;
				}

				if (bpmJobStatusEvent.Status == JobStatus.Finishing)
				{
					var items = new List<JobMessageEntry>();
					lock (theLock)
					{
						items = Messages.Where(x => x.Key == bpmJobStatusEvent.JobInstanceId)
										.Where(x => x.Status != JobMessageEntry.JobMessageEntryStatus.Completed)
										.ToList();
					}

					items.ForEach(x => x.Status = JobMessageEntry.JobMessageEntryStatus.Completed);
				}

				Transmit(hubContext, bpmJobStatusEvent);

				lock (theLock)
				{
					Messages.Add(new JobMessageEntry(bpmJobStatusEvent));
				}
			}
		}

		private static void Transmit(IHubContext hubContext, BpmJobStatusEvent bpmJobStatusEvent)
		{
			switch (bpmJobStatusEvent.Status)
			{
				case JobStatus.AllDead:
					hubContext.Clients.All.clearAllJobs();
					break;
				case JobStatus.Finishing:
					hubContext.Clients.All.removeJob(bpmJobStatusEvent);
					break;
				case JobStatus.Starting:
					hubContext.Clients.All.addJob(bpmJobStatusEvent);
					break;
			}
		}
	}
}