using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasker.Application.Tasks.Messages.TaskReceivedMessage;
using Tasker.Domain;
using Tasker.Shared.Dto;

namespace Tasker.Application.Common.Mappings.Profiles
{
	public class TaskReceivedMessageMappingProfile: Profile
	{
		public TaskReceivedMessageMappingProfile()
		{
			Mapping(this);
		}

		public void Mapping(Profile profile)
		{
			profile.CreateMap<TaskReceivedMessage, ParametrizedTask > ()
			.ForMember(task => task.Type, opt => opt.MapFrom(msg => msg.Type))
			.ForMember(task => task.Parameters, opt => opt.MapFrom(msg => msg.Parameters))
			.ForMember(task => task.TTL, opt => opt.MapFrom(msg => msg.TTL))
			.ForMember(task => task.Id, opt => opt.MapFrom(msg => msg.Id));
		}
	}
}
