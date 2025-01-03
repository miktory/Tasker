using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasker.Application.Tasks.Commands.TurnTaskIntoProgress;

namespace Tasker.Application.Tasks.Messages.TurnTaskIntoProgress
{
    public class TurnTaskIntoProgressCommandValidator : AbstractValidator<TurnTaskIntoProgressCommand>
    {
        public TurnTaskIntoProgressCommandValidator()
        {
            RuleFor(cmd => cmd.TaskId).NotEqual(Guid.Empty);
			RuleFor(cmd => cmd.WorkerName).NotEmpty();

		}
    }
}
