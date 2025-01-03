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

namespace Tasker.Application.Tasks.Queries.GetTaskDetails.FullAccess
{
    public class GetTaskDetailsQueryHandler : IRequestHandler<GetTaskDetailsQuery, TaskDetailsVm>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetTaskDetailsQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<TaskDetailsVm> Handle(GetTaskDetailsQuery request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.ParametrizedTasks
                .FirstOrDefaultAsync(task => task.Id == request.Id, cancellationToken);

            if (entity == null) 
            {
                throw new NotFoundException(nameof(ParametrizedTask), request.Id);
            }
            return _mapper.Map<TaskDetailsVm>(entity);
        }
    }
}
