using System;
using System.Collections.Generic;
using RestSharp;

namespace Hackathon.Services.Jira
{
	public enum JiraIssueStatuses
	{
		resolved,
		closed,
		done
	}

	public class JiraApiClient
	{
		private RestClient _client;
		public const string BaseUrl = "https://groupcommerce.atlassian.net/rest/api/latest/";
		public const string username = "smoore";
		public const string password = "h@ck@thon";

		public JiraApiClient()
		{
			_client = new RestClient(BaseUrl);
			_client.Authenticator = new HttpBasicAuthenticator(username, password);
		}

		public List<JiraBugsByProjectResponse> GetProjectBugList()
		{
			var projectList = GetProjectList();

			var result = new List<JiraBugsByProjectResponse>();
			foreach (var jiraProject in projectList)
			{
				var item = new JiraBugsByProjectResponse()
				{
					ProjectDetails = jiraProject
				};
				item.ProjectBugCount = ProjectBugCount(jiraProject.key);

				result.Add(item);
			}

			return result;
		}

		public List<JiraProjectDetails> GetProjectList()
		{
			var request = new RestRequest("project", Method.GET);
			request.RequestFormat = DataFormat.Json;

			var result = Execute<List<JiraProjectDetails>>(request);

			return result;
		}

		public JiraProjectBugCount ProjectBugCount(string key)
		{
			var url = buildProjectJqlSearchQuery(key, JiraIssueStatuses.resolved, JiraIssueStatuses.done,
															JiraIssueStatuses.closed);
			var request = new RestRequest(url, Method.GET);
			request.RequestFormat = DataFormat.Json;

			var result = Execute<JiraProjectBugCount>(request);

			return result;
		}

		private const string jqlBaseQuery = "search?jql=project = {0} AND status NOT IN ({1})";
		private string buildProjectJqlSearchQuery(string project, params JiraIssueStatuses[] issueStatuseses)
		{
			return String.Format(jqlBaseQuery, project, String.Join(", ", issueStatuseses));
		}

		public T Execute<T>(RestRequest request) where T : new()
		{
			var response = _client.Execute<T>(request);

			if (response.ErrorException != null)
			{
				throw response.ErrorException;
			}

			return response.Data;
		}
	}
}