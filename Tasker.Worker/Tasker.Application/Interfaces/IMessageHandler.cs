using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasker.Application.Interfaces
{
	public interface IMessageHandler<in TMessage>
	{
		public Task HandleAsync(TMessage message, CancellationToken cancellationToken);
	}
}
