using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Tasker.Application.Tasks.Queries.GetTaskList;
using System.Threading.Tasks;
using Tasker.Application.Tasks.Queries.GetTaskDetails;
using Tasker.WebApi.Models;
using AutoMapper;
using Tasker.Application.Tasks.Commands.CreateTask;
using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Authorization;
using Tasker.Application.Tasks.Commands.SendTaskToKafka;

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


			var kafkaCommand = _mapper.Map<SendTaskToKafkaCommand>(createTaskDto);
			kafkaCommand.Id = taskId;

			using (var cancellationTokenSource = new CancellationTokenSource(30000)) // отмена операции через 30 секунд
			{
				await Mediator.Send(kafkaCommand, cancellationTokenSource.Token);
			}

			return Ok(taskId);  
        }
  }
}
