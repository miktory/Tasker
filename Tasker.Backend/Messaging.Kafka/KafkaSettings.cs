﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasker.Messaging.Kafka
{
	public class KafkaSettings
	{
		public string BootstrapServers { get; set; }
		public string Topic { get; set; }
	}
}
