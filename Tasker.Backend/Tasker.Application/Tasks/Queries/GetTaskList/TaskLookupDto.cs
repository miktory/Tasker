using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasker.Application.Common.Mappings;
using Tasker.Domain;

namespace Tasker.Application.Tasks.Queries.GetTaskList
{
    public class TaskLookupDto : IMapWith<ParametrizedTask>
    {
        public Guid Id { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ParametrizedTask, TaskLookupDto>()
                .ForMember(taskDto => taskDto.Id, opt => opt.MapFrom(parametrizedTask => parametrizedTask.Id))
                .ForMember(taskDto => taskDto.Type, opt => opt.MapFrom(parametrizedTask => parametrizedTask.Type))
                .ForMember(taskDto => taskDto.Status, opt => opt.MapFrom(parametrizedTask => parametrizedTask.Status));
        }

    }
}
