using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Tasker.Domain;

namespace Tasker.Application.Tasks.Commands.CreateTaskResult
{
	public class CreateTaskResultCommand : IRequest<int>
	{
		public ParametrizedTaskResult result;
	}
}
