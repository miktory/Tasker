using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using Tasker.Application.Interfaces;
using System.Runtime.CompilerServices;


namespace Tasker.APIClient
{
	public class TaskerApiClient : ITaskerApiClient
	{
		private readonly HttpClient _httpClient;
		private string _accessToken;

		public TaskerApiClient(HttpClient httpClient)
		{
			_httpClient = httpClient;
			_accessToken = "";
		}

		public async Task<string> RequestTaskProcessing(Guid taskId, string workerName, CancellationToken cancellationToken)
		{

			var response = await _httpClient.PatchAsync($"/api/Task/request/{taskId}?workerName={workerName}", null, cancellationToken);
			response.EnsureSuccessStatusCode();
			return await response.Content.ReadFromJsonAsync<string>();
		}

		public void SetAccessToken(string token)
		{
			_accessToken = token;
			_httpClient.DefaultRequestHeaders.Authorization =
				new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _accessToken);
		}
	}

}
