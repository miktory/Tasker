using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasker.Application.Interfaces;

namespace Tasker.Messaging.Kafka
{
	public static class Extensions
	{
		public static void AddProducer<TMessage>(this IServiceCollection services, IConfigurationSection configurationSection)
		{
			services.Configure<KafkaSettings>(configurationSection);
			services.AddSingleton<IKafkaProducer<TMessage>, KafkaProducer<TMessage>>();
		}
	}
}
