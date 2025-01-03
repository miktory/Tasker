using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasker.Application.Tasks.Messages.TaskReceivedMessage;
using Tasker.Domain;

namespace Tasker.Application.Interfaces
{
	public interface ITaskProcessor
	{
		public int TaskLimit { get; }
		public int TaskCount { get; }
		public bool RequestTaskProcessing(ParametrizedTask task);
	}
}
