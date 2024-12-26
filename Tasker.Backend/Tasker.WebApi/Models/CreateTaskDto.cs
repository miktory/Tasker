using AutoMapper;
using System.Text.Json;
using Tasker.Application.Common.Mappings;
using Tasker.Application.Tasks.Commands.CreateTask;
using Tasker.Messaging.Kafka.Commands.SendMessageToKafka;

namespace Tasker.WebApi.Models
{
    public class CreateTaskDto : IMapWith<CreateTaskCommand>, IMapWith<SendMessageToKafkaCommand>
    {
        public string Type { get; set; }
        public JsonDocument? Parameters { get; set; }
        public int TTL { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateTaskDto, CreateTaskCommand>()
                .ForMember(taskCommand => taskCommand.Type, opt => opt.MapFrom(taskDto => taskDto.Type))
                .ForMember(taskCommand => taskCommand.Parameters, opt => opt.MapFrom(taskDto => taskDto.Parameters))
                .ForMember(taskCommand => taskCommand.TTL, opt => opt.MapFrom(taskDto => taskDto.TTL));

            profile.CreateMap<CreateTaskDto, SendMessageToKafkaCommand>()
                .ForMember(taskCommand => taskCommand.Type, opt => opt.MapFrom(taskDto => taskDto.Type))
                .ForMember(taskCommand => taskCommand.Parameters, opt => opt.MapFrom(taskDto => taskDto.Parameters))
                .ForMember(taskCommand => taskCommand.TTL, opt => opt.MapFrom(taskDto => taskDto.TTL));
        }
    }
}
