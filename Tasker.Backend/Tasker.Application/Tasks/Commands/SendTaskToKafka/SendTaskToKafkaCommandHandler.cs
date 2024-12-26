using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasker.Application.Interfaces;

namespace Tasker.Application.Tasks.Commands.SendTaskToKafka
{
    public class SendTaskToKafkaCommandHandler : IRequestHandler<SendTaskToKafkaCommand>
    {
        private readonly IKafkaProducer<SendTaskToKafkaCommand> _kafkaProducer;
        public SendTaskToKafkaCommandHandler(IKafkaProducer<SendTaskToKafkaCommand> kafkaProducer)
        {
            _kafkaProducer = kafkaProducer;
        }

        public async Task<Unit> Handle(SendTaskToKafkaCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _kafkaProducer.ProduceAsync(request, cancellationToken);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
			}
			return new Unit();
		}
    }
}
