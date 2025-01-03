using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasker.Application.Tasks.Queries.GetTaskResult
{
    public class GetTaskResultQueryValidator : AbstractValidator<GetTaskResultQuery>
    {
        public GetTaskResultQueryValidator()
        {
            RuleFor(query => query.TaskId).NotEqual(Guid.Empty);
        }
    }
}
