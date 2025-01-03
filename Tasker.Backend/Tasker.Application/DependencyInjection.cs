using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Tasker.Application.Common.Behaviors;
using Tasker.Application.Interfaces;
using Tasker.Application.Services;
using Tasker.Application.Common.Mappings;
using Microsoft.Extensions.Configuration;


namespace Tasker.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfigurationSection configurationSection) 
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssemblies(new[] { Assembly.GetExecutingAssembly() }); 
            services.AddTransient(typeof(IPipelineBehavior<,>),
                typeof(ValidationBehavior<,>));
			services.AddSingleton<ICustomMapper, AutoMapperWrapper>();
			services.Configure<TaskInfoUpdaterServiceSettings>(configurationSection.GetSection("TaskInfoUpdater"));
			services.Configure<TaskToBrokerSenderServiceSettings>(configurationSection.GetSection("TaskToBrokerSender"));
			services.AddHostedService<TaskInfoUpdaterService>();
			services.AddHostedService<TaskToBrokerSenderService>();
			return services;
        }
    }
}
