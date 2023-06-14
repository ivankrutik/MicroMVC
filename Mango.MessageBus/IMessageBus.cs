using Mango.MessageBus.Model;

namespace Mango.MessageBus
{
    public interface IMessageBus
    {
        void PublishMessage(BaseMessage message, string topic);
    }
}
