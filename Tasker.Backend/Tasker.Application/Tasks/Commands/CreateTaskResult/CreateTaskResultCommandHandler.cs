using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasker.Application.Interfaces;
using Tasker.Application.Tasks.Commands.CreateTask;
using Tasker.Domain;

namespace Tasker.Application.Tasks.Commands.CreateTaskResult
{
	public class CreateTaskResultCommandHandler
			   : IRequestHandler<CreateTaskResultCommand, int>
	{
		private readonly IApplicationDbContext _dbContext;
		public CreateTaskResultCommandHandler(IApplicationDbContext dbContext) =>
			_dbContext = dbContext;
		public async Task<int> Handle(CreateTaskResultCommand request,
			CancellationToken cancellationToken)
		{
			await _dbContext.ParametrizedTasksResults.AddAsync(request.result, cancellationToken);
			await _dbContext.SaveChangesAsync(cancellationToken);
			return request.result.Id;
		}
	}
}
