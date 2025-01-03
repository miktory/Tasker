using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasker.Shared.Dto
{
	public class TaskLookupDto
	{
		public Guid Id { get; set; }
		public string Type { get; set; }
		public string Status { get; set; }
	}
}
