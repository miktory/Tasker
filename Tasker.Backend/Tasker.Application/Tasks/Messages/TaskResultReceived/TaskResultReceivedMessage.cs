using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Tasker.Application.Interfaces;

namespace Tasker.Application.Tasks.Messages.TaskResultReceived
{
	public class TaskResultReceivedMessage 
	{
		public int Id { get; set; }
		public Guid ParametrizedTaskId { get; set; }
		public string? Result { get; set; }
	}
}
