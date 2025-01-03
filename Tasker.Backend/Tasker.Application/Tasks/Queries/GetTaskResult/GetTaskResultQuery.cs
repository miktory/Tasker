using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasker.Shared.Vm;

namespace Tasker.Application.Tasks.Queries.GetTaskResult
{
	public class GetTaskResultQuery : IRequest<TaskResultVm>
	{
		public Guid UserId { get; set; }
		public Guid TaskId { get; set; }
	}
}
