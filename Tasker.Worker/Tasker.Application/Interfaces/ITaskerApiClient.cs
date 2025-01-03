using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasker.Application.Interfaces
{
	public interface ITaskerApiClient
	{
		Task<string> RequestTaskProcessing(Guid taskId, string workerName, CancellationToken cancellationToken);
		public void SetAccessToken(string token);
	}
}
