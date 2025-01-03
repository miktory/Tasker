using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Tasker.Shared.Vm
{
	public class TaskDetailsVm
	{
		public Guid Id { get; set; }
		public string Type { get; set; }
		public JsonDocument Parameters { get; set; }
		public DateTime CreationDate { get; set; }
		public DateTime? StartDate { get; set; }
		public DateTime? EndDate { get; set; }
		public int TTL { get; set; }
		public string Status { get; set; }
		public string? WorkerName { get; set; }
	}
}
