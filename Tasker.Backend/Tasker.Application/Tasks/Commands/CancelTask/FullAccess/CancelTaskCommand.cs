using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Tasker.Application.Tasks.Commands.CancelTask.FullAccess
{
    public class CancelTaskCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
