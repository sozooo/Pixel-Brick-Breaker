using UniRx;

namespace Project.Scripts.WorkObjects.MessageBrokers
{
    public static class MessageBrokerHolder
    {
        public static IMessageBroker Game { get; } = new MessageBroker();
    }
}