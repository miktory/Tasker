using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Tasker.Identity.Models;

namespace Tasker.Identity.Data
{
    public class DbInitializer
    {
        public static void Initialize(AuthDbContext context, RoleManager<IdentityRole> roleManager)
        {
		    context.Database.Migrate();
			CreateRoles(roleManager).Wait();

		}

		private static async Task CreateRoles(RoleManager<IdentityRole> roleManager)
		{
			string[] roleNames = { "Admin", "User" };

			foreach (var roleName in roleNames)
			{
				if (!await roleManager.RoleExistsAsync(roleName))
				{
					await roleManager.CreateAsync(new IdentityRole(roleName));
				}
			}
		}
	}
}
