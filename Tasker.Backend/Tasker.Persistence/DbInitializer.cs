using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasker.Persistence
{
    public class DbInitializer
    {
        public static void Initialize(ParametrizedTasksDbContext context)
        {
            context.Database.EnsureCreated();
        }
    }
}
