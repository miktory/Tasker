using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasker.Application.Interfaces;
using Tasker.Application.Tasks.Commands.CreateTask;
using Tasker.Domain;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using Tasker.Application.Common.Exceptions;

namespace Tasker.Application.Tasks.Commands.CancelTask.FullAccess
{
    public class CancelTaskCommandHandler
            : IRequestHandler<CancelTaskCommand>
    {
        private readonly IApplicationDbContext _dbContext;
        public CancelTaskCommandHandler(IApplicationDbContext dbContext) =>
            _dbContext = dbContext;
        public async Task<Unit> Handle(CancelTaskCommand request,
            CancellationToken cancellationToken)
        {
            var entity = await _dbContext.ParametrizedTasks.FirstOrDefaultAsync(task => task.Id == request.Id, cancellationToken);
            if (entity == null) 
            {
                throw new NotFoundException(nameof(ParametrizedTask), request.Id);
            }

            if (entity.Status != "CANCELLED" && entity.Status != "FINISHED")
            {
                entity.Status = "CANCELLED";
                entity.EndDate = DateTime.Now.ToUniversalTime();
            }

			await _dbContext.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
