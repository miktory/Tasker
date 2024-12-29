using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasker.Application.Interfaces;

namespace Tasker.Application.Tasks.Messages.TaskInfoUpdated
{
    public class TaskInfoUpdatedMessageHandler(ILogger<TaskInfoUpdatedMessageHandler> logger) :
        IMessageHandler<TaskInfoUpdatedMessage>
    {
        public Task HandleAsync(TaskInfoUpdatedMessage message, CancellationToken cancellationToken)
        {
            logger.LogInformation($"New task info received. Updating data. {message.Id}");

            return Task.CompletedTask;
        }
    }
}
