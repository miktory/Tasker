namespace Tasker.Application.Interfaces
{
	public interface IMessageProducer<in TMessage> : IDisposable
	{
		Task ProduceAsync(TMessage message, CancellationToken cancellationToken);
	}
}
