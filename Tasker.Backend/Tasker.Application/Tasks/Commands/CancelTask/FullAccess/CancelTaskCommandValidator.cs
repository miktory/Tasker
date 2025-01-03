using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasker.Application.Tasks.Commands.CreateTask;

namespace Tasker.Application.Tasks.Commands.CancelTask.FullAccess
{
    public class CancelTaskCommandValidator: AbstractValidator<CancelTaskCommand>
    {
        public CancelTaskCommandValidator()
        {
            RuleFor(cancelTaskCommand => cancelTaskCommand.Id).NotEqual(Guid.Empty);
        }
    }
}
