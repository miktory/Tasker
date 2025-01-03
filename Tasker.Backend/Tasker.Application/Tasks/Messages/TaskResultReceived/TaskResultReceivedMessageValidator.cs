using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasker.Application.Tasks.Messages.TaskResultReceived
{
    public class TaskResultReceivedMessageValidator : AbstractValidator<TaskResultReceivedMessage>
    {
        public TaskResultReceivedMessageValidator()
        {
            RuleFor(msg => msg.ParametrizedTaskId).NotEqual(Guid.Empty);
        }
    }
}
