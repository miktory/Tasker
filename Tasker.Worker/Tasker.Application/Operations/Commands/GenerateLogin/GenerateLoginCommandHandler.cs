using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasker.Application.Interfaces;
using Tasker.Domain;

namespace Tasker.Application.Operations.Commands.GenerateLogin
{
	public class GenerateLoginCommandHandler
		   : IRequestHandler<GenerateLoginCommand, ParametrizedTaskResult>
	{
		public GenerateLoginCommandHandler() { }

		public async Task<ParametrizedTaskResult> Handle(GenerateLoginCommand request,
			CancellationToken cancellationToken)
		{
			await Task.Delay(new Random().Next(30000, 60000)); // симуляция долгих вычислений
			const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
			Random random = new Random();
			char[] stringChars = new char[request.Length];

			for (int i = 0; i < request.Length; i++)
			{
				stringChars[i] = chars[random.Next(chars.Length)];
			}

			return new ParametrizedTaskResult { ParametrizedTaskId = request.TaskId, Result = new string(stringChars) };

		}
	}
}
