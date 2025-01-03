using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Tasker.Application.Operations.Commands.GenerateLogin 
{
    public class GenerateLoginCommandValidator : AbstractValidator<GenerateLoginCommand>
    {
        public GenerateLoginCommandValidator() 
        {
            RuleFor(createTaskCommand => createTaskCommand.TaskId).NotEqual(Guid.Empty);
            RuleFor(createTaskCommand => createTaskCommand.Length).GreaterThan(0);

        }
    }
}
