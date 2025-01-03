using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Tasker.Application.Tasks.Queries.GetTaskList;
using System.Threading.Tasks;
using Tasker.Application.Tasks.Queries.GetTaskDetails;
using Tasker.Shared.Dto;
using AutoMapper;
using Tasker.Application.Tasks.Commands.CreateTask;
using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Authorization;
using Tasker.Shared.Vm;
using Tasker.Application.Tasks.Commands.SendTaskToBroker;
using Tasker.Application.Tasks.Queries.GetWaitingTasks;
using System.Threading;
using Tasker.Application.Tasks.Commands.TurnTaskIntoProgress;
using Tasker.Application.Tasks.Queries.GetTaskResult;

namespace Tasker.WebApi.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class TaskController : BaseController
    {
        private readonly IMapper _mapper;
        public TaskController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<TaskListVm>> GetAll()
        {
            var query = new GetTaskListQuery
            {
                UserId = UserId
            };
            var vm = await Mediator.Send(query);
            return Ok(vm);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TaskDetailsVm>> Get(Guid id)
        {
            var query = new GetTaskDetailsQuery
            {
                UserId = UserId,
                Id = id
            };
            var vm = await Mediator.Send(query);
            return Ok(vm);
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> Create([FromBody] CreateTaskDto createTaskDto)
        {
	
			var command = _mapper.Map<CreateTaskCommand>(createTaskDto);
            command.UserId = UserId;
            var taskId = Guid.Empty;

			using (var cancellationTokenSource = new CancellationTokenSource(30000)) // отмена операции через 30 секунд
			{
				taskId = await Mediator.Send(command, cancellationTokenSource.Token);
			}

            var taskDetails = _mapper.Map<TaskDetailsDto>(createTaskDto);
			var kafkaCommand = _mapper.Map<SendTaskToBrokerCommand>(taskDetails);

			kafkaCommand.Id = taskId;

			using (var cancellationTokenSource = new CancellationTokenSource(30000)) // отмена операции через 30 секунд
			{
				await Mediator.Send(kafkaCommand, cancellationTokenSource.Token);
			}

			return Ok(taskId);  
        }

        [Authorize(Roles = "Admin")]
		[HttpPatch("request/{id}")]
		public async Task<ActionResult<string>> RequestTaskProcessing(Guid id, string workerName)
		{
            var cmd = new TurnTaskIntoProgressCommand { TaskId = id, WorkerName = workerName };
            var result = "";
			using (var cancellationTokenSource = new CancellationTokenSource(30000)) // отмена операции через 30 секунд
			{
                result = await Mediator.Send(cmd);
			}
			return Ok(result);
		}

		[HttpGet("{id}/result")]
		public async Task<ActionResult<TaskResultVm>> GetResult(Guid id)
		{
			var cmd = new GetTaskResultQuery { TaskId = id, UserId = UserId };
            var result = new TaskResultVm();
			using (var cancellationTokenSource = new CancellationTokenSource(30000)) // отмена операции через 30 секунд
			{
				result = await Mediator.Send(cmd);
			}
			return Ok(result);
		}
	}
}
