﻿namespace MessageBus
{
    public interface IMessageProducer
    {
        void SendMessage<T>(T message);
    }
}
