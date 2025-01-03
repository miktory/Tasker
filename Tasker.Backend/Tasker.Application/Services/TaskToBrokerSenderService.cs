﻿using AutoMapper;
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
using Tasker.Application.Tasks.Commands.SendTaskToBroker;
using Tasker.Application.Tasks.Queries.GetWaitingTasks;
using Tasker.Application.Tasks.Queries.GetWaitingTasks.FullAccess;
using Tasker.Domain;
using Tasker.Shared.Dto;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Tasker.Application.Services
{
	public class TaskToBrokerSenderService : BackgroundService
	{
		private IMediator _mediator;

		private readonly IMessageProducer<TaskDetailsDto> _messageProducer;
		private readonly IMapper _mapper;
		private readonly IServiceScopeFactory _serviceScopeFactory;
		private readonly TaskToBrokerSenderServiceSettings _settings;

		public TaskToBrokerSenderService(IMessageProducer<TaskDetailsDto> messageProducer, IMapper mapper, IServiceScopeFactory serviceScopeFactory, IOptions<TaskToBrokerSenderServiceSettings> settings)
		{
			_messageProducer = messageProducer;
			_mapper = mapper;
			_serviceScopeFactory = serviceScopeFactory;
			_settings = settings.Value;
		}

		protected override async Task ExecuteAsync(CancellationToken stoppingToken)
		{
			while (!stoppingToken.IsCancellationRequested)
			{

				List<ParametrizedTask> tasks = new List<ParametrizedTask>();
				try
				{
					using (var scope = _serviceScopeFactory.CreateScope())
					{
						_mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
						using (var cancellationTokenSource = new CancellationTokenSource(30000)) // отмена операции через 30 секунд
						{
							var query = new GetWaitingTasksQuery();
							tasks = await _mediator.Send(query, cancellationTokenSource.Token);
						}


						foreach (var task in tasks)
						{
							using (var cancellationTokenSource = new CancellationTokenSource(5000))
							{
								var createTaskDto = _mapper.Map<CreateTaskDto>(task);
								var taskDetails = _mapper.Map<TaskDetailsDto>(createTaskDto);
								var command = _mapper.Map<SendTaskToBrokerCommand>(taskDetails);
								command.Id = task.Id;
								await _mediator.Send(command, cancellationTokenSource.Token);
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
