using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasker.Application.Common.Mappings;
using Tasker.Application.Interfaces;
using Tasker.Domain;

namespace Tasker.Application.Tasks.Messages.TaskReceivedMessage
{
    public class TaskReceivedMessageHandler(ILogger<TaskReceivedMessageHandler> logger, ITaskProcessor processor, ICustomMapper mapper) :
        IMessageHandler<TaskReceivedMessage>
    {
        public async Task HandleAsync(TaskReceivedMessage message, CancellationToken cancellationToken)
        {
            logger.LogInformation($"New task received. Start processing... {message.Id}");
            var task = mapper.Map<TaskReceivedMessage, ParametrizedTask>(message);
            while (!processor.RequestTaskProcessing(task))
            {
                await Task.Delay(10000);
            }

        }
    }
}
