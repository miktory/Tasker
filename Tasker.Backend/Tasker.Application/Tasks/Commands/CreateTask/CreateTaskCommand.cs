using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using MediatR;

namespace Tasker.Application.Tasks.Commands.CreateTask
{
    public class CreateTaskCommand: IRequest<Guid>
    {
        public Guid UserId { get; set; }
        public string Type { get; set; }
        public JsonDocument? Parameters { get; set; }
        public int TTL { get; set; }
    }
}
