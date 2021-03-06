﻿using System;
using System.Collections.Generic;
using Microsoft.Azure.WebJobs.Host.Protocols;

namespace WebJobs.Extensions.RabbitMQ
{
    [AttributeUsage(AttributeTargets.Parameter)]
    public sealed class RabbitQueueTriggerAttribute : Attribute
    {
        public RabbitQueueTriggerAttribute(string queueName)
        {
            QueueName = queueName;
        }

        public string QueueName { get; set; }
    }

    [AttributeUsage(AttributeTargets.Parameter)]
    public class RabbitQueueBinderAttribute : Attribute
    {
        public RabbitQueueBinderAttribute(string exchange, string routingKey, string errorExchange = "", bool autoDelete = false, bool durable = true,
            bool execlusive = false)
        {
            Exchange = exchange;
            RoutingKey = routingKey;
            ErrorExchange = errorExchange;
            AutoDelete = autoDelete;
            Durable = durable;
            Execlusive = execlusive;
        }

        public bool AutoDelete { get; set; }

        public bool Durable { get; set; } = true;

        public bool Execlusive { get; set; }

        public string Exchange { get; set; }

        public string RoutingKey { get; set; }

        public string ErrorExchange { get; set; }
    }

    internal class RabbitQueueTriggerParameterDescriptor : TriggerParameterDescriptor
    {
        public override string GetTriggerReason(IDictionary<string, string> arguments)
        {
            return $"RabbitQueue trigger fired at {DateTime.UtcNow:o}";
        }
    }

    public class RabbitQueueTriggerValue
    {
        public string MessageId { get; set; }

        public string ApplicationId { get; set; }

        public string ContentType { get; set; }

        public string CorrelationId { get; set; }

        public IDictionary<string, object> Headers { get; set; }

        public byte[] MessageBytes { get; set; }
    }
}