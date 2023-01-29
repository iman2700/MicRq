using MediatR;
using MicRq.Domain.Bus;
using MicRq.Domain.Commands;
using MicRq.Domain.Event;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace MicRq.Infra.Bus
{
    public sealed class RabbitMqBus : IEventBus
    {
        private readonly IMediator _mediator;
        private readonly Dictionary<string, List<Type>> _handlers;
        private readonly List<Type> _enventTypes;

        public RabbitMqBus(IMediator mediator)
        {
            mediator = _mediator;
            _enventTypes = new List<Type>();
            _handlers = new Dictionary<string, List<Type>>();
        }
        public void Publish<T>(T @event) where T : Events
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                var eventName = @event.GetType().Name;
                channel.QueueDeclare(eventName, false, false, false, null);
                var message = JsonConvert.SerializeObject(@event);
                var body = Encoding.UTF8.GetBytes(message);
                channel.BasicPublish("", eventName, null, body);

            }
        }

        public Task SecndCommand<T>(T command) where T : Command
        {
            return _mediator.Send(command);
        }

        public void Subscribe<T, TH>()
            where T : Events
            where TH : IEventHandler<T>
        {
            var eventName = typeof(T).Name;
            var handlerType = typeof(TH);
            if (!_enventTypes.Contains(typeof(T)))
            {
                _enventTypes.Add(typeof(T));
            }
            if (!_handlers.ContainsKey(eventName))
            {
                _handlers.Add(eventName, new List<Type>());
            }
            if (_handlers[eventName].Any(s => s.GetType() == handlerType))
            {
                throw new ArgumentException($"Handler type {handlerType.Name} alredy is register for {eventName}");
            }
            _handlers[eventName].Add(handlerType);
            StartBasicConsume<T>();
        }

        private void StartBasicConsume<T>() where T : Events
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            var connection = factory.CreateConnection();
            var channal = connection.CreateModel();
            var eventName = typeof(T).Name;
            channal.QueueDeclare(eventName, false, false, false, null);
            var consumer = new AsyncEventingBasicConsumer(channal);
            consumer.Received += Consumer_Resive;
            channal.BasicConsume(eventName, true, consumer);

        }

        private async Task Consumer_Resive(object sender, BasicDeliverEventArgs e)
        {
            var eventName = e.RoutingKey;
            var message = Encoding.UTF8.GetString(e.Body.Span);
            try
            {
                await ProcessorEvent(eventName, message).ConfigureAwait(false);
            }
            catch (Exception ex)
            {

               
            }
        }

        private async Task ProcessorEvent(string eventName, string message)
        {
            if (_handlers.ContainsKey(eventName)) 
            {
                var subscriptions = _handlers[eventName];
                foreach (var subscription in subscriptions)
                {
                    var handler = Activator.CreateInstance(subscription);
                    if (handler == null)
                        continue;
                    var eventType = _enventTypes.SingleOrDefault(t => t.Name == eventName);
                    var @event = JsonConvert.DeserializeObject(message, eventType);
                    var conreteType = typeof(IEventHandler<>).MakeGenericType(eventType);
                    await (Task)conreteType.GetMethod("Handle").Invoke(handler, new object[] {@event});
                }
            }
        }
    }
}