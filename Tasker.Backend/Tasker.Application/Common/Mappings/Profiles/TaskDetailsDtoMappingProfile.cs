using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasker.Application.Tasks.Commands.CreateTask;
using Tasker.Application.Tasks.Commands.SendTaskToBroker;
using Tasker.Domain;
using Tasker.Shared.Dto;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Tasker.Application.Common.Mappings.Profiles
{
	public class TaskDetailsDtoMappingProfile : Profile
	{
		public TaskDetailsDtoMappingProfile()
		{
			Mapping(this);
		}

		public void Mapping(Profile profile)
		{
			profile.CreateMap<ParametrizedTask, TaskDetailsDto>()
				.ForMember(taskVm => taskVm.Type, opt => opt.MapFrom(parametrizedTask => parametrizedTask.Type))
				.ForMember(taskVm => taskVm.Parameters, opt => opt.MapFrom(parametrizedTask => parametrizedTask.Parameters))
				.ForMember(taskVm => taskVm.Id, opt => opt.MapFrom(parametrizedTask => parametrizedTask.Id))
				.ForMember(taskVm => taskVm.TTL, opt => opt.MapFrom(parametrizedTask => parametrizedTask.TTL));

			profile.CreateMap<TaskDetailsDto, SendTaskToBrokerCommand>()
	.ForMember(command => command.Type, opt => opt.MapFrom(taskDto => taskDto.Type))
	.ForMember(command => command.Parameters, opt => opt.MapFrom(taskDto => taskDto.Parameters))
	.ForMember(command => command.TTL, opt => opt.MapFrom(taskDto => taskDto.TTL))
	.ForMember(command => command.Id, opt => opt.MapFrom(taskDto => taskDto.Id));
		}
	}
}

