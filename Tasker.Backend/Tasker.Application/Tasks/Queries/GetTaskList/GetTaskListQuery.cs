using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasker.Domain;

namespace Tasker.Application.Tasks.Queries.GetTaskList
{
    public class GetTaskListQuery : IRequest<TaskListVm>
    {
        public Guid UserId { get; set; }
    }
}
