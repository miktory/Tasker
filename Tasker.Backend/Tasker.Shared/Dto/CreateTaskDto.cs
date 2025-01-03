using System.Text.Json;


namespace Tasker.Shared.Dto
{
	public class CreateTaskDto 
	{
		public string Type { get; set; }
		public JsonDocument? Parameters { get; set; }
		public int TTL { get; set; }

	}
}