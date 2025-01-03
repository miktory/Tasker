using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasker.Application.Tasks.Queries.GetTaskDetails.FullAccess
{
    public class GetTaskDetailsQueryValidator : AbstractValidator<GetTaskDetailsQuery>
    {
        public GetTaskDetailsQueryValidator()
        {
            RuleFor(getTaskDetailsQuery => getTaskDetailsQuery.Id).NotEqual(Guid.Empty);
        }
    }
}
