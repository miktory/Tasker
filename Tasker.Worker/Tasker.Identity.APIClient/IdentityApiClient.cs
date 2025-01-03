using Tasker.Application.Interfaces;
using Tasker.Identity.APIClient.Models;

namespace Tasker.Identity.APIClient
{
	public class IdentityApiClient : IIdentityApiClient
	{
		private readonly HttpClient _httpClient;

		public IdentityApiClient(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}

		public async Task<string> AuthenticateAsync(string username, string password, string clientId, CancellationToken cancellationToken)
		{
			var requestData = new Dictionary<string, string>
		{
			{ "grant_type", "password" },
			{ "username", username },
			{ "password", password },
			{ "client_id", clientId }
		};

			var response = await _httpClient.PostAsync("/connect/token", new FormUrlEncodedContent(requestData), cancellationToken);
			response.EnsureSuccessStatusCode();

			var content = await response.Content.ReadAsAsync<AuthenticationResponse>();
			return content.AccessToken; 
		}
	}

}
