using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasker.Domain;
using Microsoft.EntityFrameworkCore;

namespace Tasker.Application.Interfaces
{
    public interface IParametrizedTasksDbContext
    {
        DbSet<ParametrizedTask> ParametrizedTasks { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

    }
}
