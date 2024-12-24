using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Tasker.Application.Tasks.Commands.CreateTask 
{
    public class CreateTaskCommandValidator : AbstractValidator<CreateTaskCommand>
    {
        public CreateTaskCommandValidator() 
        {
            RuleFor(createTaskCommand => createTaskCommand.TTL).GreaterThanOrEqualTo(0);
            RuleFor(createTaskCommand => createTaskCommand.UserId).NotEqual(Guid.Empty);
            RuleFor(createTaskCommand => createTaskCommand.Type).NotEmpty().MaximumLength(100);
            RuleFor(createTaskCommand => createTaskCommand.Parameters.ToString()).MaximumLength(100);

        }
    }
}
