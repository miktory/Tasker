﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using System.Reflection;
using Tasker.Application.Common.Mappings;
using Tasker.Persistence;
using Tasker.Application;
using Tasker.Application.Interfaces;
using Tasker.WebApi.Middleware;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore.Scaffolding;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using Tasker.WebApi;
using Tasker.Messaging.Kafka;
using Tasker.Application.Tasks.Messages.TaskResultReceived;
using Tasker.Shared.Dto;


namespace Notes.WebApi
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration) => Configuration = configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(config =>
            {
                config.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
                config.AddProfile(new AssemblyMappingProfile(typeof(IApplicationDbContext).Assembly));
                config.AddMaps(Assembly.GetExecutingAssembly());
            });

			//	services.AddAutoMapper(Assembly.GetExecutingAssembly());

			services.AddApplication(Configuration.GetSection("ServicesSettings"));
			services.AddConsumer<TaskResultReceivedMessage, TaskResultReceivedMessageHandler>(Configuration.GetSection("Kafka"));
			services.AddProducer<TaskDetailsDto>(Configuration.GetSection("Kafka"));
			//      services.AddScoped(typeof(IMessageProducer<>), typeof(KafkaProducer<>));

            services.AddPersistence(Configuration);
            services.AddControllers();

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.AllowAnyHeader();
                    policy.AllowAnyMethod();
                    policy.AllowAnyOrigin();
                });
            });


			var jwtSettings = Configuration.GetSection("JwtSettings");
			// Настройка аутентификации JWT
			services.AddAuthentication(config =>
            {
                config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })


                 //            .AddJwtBearer("Bearer", options =>
                 //            {
                 //                options.Authority = "https://localhost:7163";
                 //                options.Audience = "TaskerWebAPI";
                 //                options.RequireHttpsMetadata = false;
                 //});
                 .AddJwtBearer(options =>
                 {
                     options.Authority = jwtSettings["Authority"];
                     options.Audience = jwtSettings["Audience"];
                     options.RequireHttpsMetadata = bool.Parse(jwtSettings["RequireHttpsMetadata"]);
                 });

					 services.AddTransient<IConfigureOptions<SwaggerGenOptions>,
				   ConfigureSwaggerOptions>();

			services.AddSwaggerGen(config => {
              var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
              var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
              config.IncludeXmlComments(xmlPath);
			});


		}

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();
            app.UseSwaggerUI(config =>
            {
                config.RoutePrefix = String.Empty;
                config.SwaggerEndpoint("swagger/v1/swagger.json", "Tasker API");
            });
            app.UseCustomExceptionHander();
            app.UseRouting();
            app.UseHttpsRedirection();
            app.UseCors("AllowAll");
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}