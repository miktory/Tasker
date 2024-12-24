using System;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
namespace Tasker.WebApi
{
	public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
	{
		public ConfigureSwaggerOptions() { }
		public void Configure(SwaggerGenOptions options)
		{
			options.AddSecurityDefinition($"AuthToken",
				new OpenApiSecurityScheme
				{
					In = ParameterLocation.Header,
					Type = SecuritySchemeType.Http,
					BearerFormat = "JWT",
					Scheme = "bearer",
					Name = "Authorization",
					Description = "Authorization token"
				});
			options.AddSecurityRequirement(new OpenApiSecurityRequirement
				{
					{
						new OpenApiSecurityScheme
						{
							Reference = new OpenApiReference
							{
								Type = ReferenceType.SecurityScheme,
								Id = $"AuthToken"
							}
						},
						new string[] { }
					}
				});
		}
	}
}