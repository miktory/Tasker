using AutoMapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Tasker.Application.Interfaces;
using Tasker.Application.Tasks.Commands.CancelTask.FullAccess;
using Tasker.Application.Tasks.Commands.SendTaskToBroker;
using Tasker.Application.Tasks.Queries.GetRunningTasks.FullAccess;
using Tasker.Application.Tasks.Queries.GetWaitingTasks;
using Tasker.Application.Tasks.Queries.GetWaitingTasks.FullAccess;
using Tasker.Domain;
using Tasker.Shared.Dto;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Tasker.Application.Services
{
	public class TaskInfoUpdaterService : BackgroundService
	{
		private IMediator _mediator;

		private readonly IMessageProducer<TaskDetailsDto> _messageProducer;
		private readonly IMapper _mapper;
		private readonly IServiceScopeFactory _serviceScopeFactory;
		private readonly TaskInfoUpdaterServiceSettings _settings;

		public TaskInfoUpdaterService(IServiceScopeFactory serviceScopeFactory, IOptions<TaskInfoUpdaterServiceSettings> settings)
		{
			_serviceScopeFactory = serviceScopeFactory;
			_settings = settings.Value;
		}

		protected override async Task ExecuteAsync(CancellationToken stoppingToken)
		{
			while (!stoppingToken.IsCancellationRequested)
			{
				var currentTime = DateTime.Now.ToUniversalTime();

				List<ParametrizedTask> tasks = new List<ParametrizedTask>();
				try
				{
					using (var scope = _serviceScopeFactory.CreateScope())
					{
						_mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
						using (var cancellationTokenSource = new CancellationTokenSource(30000)) // отмена операции через 30 секунд
						{
							var query = new GetRunningTasksQuery();
							tasks = await _mediator.Send(query, cancellationTokenSource.Token);
						}


						foreach (var task in tasks)
						{
							try
							{
								if (task.StartDate != null)
								{
									var maxTime = task.StartDate.Value.ToUniversalTime().AddMilliseconds(task.TTL);
									if (currentTime > maxTime)
									{
										var cmd = new CancelTaskCommand { Id = task.Id };

										using (var cancellationTokenSource = new CancellationTokenSource(5000))
										{
											await _mediator.Send(cmd, cancellationTokenSource.Token);
										}
									}
								}
							} catch (Exception ex) 
							{
								Console.WriteLine(ex.Message);
							}
						}
					}
				}
				catch (Exception ex)
				{
					// Логирование ошибок
					// Пример: _logger.LogError(ex, "Error occurred while processing tasks.");
				}
				await Task.Delay(TimeSpan.FromMilliseconds(_settings.IterationDelayInMs), stoppingToken);
			}
		}

		public override Task StopAsync(CancellationToken cancellationToken)
		{
			return base.StopAsync(cancellationToken);
		}
	}
}
