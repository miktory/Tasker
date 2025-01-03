using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Tasker.Application.Tasks.Commands.SendTaskToBroker
{
    public class SendTaskToBrokerCommandValidator : AbstractValidator<SendTaskToBrokerCommand>
    {
        public SendTaskToBrokerCommandValidator() 
        {
            RuleFor(createTaskCommand => createTaskCommand.TTL).GreaterThanOrEqualTo(0);
            RuleFor(createTaskCommand => createTaskCommand.Id).NotEqual(Guid.Empty);
            RuleFor(createTaskCommand => createTaskCommand.Type).NotEmpty().MaximumLength(100);
            RuleFor(createTaskCommand => createTaskCommand.Parameters.ToString()).MaximumLength(100);
		}
    }
}
