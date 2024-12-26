using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using MediatR;

namespace Tasker.Messaging.Kafka.Commands.SendMessageToKafka
{
    public class SendMessageToKafkaCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
        public string Type { get; set; }
        public JsonDocument Parameters { get; set; }
        public int TTL { get; set; }
    }
}
