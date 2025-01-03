using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Tasker.Application.Tasks.Commands.SendTaskResultToBroker
{
    public class SendTaskToBrokerCommandValidator : AbstractValidator<SendTaskResultToBrokerCommand>
    {
        public SendTaskToBrokerCommandValidator() 
        {
		}
    }
}
