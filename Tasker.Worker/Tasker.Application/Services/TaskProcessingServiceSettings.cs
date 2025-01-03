using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasker.Application.Services
{
	public class TaskProcessingServiceSettings
	{
		public class AuthCredentials 
		{
			public string Username { get; set; }
			public string Password { get; set; }
			public string ClientId { get; set; }
		}
		public int TaskLimit { get; set; }	
		public string WorkerName { get; set; }	
		public int IterationDelayInMs { get; set; }
		public AuthCredentials AuthData { get; set; } = new AuthCredentials();

	}
}
