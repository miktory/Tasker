﻿using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasker.Application.Common.Exceptions;
using Tasker.Application.Interfaces;
using Tasker.Domain;
using Tasker.Shared.Vm;

namespace Tasker.Application.Tasks.Queries.GetWaitingTasks.FullAccess
{
    public class GetWaitingTasksQueryHandler : IRequestHandler<GetWaitingTasksQuery, List<ParametrizedTask>>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetWaitingTasksQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<List<ParametrizedTask>> Handle(GetWaitingTasksQuery request, CancellationToken cancellationToken)
        {
            var tasks = await _dbContext.ParametrizedTasks.Where(t => t.Status == "CREATED").ToListAsync();

            if (tasks == null)
            {
                throw new NotFoundException(nameof(ParametrizedTask), null);
            }
            return tasks;

        }
    }

}
