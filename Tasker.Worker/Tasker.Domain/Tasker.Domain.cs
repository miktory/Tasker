using System.Diagnostics;
using System.Text.Json;

namespace Tasker.Domain
{
    public class ParametrizedTask
    {
        public Guid UserId { get; set; }
        public Guid Id { get; set; }
        public string Type { get; set; }
        public JsonDocument? Parameters { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int TTL { get; set; }
        public string Status { get; set; }
		public string? WorkerName { get; set; }
	}

	public class ParametrizedTaskResult
	{
		public int Id { get; set; }
		public Guid ParametrizedTaskId { get; set; }
		public string? Result { get; set; }
	}

    public enum Operation
    {
        None,
        GENERATE_LOGIN,
        GENERATE_NUMBER
    }
}
