using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasker.Application.Interfaces;
using Tasker.Domain;

namespace Tasker.Application.Operations.Commands.GenerateNumber
{
	public class GenerateNumberCommandHandler
		   : IRequestHandler<GenerateNumberCommand, ParametrizedTaskResult>
	{
		public GenerateNumberCommandHandler() { }

		public async Task<ParametrizedTaskResult> Handle(GenerateNumberCommand request,
			CancellationToken cancellationToken)
		{
			await Task.Delay(new Random().Next(30000, 60000)); // симуляция долгих вычислений
			Random random = new Random();
			// Генерируем случайное число в диапазоне от 10^(length-1) до (10^length) - 1
			int min = (int)Math.Pow(10, request.Length - 1);
			int max = (int)Math.Pow(10, request.Length) - 1;
			int randomNumber = random.Next(min, max + 1);
			return new ParametrizedTaskResult { ParametrizedTaskId = request.TaskId, Result = randomNumber.ToString() };
		}
	}
}
