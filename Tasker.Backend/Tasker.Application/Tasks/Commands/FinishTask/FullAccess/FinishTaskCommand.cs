using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Tasker.Application.Tasks.Commands.FinishTask.FullAccess
{
    public class FinishTaskCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
