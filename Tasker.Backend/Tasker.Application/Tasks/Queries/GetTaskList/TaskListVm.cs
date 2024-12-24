using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Tasker.Application.Common.Mappings;
using Tasker.Application.Tasks.Queries.GetTaskDetails;
using Tasker.Domain;

namespace Tasker.Application.Tasks.Queries.GetTaskList
{
    public class TaskListVm
    {
        public List<TaskLookupDto> Tasks { get; set; }
    }
}
