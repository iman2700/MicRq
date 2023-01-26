using MicRq.Domain.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicRq.Domain.Bus
{
    public interface IEventHandler<in TEvent> : IEventHandler where TEvent : Events
    {
        Task Heandel(TEvent @event);
    }

    public interface IEventHandler
    { 
    }
}
