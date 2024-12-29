using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasker.Domain;
using Microsoft.EntityFrameworkCore;

namespace Tasker.Application.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<ParametrizedTask> ParametrizedTasks { get; set; }
		DbSet<ParametrizedTaskResult> ParametrizedTasksResults { get; set; }
		Task<int> SaveChangesAsync(CancellationToken cancellationToken);

    }
}
