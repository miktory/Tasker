using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasker.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using AutoMapper.QueryableExtensions;

namespace Tasker.Application.Tasks.Queries.GetTaskList
{
    public class GetTaskListQueryHandler : IRequestHandler<GetTaskListQuery, TaskListVm>
    {
        private readonly IParametrizedTasksDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetTaskListQueryHandler(IParametrizedTasksDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }   

        public async Task<TaskListVm> Handle (GetTaskListQuery request, CancellationToken cancellationToken)
        {
            var tasksQuery = await _dbContext.ParametrizedTasks
                .Where(task => task.UserId == request.UserId)
                .ProjectTo<TaskLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return new TaskListVm { Tasks = tasksQuery };
        }
    }
}
