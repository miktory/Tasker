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
using Tasker.Messaging.Kafka;
using Tasker.Messaging.Kafka.Commands.SendMessageToKafka;

namespace Tasker.WebApi.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class TaskController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IKafkaProducer<Guid> _kafkaProducer;
        public TaskController(IMapper mapper, IKafkaProducer<Guid> kafkaProducer)
        {
            _mapper = mapper;
            _kafkaProducer = kafkaProducer;
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
            var taskId = await Mediator.Send(command);
            var kafkaCommand = _mapper.Map<SendMessageToKafkaCommand>(createTaskDto);
            //kafkaCommand.Id = taskId;
            await Mediator.Send(kafkaCommand);
            return Ok(taskId);  
        }
  }
}
