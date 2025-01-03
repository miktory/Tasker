using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Tasker.Application.Tasks.Commands.CreateTaskResult
{
    public class CreateTaskResultCommandValidator : AbstractValidator<CreateTaskResultCommand>
    {
        public CreateTaskResultCommandValidator() 
        {
            RuleFor(createResult => createResult.result.ParametrizedTaskId).NotEmpty();
		}
    }
}
