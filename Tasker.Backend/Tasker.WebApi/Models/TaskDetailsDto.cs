using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Tasker.Application.Common.Mappings;
using Tasker.Domain;

namespace Tasker.WebApi.Models
{
    public class TaskDetailsDto: IMapWith<ParametrizedTask>
    {
        public Guid Id { get; set; }
        public string Type { get; set; }
        public JsonDocument Parameters { get; set; }
        public int TTL { get; set; }

		public void Mapping(Profile profile)
        {
            profile.CreateMap<ParametrizedTask, TaskDetailsDto>()
                .ForMember(taskVm => taskVm.Type, opt => opt.MapFrom(parametrizedTask => parametrizedTask.Type))
                .ForMember(taskVm => taskVm.Parameters, opt => opt.MapFrom(parametrizedTask => parametrizedTask.Parameters))
                .ForMember(taskVm => taskVm.Id, opt => opt.MapFrom(parametrizedTask => parametrizedTask.Id))
                .ForMember(taskVm => taskVm.TTL, opt => opt.MapFrom(parametrizedTask => parametrizedTask.TTL));
		}

    }
}
