using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Tasker.Application.Interfaces;

namespace Tasker.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration) 
        {
            var connectionString = configuration["DbConnection"];
            services.AddDbContext<ParametrizedTasksDbContext>(options =>
            {
                options.UseNpgsql(connectionString);
            });
            services.AddScoped<IParametrizedTasksDbContext>(provider => provider.GetService<ParametrizedTasksDbContext>());
            return services;
        }
    }
}
