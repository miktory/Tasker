using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasker.Domain;
using Tasker.Shared.Vm;

namespace Tasker.Application.Common.Mappings.Profiles
{
	public class TaskDetailsVmMappingProfile : Profile
	{
		public TaskDetailsVmMappingProfile() 
		{
			Mapping(this);
		}
		public void Mapping(Profile profile)
		{
			profile.CreateMap<ParametrizedTask, TaskDetailsVm>()
				.ForMember(taskVm => taskVm.Type, opt => opt.MapFrom(parametrizedTask => parametrizedTask.Type))
				.ForMember(taskVm => taskVm.Parameters, opt => opt.MapFrom(parametrizedTask => parametrizedTask.Parameters))
				.ForMember(taskVm => taskVm.Id, opt => opt.MapFrom(parametrizedTask => parametrizedTask.Id))
				.ForMember(taskVm => taskVm.CreationDate, opt => opt.MapFrom(parametrizedTask => parametrizedTask.CreationDate))
				.ForMember(taskVm => taskVm.StartDate, opt => opt.MapFrom(parametrizedTask => parametrizedTask.StartDate))
				.ForMember(taskVm => taskVm.EndDate, opt => opt.MapFrom(parametrizedTask => parametrizedTask.EndDate))
				.ForMember(taskVm => taskVm.TTL, opt => opt.MapFrom(parametrizedTask => parametrizedTask.TTL))
				.ForMember(taskVm => taskVm.Status, opt => opt.MapFrom(parametrizedTask => parametrizedTask.Status))
				.ForMember(taskVm => taskVm.WorkerName, opt => opt.MapFrom(parametrizedTask => parametrizedTask.WorkerName));
		}
	}
}
