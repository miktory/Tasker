using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Tasker.Application.Interfaces;
using Tasker.Application.Common.Exceptions;
using Tasker.Domain;
using Tasker.Shared.Vm;

namespace Tasker.Application.Tasks.Queries.GetTaskResult
{
    public class GetTaskResultQueryHandler : IRequestHandler<GetTaskResultQuery, TaskResultVm>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetTaskResultQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<TaskResultVm> Handle(GetTaskResultQuery request, CancellationToken cancellationToken)
        {
            var result = await _dbContext.ParametrizedTasksResults
                .FirstOrDefaultAsync(r => r.ParametrizedTaskId == request.TaskId, cancellationToken);
            var task = await _dbContext.ParametrizedTasks
				.FirstOrDefaultAsync(t => t.Id == request.TaskId, cancellationToken);

			if (result == null || task.UserId != request.UserId)
			{
				throw new NotFoundException(nameof(ParametrizedTaskResult), request.TaskId);
			}
			return _mapper.Map<TaskResultVm>(result);
        }
    }
}
