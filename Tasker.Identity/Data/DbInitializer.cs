using Microsoft.EntityFrameworkCore;

namespace Tasker.Identity.Data
{
    public class DbInitializer
    {
        public static void Initialize(AuthDbContext context)
        {
            context.Database.EnsureCreated();
		    context.Database.Migrate();
		}
    }
}
