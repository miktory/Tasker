using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Tasker.Application.Interfaces;

namespace Tasker.Application.Tasks.Messages.TaskReceivedMessage
{
	public class TaskReceivedMessage 
	{
		public Guid Id { get; set; }
		public string Type { get; set; }
		public JsonDocument Parameters { get; set; }
		public int TTL { get; set; }
	}
}
