﻿using Confluent.Kafka;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Tasker.Messaging.Kafka
{
	public class KafkaJsonSerializer<TMessage> : ISerializer<TMessage>
	{
		public byte[] Serialize(TMessage data, SerializationContext context)
		{
			return JsonSerializer.SerializeToUtf8Bytes(data);
		}
	}
}
