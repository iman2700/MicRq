using MicRq.Domain.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MicRq.Domain.Event;
using System.Threading.Tasks;

namespace MicRq.Domain.Bus
{
    public interface IEventBus
    {
        Task SecndCommand<T>(T command) where T : Command;
        void Publish<T>(T @event) where T : Events;
        void Subscribe<T, TH>() where T : Events where TH : IEventHandler<T>;
    }
}
