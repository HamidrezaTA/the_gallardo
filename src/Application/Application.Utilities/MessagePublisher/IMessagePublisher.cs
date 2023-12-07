using System.Threading.Tasks;

namespace Application.Utilities.MessagePublisher
{
    public interface IMessagePublisher
    {
        Task PublishAsync(object envelop, string? routingKey = null, string? exchangeName = null, int delayTimeInMilisecond = 0);
    }
}