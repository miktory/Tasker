using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasker.Application.Tasks.Queries.GetTaskDetails
{
    public class GetTaskDetailsQueryValidator : AbstractValidator<GetTaskDetailsQuery>
    {
        public GetTaskDetailsQueryValidator()
        {
            RuleFor(getTaskDetailsQuery => getTaskDetailsQuery.Id).NotEqual(Guid.Empty);
            RuleFor(getTaskDetailsQuery => getTaskDetailsQuery.UserId).NotEqual(Guid.Empty);
        }
    }
}
