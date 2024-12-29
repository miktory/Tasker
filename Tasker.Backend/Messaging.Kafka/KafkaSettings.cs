using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasker.Messaging.Kafka
{
	public class KafkaSettings
	{
		public ConsumerSettings Consumer { get; set; } = new ConsumerSettings();
		public ProducerSettings Producer { get; set; } = new ProducerSettings();

		public class ConsumerSettings
		{
			public string BootstrapServers { get; set; }
			public string Topic { get; set; }
			public string GroupId { get; set; }
		}

		public class ProducerSettings
		{
			public string BootstrapServers { get; set; }
			public string Topic { get; set; }
			public string GroupId { get; set; }
		}
	}
}
