using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasker.Application.Interfaces;

namespace Tasker.Identity.APIClient
{
	public static class DependencyInjection
	{
		public static void AddIdentityApi(this IServiceCollection services, IConfigurationSection configurationSection)
		{
			services.Configure<IdentityApiClientSettings>(configurationSection);

			services.AddHttpClient<IIdentityApiClient, IdentityApiClient>((serviceProvider, client) =>
			{
				var settings = serviceProvider.GetRequiredService<IOptions<IdentityApiClientSettings>>().Value;
				client.BaseAddress = new Uri(settings.BaseAddress);
			});
		}
	}
}
