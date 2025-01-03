using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Tasker.Domain;

namespace Tasker.Application.Operations.Commands.GenerateLogin
{
	public class GenerateLoginCommand : IRequest<ParametrizedTaskResult>
	{
		public Guid TaskId { get; set; }
		public int Length { get; set; }	
	}
}
