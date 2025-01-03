using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasker.Domain;
using Tasker.Shared.Dto;

namespace Tasker.Application.Common.Mappings.Profiles
{
	public class TaskLookupDtoMappingProfile : Profile
	{
		public TaskLookupDtoMappingProfile()
		{
			Mapping(this);
		}
		public void Mapping(Profile profile)
		{
			profile.CreateMap<ParametrizedTask, TaskLookupDto>()
				.ForMember(taskDto => taskDto.Id, opt => opt.MapFrom(parametrizedTask => parametrizedTask.Id))
				.ForMember(taskDto => taskDto.Type, opt => opt.MapFrom(parametrizedTask => parametrizedTask.Type))
				.ForMember(taskDto => taskDto.Status, opt => opt.MapFrom(parametrizedTask => parametrizedTask.Status));
		}
	}
}




