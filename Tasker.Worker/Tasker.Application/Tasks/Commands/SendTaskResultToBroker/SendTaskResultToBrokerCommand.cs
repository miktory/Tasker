using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasker.Domain;

namespace Tasker.Application.Tasks.Commands.SendTaskResultToBroker
{
    public class SendTaskResultToBrokerCommand : IRequest
    {
        public ParametrizedTaskResult ParametrizedTaskResult { get; set; }
    }
}
