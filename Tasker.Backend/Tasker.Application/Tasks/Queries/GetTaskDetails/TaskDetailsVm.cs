using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Tasker.Application.Common.Mappings;
using Tasker.Domain;

namespace Tasker.Application.Tasks.Queries.GetTaskDetails
{
    public class TaskDetailsVm: IMapWith<ParametrizedTask>
    {
        public Guid Id { get; set; }
        public string Type { get; set; }
        public JsonDocument Parameters { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int TTL { get; set; }
        public string Status { get; set; }
		public string? WorkerName { get; set; }

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
