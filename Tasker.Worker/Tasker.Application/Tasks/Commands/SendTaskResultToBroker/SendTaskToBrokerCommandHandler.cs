using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasker.Application.Interfaces;
using Tasker.Domain;
using Tasker.Shared.Dto;


namespace Tasker.Application.Tasks.Commands.SendTaskResultToBroker
{
    public class SendTaskToBrokerCommandHandler : IRequestHandler<SendTaskResultToBrokerCommand>
    {
        private readonly IMessageProducer<ParametrizedTaskResult> _kafkaProducer;
        public SendTaskToBrokerCommandHandler(IMessageProducer<ParametrizedTaskResult> kafkaProducer)
        {
            _kafkaProducer = kafkaProducer;
        }

        public async Task<Unit> Handle(SendTaskResultToBrokerCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _kafkaProducer.ProduceAsync(request.ParametrizedTaskResult, cancellationToken);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
			}
			return new Unit();
		}
    }
}
