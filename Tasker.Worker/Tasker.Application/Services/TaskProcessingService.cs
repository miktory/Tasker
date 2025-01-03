using AutoMapper;
using AutoMapper.Configuration.Annotations;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Tasker.Application.Interfaces;
using Tasker.Application.Operations.Commands.GenerateLogin;
using Tasker.Application.Operations.Commands.GenerateNumber;
using Tasker.Application.Tasks.Commands.SendTaskResultToBroker;
using Tasker.Application.Tasks.Messages.TaskReceivedMessage;
using Tasker.Domain;
using Tasker.Shared.Dto;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using static System.Formats.Asn1.AsnWriter;

namespace Tasker.Application.Services
{
	public class TaskProcessingService : BackgroundService, ITaskProcessor
	{
		private IMediator _mediator;
		private readonly IServiceScopeFactory _serviceScopeFactory;
		private readonly IIdentityApiClient _identityApiClient;
		private readonly ITaskerApiClient _taskerApiClient;

		Queue<ParametrizedTask> _queue;
		List<Task<ParametrizedTaskResult>> _taskInProcess;
		public int TaskLimit { get => _taskLimit; }
		public int TaskCount { get => _taskInProcess.Count(); }
		private readonly int _taskLimit;
		private Guid _name;
		private readonly ILogger<TaskProcessingService> _logger;
		private readonly TaskProcessingServiceSettings _settings;


		public TaskProcessingService(ILogger<TaskProcessingService> logger, IOptions<TaskProcessingServiceSettings> settings, IServiceScopeFactory serviceScopeFactory, IIdentityApiClient identityApiClient, ITaskerApiClient taskerApiClient)
		{
			_taskLimit = settings.Value.TaskLimit;
			_queue = new Queue<ParametrizedTask>();
			_taskInProcess = new List<Task<ParametrizedTaskResult>>();
			_serviceScopeFactory = serviceScopeFactory;
			_name = Guid.NewGuid();
			_logger = logger;
			_identityApiClient = identityApiClient;
			_settings = settings.Value;
			_taskerApiClient = taskerApiClient;
		}

		protected override async Task ExecuteAsync(CancellationToken stoppingToken)
		{

			while (!stoppingToken.IsCancellationRequested)
			{
				try
				{

					if (TaskCount < TaskLimit && _queue.Count > 0)
					{
						var task = new ParametrizedTask { Type = "None" };
						_queue.TryPeek(out task);

			

						if (task == null)
							task = new ParametrizedTask { Type = "None" };
						object? operation = null;
						Enum.TryParse(typeof(Operation), task.Type, true, out operation);
						if (operation == null)
							operation = Operation.None;
						var cts = new CancellationTokenSource(TimeSpan.FromMilliseconds(task.TTL));
						CancellationToken token = cts.Token;
						var taskAssignedToWorker = false;
						if ((Operation)operation != Operation.None)
						{
							using (var cancellationTokenSource = new CancellationTokenSource(10000))
							{
								try
								{
									var bearer = await _identityApiClient.AuthenticateAsync(_settings.AuthData.Username, _settings.AuthData.Password, _settings.AuthData.ClientId,
										cancellationTokenSource.Token);
									_taskerApiClient.SetAccessToken(bearer);

									// Пытаемся закрепить задачу за текущим Worker
									var worker = await _taskerApiClient.RequestTaskProcessing(task.Id, _settings.WorkerName,
										cancellationTokenSource.Token);
									worker = await _taskerApiClient.RequestTaskProcessing(task.Id, _settings.WorkerName,
										cancellationTokenSource.Token);
									if (worker == _settings.WorkerName)
										taskAssignedToWorker = true;
								}
								catch (Exception ex)
								{
									_logger.LogInformation(ex.Message);
								}
							}
						}
						if (taskAssignedToWorker)
						{
							switch ((Operation)operation)
							{
								case Operation.None:
									_logger.LogInformation($"Task {task.Id} skipped due to incorrect operation type.");
									break;

								case Operation.GENERATE_LOGIN:
									_logger.LogInformation($"Started generate_login procedure for task {task.Id}.");
									var taskInProcess = Task.Run(async () =>
									{
										using (var scope = _serviceScopeFactory.CreateScope())
										{
											var _mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
											return await _mediator.Send(new GenerateLoginCommand
											{
												TaskId = task.Id,
												Length = 10
											}, token);
										}
									});

									_taskInProcess.Add(taskInProcess);
									break;

								case Operation.GENERATE_NUMBER:
									_logger.LogInformation($"Started generate_number procedure for task {task.Id}.");
									taskInProcess = Task.Run(async () =>
									{
										using (var scope = _serviceScopeFactory.CreateScope())
										{
											var _mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
											return await _mediator.Send(new GenerateNumberCommand
											{
												TaskId = task.Id,
												Length = 8
											}, token);
										}
									});
									_taskInProcess.Add(taskInProcess);
									break;

							}
						}
					}

					_taskInProcess.RemoveAll(t => t.IsCompleted && !t.IsCompletedSuccessfully);
					foreach (var task in _taskInProcess)
					{
						using (var scope = _serviceScopeFactory.CreateScope())
						{
							var _mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
							if (task.IsCompletedSuccessfully)
							{
								var cts = new CancellationTokenSource(TimeSpan.FromMinutes(1));
								CancellationToken token = cts.Token;
								var cmd = new SendTaskResultToBrokerCommand { ParametrizedTaskResult = task.Result };
								_logger.LogInformation($"Task {task.Result.ParametrizedTaskId} successfully processed!");

								await _mediator.Send(cmd, token);
								_logger.LogInformation($"Task {task.Result.ParametrizedTaskId} result has just been sent to broker.");
							}
						}
					}
					_taskInProcess.RemoveAll(t => t.IsCompletedSuccessfully);


				}
				catch (Exception ex)
				{
					_logger.LogError(ex.Message, "Error occurred while processing tasks.");
				}
				finally
				{
					_queue.TryDequeue(out var task);
				}
				await Task.Delay(TimeSpan.FromMilliseconds(_settings.IterationDelayInMs), stoppingToken);
			}
		}

		public override Task StopAsync(CancellationToken cancellationToken)
		{
			return base.StopAsync(cancellationToken);
		}

		public bool RequestTaskProcessing(ParametrizedTask task)
		{
			if (TaskCount < TaskLimit && _queue.Count < TaskLimit)
			{
				_queue.Enqueue(task);
				return true;
			}
			return false;
		}
	}
}
