using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasker.Application.Common.Exceptions;
using Tasker.Application.Interfaces;
using Tasker.Application.Tasks.Commands.CreateTask;
using Tasker.Domain;
using Tasker.Shared.Dto;


namespace Tasker.Application.Tasks.Commands.TurnTaskIntoProgress
{
	public class TurnTaskIntoProgressCommandHandler : IRequestHandler<TurnTaskIntoProgressCommand, string>
	{
		private readonly IApplicationDbContext _dbContext;
		public TurnTaskIntoProgressCommandHandler(IApplicationDbContext dbContext) =>
			_dbContext = dbContext;
		public async Task<string> Handle(TurnTaskIntoProgressCommand request,
			CancellationToken cancellationToken)
		{

			var entity = await _dbContext.ParametrizedTasks.FirstOrDefaultAsync(task => task.Id == request.TaskId, cancellationToken);
			if (entity == null)
			{
				throw new NotFoundException(nameof(ParametrizedTask), request.TaskId);
			}

			if (entity.WorkerName != null & entity.WorkerName != request.WorkerName)
			{
				throw new AlreadyRequestedException(nameof(ParametrizedTask), request.TaskId, entity.WorkerName);
			}

			if (entity.WorkerName == null)
			{
				entity.Status = "RUNNING";
				entity.WorkerName = request.WorkerName;
				entity.StartDate = DateTime.Now.ToUniversalTime();
			}
			await _dbContext.SaveChangesAsync(cancellationToken);
			return request.WorkerName;
		}
	}
}
