using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Hackathon.Services.Jira;
using System.Linq;

namespace Hackathon.Controllers
{
	public class MetricController : Controller
	{
		public static List<JiraBugsByProjectResponse> cachedBugCounts;

		public ActionResult AllProjects()
		{
			if (cachedBugCounts == null)
			{
				cachedBugCounts = new JiraApiClient().GetProjectBugList();
			}

			return Json(Calculate(cachedBugCounts), JsonRequestBehavior.AllowGet);
		}

		public ActionResult SingleProject(string projectKey)
		{
			return Json(new JiraApiClient().ProjectBugCount(projectKey), JsonRequestBehavior.AllowGet);
		}

		private MetricResult Calculate(List<JiraBugsByProjectResponse> bugCounts)
		{
			return HideousCalculateImpl(bugCounts);
		}

		private MetricResult HideousCalculateImpl(List<JiraBugsByProjectResponse> bugCounts)
		{
			var max = bugCounts.Max(x => x.ProjectBugCount.total);
			var min = bugCounts.Min(x => x.ProjectBugCount.total);
			var bucketSize = (max - min) / 10;

			var buckets = new List<int>();
			for (int i = 1; i <= 10; i++)
			{
				buckets.Add(i * bucketSize);
			}

			var results = new List<MetricResult.Score>();
			foreach (var jiraBugsByProjectResponse in bugCounts)
			{
				var score = 0;
				for (int i = 0; i < buckets.Count; i++)
				{
					if (jiraBugsByProjectResponse.ProjectBugCount.total <= buckets[i])
					{
						score = i + 1;
						break;
					}
				}

				results.Add(new MetricResult.Score()
				{
					label = jiraBugsByProjectResponse.ProjectDetails.name,
					value = score.ToString()
				});
			}

			var parent = new MetricResult()
			{
				displayFormat = "smiley",
				metric = "bugCount",
				scores = results.ToArray()
			};

			return parent;
		}
	}

	

	public class MetricResult
	{
		public class Score
		{
			public string label { get; set; }
			public string value { get; set; }
		}

		public string displayFormat { get; set; }
		public string metric { get; set; }
		public Score[] scores { get; set; }
	}
}
