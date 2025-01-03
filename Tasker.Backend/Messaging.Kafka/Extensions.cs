using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasker.Application.Interfaces;
using Tasker.Application.Tasks.Messages.TaskResultReceived;

namespace Tasker.Messaging.Kafka
{
	public static class Extensions
	{
		public static IServiceCollection AddConsumer<TMessage, THandler>(this IServiceCollection services, IConfigurationSection configurationSection)
			where THandler: class, IMessageHandler<TMessage>
		{
			services.Configure<KafkaSettings>(configurationSection);
			services.AddHostedService<KafkaConsumer<TMessage>>();
			services.AddSingleton<IMessageHandler<TMessage>, THandler>();

			return services;
		}

		public static void AddProducer<TMessage>(this IServiceCollection services, IConfigurationSection configurationSection)
		{
			services.Configure<KafkaSettings>(configurationSection);
			services.AddSingleton<IMessageProducer<TMessage>, KafkaProducer<TMessage>>();
		}
	}
}
