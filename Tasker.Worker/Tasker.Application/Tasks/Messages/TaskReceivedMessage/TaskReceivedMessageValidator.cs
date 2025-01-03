using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasker.Application.Tasks.Messages.TaskReceivedMessage
{
    public class TaskReceivedMessageValidator : AbstractValidator<TaskReceivedMessage>
    {
        public TaskReceivedMessageValidator()
        {
            RuleFor(msg => msg.Id).NotEqual(Guid.Empty);
            RuleFor(msg => msg.TTL).GreaterThanOrEqualTo(0);
		}
    }
}
