using Confluent.Kafka;
using MediatR;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasker.Application.Interfaces;

namespace Tasker.Messaging.Kafka
{
	public class KafkaConsumer <TMessage> : BackgroundService
	{
		public readonly string _topic;
		private readonly IConsumer<string, TMessage> _consumer;
		private readonly IMessageHandler<TMessage> _messageHandler;

		public KafkaConsumer(IOptions<KafkaSettings> kafkaSettings, IMessageHandler<TMessage> messageHandler)
		{
			_messageHandler = messageHandler;
			var config = new ConsumerConfig()
			{
				BootstrapServers = kafkaSettings.Value.Consumer.BootstrapServers,
				GroupId = kafkaSettings.Value.Consumer.GroupId,
				EnableAutoCommit = true,            
				AutoOffsetReset = AutoOffsetReset.Latest
			};
			_topic = kafkaSettings.Value.Consumer.Topic;
			_consumer = new ConsumerBuilder<string, TMessage>(config)
				.SetValueDeserializer(new KafkaJsonSerializer<TMessage>())
				.Build();
		}

		protected override Task ExecuteAsync(CancellationToken stoppingToken)
		{
			return Task.Run(() => ConsumeAsync(stoppingToken), stoppingToken);
		}

		private async Task ConsumeAsync(CancellationToken stoppingToken)
		{
			_consumer.Subscribe(_topic);
			try
			{
				while (!stoppingToken.IsCancellationRequested)
				{
					var result = _consumer.Consume(stoppingToken);
					var x = result.Value;
					await _messageHandler.HandleAsync(result.Message.Value, stoppingToken);

					try
					{
					//	_consumer.Commit();
					} catch (Exception ex) 
						{

						}
				}

			}
			catch (Exception ex) 
			{
				Console.WriteLine(ex.Message);
			}
		}

		public override Task StopAsync(CancellationToken cancellationToken)
		{
			_consumer.Close();
			return base.StopAsync(cancellationToken);
		}
	}
}
