using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Tasker.Application.Interfaces;
using Tasker.Domain;

using System.Text.Json;

namespace Tasker.Application.Tasks.Commands.CreateTask
{
    public class CreateTaskCommandHandler
            : IRequestHandler<CreateTaskCommand, Guid>
    {
        private readonly IParametrizedTasksDbContext _dbContext;
        public CreateTaskCommandHandler(IParametrizedTasksDbContext dbContext) =>
            _dbContext = dbContext;
        public async Task<Guid> Handle(CreateTaskCommand request,
            CancellationToken cancellationToken)
        {
            var task = new ParametrizedTask
            {
                UserId = request.UserId,
                Id = Guid.NewGuid(),
                Parameters = request.Parameters,
                Type = request.Type,
                CreationDate = DateTime.Now.ToUniversalTime(),
                StartDate = null,
                EndDate = null,
                TTL = request.TTL,
                Status = "CREATED"
            };

            await _dbContext.ParametrizedTasks.AddAsync(task, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return task.Id;
        }
    }
}
