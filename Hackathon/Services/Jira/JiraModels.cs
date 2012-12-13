using System;

namespace Hackathon.Services.Jira
{
	public class JiraProjectBugCount
	{
		public string expand { get; set; }
		public int startAt { get; set; }
		public int maxResults { get; set; }
		public int total { get; set; }
	}

	public class JiraProjectDetails
	{
		public string self { get; set; }
		public string id { get; set; }
		public string key { get; set; }
		public string name { get; set; }
	}

	public class JiraBugsByProjectResponse
	{
		public JiraProjectDetails ProjectDetails;
		public JiraProjectBugCount ProjectBugCount;
	}

	//public class Progress
	//{
	//	public int progress { get; set; }
	//	public int total { get; set; }
	//}

	//public class Issuetype
	//{
	//	public string self { get; set; }
	//	public string id { get; set; }
	//	public string description { get; set; }
	//	public string iconUrl { get; set; }
	//	public string name { get; set; }
	//	public bool subtask { get; set; }
	//}

	//public class Votes
	//{
	//	public string self { get; set; }
	//	public int votes { get; set; }
	//	public bool hasVoted { get; set; }
	//}


	//public class Reporter
	//{
	//	public string self { get; set; }
	//	public string name { get; set; }
	//	public string emailAddress { get; set; }
	//	public string displayName { get; set; }
	//	public bool active { get; set; }
	//}

	//public class Priority
	//{
	//	public string self { get; set; }
	//	public string iconUrl { get; set; }
	//	public string name { get; set; }
	//	public string id { get; set; }
	//}

	//public class Watches
	//{
	//	public string self { get; set; }
	//	public int watchCount { get; set; }
	//	public bool isWatching { get; set; }
	//}

	//public class Status
	//{
	//	public string self { get; set; }
	//	public string description { get; set; }
	//	public string iconUrl { get; set; }
	//	public string name { get; set; }
	//	public string id { get; set; }
	//}

	//public class Assignee
	//{
	//	public string self { get; set; }
	//	public string name { get; set; }
	//	public string emailAddress { get; set; }
	//	public string displayName { get; set; }
	//	public bool active { get; set; }
	//}


	//public class Project
	//{
	//	public string self { get; set; }
	//	public string id { get; set; }
	//	public string key { get; set; }
	//	public string name { get; set; }
	//}

	//public class Aggregateprogress
	//{
	//	public int progress { get; set; }
	//	public int total { get; set; }
	//}

	//public class Fields
	//{
	//	public Progress progress { get; set; }
	//	public string summary { get; set; }
	//	public Issuetype issuetype { get; set; }
	//	public Votes votes { get; set; }
	//	public object resolution { get; set; }
	//	public object[] fixVersions { get; set; }
	//	public object resolutiondate { get; set; }
	//	public object customfield_10521 { get; set; }
	//	public string customfield_10520 { get; set; }
	//	public object timespent { get; set; }
	//	public Reporter reporter { get; set; }
	//	public object aggregatetimeoriginalestimate { get; set; }
	//	public DateTime updated { get; set; }
	//	public DateTime created { get; set; }
	//	public object description { get; set; }
	//	public Priority priority { get; set; }
	//	public string customfield_10120 { get; set; }
	//	public object customfield_10121 { get; set; }
	//	public object customfield_10020 { get; set; }
	//	public object[] issuelinks { get; set; }
	//	public object customfield_10620 { get; set; }
	//	public Watches watches { get; set; }
	//	public object customfield_10000 { get; set; }
	//	public object[] subtasks { get; set; }
	//	public Status status { get; set; }
	//	public object[] labels { get; set; }
	//	public object customfield_10005 { get; set; }
	//	public int workratio { get; set; }
	//	public Assignee assignee { get; set; }
	//	public object customfield_10221 { get; set; }
	//	public object customfield_10220 { get; set; }
	//	public object aggregatetimeestimate { get; set; }
	//	public string customfield_10522 { get; set; }
	//	public Project project { get; set; }
	//	public object[] versions { get; set; }
	//	public string environment { get; set; }
	//	public object customfield_10420 { get; set; }
	//	public object customfield_10321 { get; set; }
	//	public object timeestimate { get; set; }
	//	public Aggregateprogress aggregateprogress { get; set; }
	//	public object lastViewed { get; set; }
	//	public object[] components { get; set; }
	//	public object timeoriginalestimate { get; set; }
	//	public object aggregatetimespent { get; set; }
	//}

	//public class Issue
	//{
	//	public string expand { get; set; }
	//	public string id { get; set; }
	//	public string self { get; set; }
	//	public string key { get; set; }
	//	public Fields fields { get; set; }
	//}
}