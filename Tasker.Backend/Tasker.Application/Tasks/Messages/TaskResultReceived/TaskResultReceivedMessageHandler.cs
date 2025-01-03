using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasker.Application.Common.Mappings;
using Tasker.Application.Interfaces;
using Tasker.Application.Tasks.Commands.CancelTask.FullAccess;
using Tasker.Application.Tasks.Commands.CreateTaskResult;
using Tasker.Application.Tasks.Commands.FinishTask.FullAccess;
using Tasker.Application.Tasks.Queries.GetTaskDetails.FullAccess;
using Tasker.Domain;
using Tasker.Shared.Vm;

namespace Tasker.Application.Tasks.Messages.TaskResultReceived
{
    public class TaskResultReceivedMessageHandler(ILogger<TaskResultReceivedMessageHandler> logger, ICustomMapper mapper, IServiceScopeFactory serviceScopeFactory) :
        IMessageHandler<TaskResultReceivedMessage>
    {

        public async Task HandleAsync(TaskResultReceivedMessage message, CancellationToken cancellationToken)
        {
            try
            {
				var currentTime = DateTime.Now.ToUniversalTime();
                logger.LogInformation($"New task result received. Updating data. {message.ParametrizedTaskId}");
                var res = mapper.Map<TaskResultReceivedMessage, ParametrizedTaskResult>(message);

                using (var scope = serviceScopeFactory.CreateScope())
                {
					var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
                    var taskDetailsVm = new TaskDetailsVm();
					using (var cancellationTokenSource = new CancellationTokenSource(5000))
					{
						var cmd = new GetTaskDetailsQuery { Id = res.ParametrizedTaskId };
						taskDetailsVm = await mediator.Send(cmd, cancellationToken);
					}

					if (taskDetailsVm.Status == "FINISHED" || taskDetailsVm.Status == "CANCELLED")
						return;

					if (taskDetailsVm.StartDate != null)
					{
						var maxTime = taskDetailsVm.StartDate.Value.ToUniversalTime().AddMilliseconds(taskDetailsVm.TTL);
						if (currentTime > maxTime)
						{
							var cmd = new CancelTaskCommand { Id = taskDetailsVm.Id };
							using (var cancellationTokenSource = new CancellationTokenSource(5000))
							{
								await mediator.Send(cmd, cancellationTokenSource.Token);
							}
							return;
						}
						else
						{
							
							using (var cancellationTokenSource = new CancellationTokenSource(5000))
							{
								var cmd = new FinishTaskCommand { Id = taskDetailsVm.Id };
								await mediator.Send(cmd, cancellationTokenSource.Token);
							}

							using (var cancellationTokenSource = new CancellationTokenSource(5000))
							{
								var cmd = new CreateTaskResultCommand { result = res };
								await mediator.Send(cmd, cancellationToken);
							}

						}

					}

				
				

				}

                
            }
            catch (Exception ex)
            { 
                logger.LogWarning($"Result processing failed! {ex.Message}"); 
            }
        }
    }
}
