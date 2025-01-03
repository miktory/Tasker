using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasker.Domain;
using Tasker.Shared.Dto;
using Tasker.Shared.Vm;

namespace Tasker.Application.Common.Mappings.Profiles
{
	public class ParemetrizedTaskResultMappingProfile : Profile
	{
		public ParemetrizedTaskResultMappingProfile()
		{
			Mapping(this);
		}

		public void Mapping(Profile profile)
		{
			profile.CreateMap<ParametrizedTaskResult, TaskResultVm>()
			.ForMember(vm => vm.Result, opt => opt.MapFrom(r => r.Result));
		}
	}
}
