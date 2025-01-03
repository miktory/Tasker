using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasker.Domain;

namespace Tasker.Application.Operations.Commands.GenerateNumber
{
	public class GenerateNumberCommand : IRequest <ParametrizedTaskResult>
	{
		public Guid TaskId { get; set; }
		public int Length { get; set; }
	}
}
