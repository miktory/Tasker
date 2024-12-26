using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasker.Messaging.Kafka.Commands.SendMessageToKafka
{
    public class SendMessageToKafkaCommandHandler : IRequestHandler<SendMessageToKafkaCommand, Guid>
    {
        private readonly IKafkaProducer<SendMessageToKafkaCommand> _kafkaProducer;
        public SendMessageToKafkaCommandHandler(IKafkaProducer<SendMessageToKafkaCommand> kafkaProducer)
        {
            _kafkaProducer = kafkaProducer;
        }

        public async Task<Guid> Handle(SendMessageToKafkaCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _kafkaProducer.ProduceAsync(request, CancellationToken.None);
                return request.Id;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
