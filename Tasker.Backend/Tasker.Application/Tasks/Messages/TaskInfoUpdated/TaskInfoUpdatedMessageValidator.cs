using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasker.Application.Tasks.Messages.TaskInfoUpdated
{
    public class TaskInfoUpdatedMessageValidator : AbstractValidator<TaskInfoUpdatedMessage>
    {
        public TaskInfoUpdatedMessageValidator()
        {
            RuleFor(msg => msg.Id).NotEqual(Guid.Empty);
            RuleFor(msg => msg.StartDate).LessThanOrEqualTo(msg => msg.EndDate)
				.When(msg => msg.EndDate.HasValue);
        }
    }
}
