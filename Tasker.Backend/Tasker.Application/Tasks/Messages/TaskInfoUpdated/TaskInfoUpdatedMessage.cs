using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Tasker.Application.Interfaces;

namespace Tasker.Application.Tasks.Messages.TaskInfoUpdated
{
	public class TaskInfoUpdatedMessage 
	{
		public Guid Id { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime? EndDate { get; set; }
		public string? Result { get; set; }
	}
}
