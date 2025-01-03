using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasker.Application.Interfaces;
using Tasker.Shared.Dto;


namespace Tasker.Application.Tasks.Commands.SendTaskToBroker
{
    public class SendTaskToBrokerCommandHandler : IRequestHandler<SendTaskToBrokerCommand>
    {
        private readonly IMessageProducer<TaskDetailsDto> _kafkaProducer;
        public SendTaskToBrokerCommandHandler(IMessageProducer<TaskDetailsDto> kafkaProducer)
        {
            _kafkaProducer = kafkaProducer;
        }

        public async Task<Unit> Handle(SendTaskToBrokerCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var taskDetailsDto = new TaskDetailsDto
                {
                    Id = request.Id,
                    Parameters = request.Parameters,
                    TTL = request.TTL,
                    Type = request.Type,
                };
                await _kafkaProducer.ProduceAsync(taskDetailsDto, cancellationToken);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
			}
			return new Unit();
		}
    }
}
