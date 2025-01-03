using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasker.Application.Interfaces;

namespace Tasker.APIClient
{
	public static class DependencyInjection
	{
		public static void AddTaskerClientApi(this IServiceCollection services, IConfigurationSection configurationSection)
		{
			services.Configure<TaskerApiClientSettings>(configurationSection);

			services.AddHttpClient<ITaskerApiClient, TaskerApiClient>((serviceProvider, client) =>
			{
				var settings = serviceProvider.GetRequiredService<IOptions<TaskerApiClientSettings>>().Value;
				client.BaseAddress = new Uri(settings.BaseAddress);
			});
		}
	}
}
