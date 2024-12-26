using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using MediatR;

namespace Tasker.Application.Tasks.Commands.SendTaskToKafka
{
    public class SendTaskToKafkaCommand : IRequest
    {
        public Guid Id { get; set; }
        public string Type { get; set; }
        public JsonDocument Parameters { get; set; }
        public int TTL { get; set; }
    }
}
