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
	public class ParemetrizedTaskMappingProfile : Profile
	{
		public ParemetrizedTaskMappingProfile() 
		{
			Mapping(this);
		}

		public void Mapping(Profile profile)
		{
			profile.CreateMap<ParametrizedTask, CreateTaskDto>()
			.ForMember(dto => dto.Type, opt => opt.MapFrom(task => task.Type))
			.ForMember(dto => dto.Parameters, opt => opt.MapFrom(task => task.Parameters))
			.ForMember(dto => dto.TTL, opt => opt.MapFrom(task => task.TTL));
		}
	}
}
