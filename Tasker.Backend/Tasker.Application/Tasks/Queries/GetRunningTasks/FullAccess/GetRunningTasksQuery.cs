using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Tasker.Domain;
using Tasker.Shared.Vm;

namespace Tasker.Application.Tasks.Queries.GetRunningTasks.FullAccess
{
    public class GetRunningTasksQuery : IRequest<List<ParametrizedTask>>
    {
    }
}
