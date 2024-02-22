﻿namespace Microservices.Services.AuthAPI.RabbitMQSender
{
    public interface IRabbitMQAuthMessageSender
    {
        void SendMessage(Object message, string queueName);
    }
}
