using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Tasker.Application.Operations.Commands.GenerateNumber
{
    public class GenerateNumberCommandValidator : AbstractValidator<GenerateNumberCommand>
    {
        public GenerateNumberCommandValidator() 
        {
            RuleFor(cmd => cmd.Length).GreaterThan(0);
            RuleFor(cmd => cmd.TaskId).NotEqual(Guid.Empty);
        }
    }
}
