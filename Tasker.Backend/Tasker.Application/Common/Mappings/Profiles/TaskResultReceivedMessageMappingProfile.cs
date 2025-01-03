using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasker.Application.Tasks.Messages.TaskResultReceived;
using Tasker.Domain;
using Tasker.Shared.Dto;

namespace Tasker.Application.Common.Mappings.Profiles
{
	public class TaskResultReceivedMessageMappingProfile : Profile
	{
		public TaskResultReceivedMessageMappingProfile()
		{
			Mapping(this);
		}

		public void Mapping(Profile profile)
		{
			profile.CreateMap<TaskResultReceivedMessage, ParametrizedTaskResult>()
			.ForMember(res => res.Result, opt => opt.MapFrom(msg => msg.Result))
			.ForMember(res => res.ParametrizedTaskId, opt => opt.MapFrom(msg => msg.ParametrizedTaskId))
			.ForMember(res => res.Id, opt => opt.MapFrom(msg => msg.Id));
		}
	}
}
