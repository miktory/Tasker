﻿using Confluent.Kafka;
using Confluent.Kafka.Admin;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasker.Application.Interfaces;

namespace Tasker.Messaging.Kafka
{
	public class KafkaProducer<TMessage> : IMessageProducer<TMessage>
	{
		private readonly IProducer<string, TMessage> producer;
		private readonly string topic;
		public KafkaProducer(IOptions<KafkaSettings> kafkaSettings)
		{
			var config = new ProducerConfig
			{
				BootstrapServers = kafkaSettings.Value.Producer.BootstrapServers,
			};

			producer = new ProducerBuilder<string, TMessage>(config)
				.SetValueSerializer(new KafkaJsonSerializer<TMessage>())
				.Build();

			topic = kafkaSettings.Value.Producer.Topic;

		}
		public void Dispose()
		{
			producer?.Dispose();
		}

		public async Task ProduceAsync(TMessage message, CancellationToken cancellationToken)
		{
			await producer.ProduceAsync(topic, new Message<string, TMessage>
			{
			//	Key = "worker",
				Value = message
			}, cancellationToken);
		}
	}
}
