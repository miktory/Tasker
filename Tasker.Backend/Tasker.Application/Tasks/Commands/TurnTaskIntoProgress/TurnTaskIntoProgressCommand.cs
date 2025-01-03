using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasker.Application.Tasks.Commands.TurnTaskIntoProgress
{
	public class TurnTaskIntoProgressCommand : IRequest <string>
	{ 
		public Guid TaskId { get; set; }
		public string WorkerName { get; set; }	
	}
}
