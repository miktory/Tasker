using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasker.Application.Tasks.Commands.CreateTask;
using Tasker.Application.Tasks.Commands.SendTaskToBroker;
using Tasker.Shared.Dto;

namespace Tasker.Application.Common.Mappings.Profiles
{
	public class CreateTaskDtoMappingProfile : Profile
	{
		public CreateTaskDtoMappingProfile() 
		{
			Mapping(this);
		}

		public void Mapping(Profile profile)
		{
			profile.CreateMap<CreateTaskDto, CreateTaskCommand>()
				.ForMember(taskCommand => taskCommand.Type, opt => opt.MapFrom(taskDto => taskDto.Type))
				.ForMember(taskCommand => taskCommand.Parameters, opt => opt.MapFrom(taskDto => taskDto.Parameters))
				.ForMember(taskCommand => taskCommand.TTL, opt => opt.MapFrom(taskDto => taskDto.TTL));

			profile.CreateMap<CreateTaskDto, TaskDetailsDto>()
				.ForMember(taskDetailsDto => taskDetailsDto.Type, opt => opt.MapFrom(taskCreateDto => taskCreateDto.Type))
				.ForMember(taskDetailsDto => taskDetailsDto.Parameters, opt => opt.MapFrom(taskCreateDto => taskCreateDto.Parameters))
				.ForMember(taskDetailsDto => taskDetailsDto.TTL, opt => opt.MapFrom(taskCreateDto => taskCreateDto.TTL));
		}
	}
}
