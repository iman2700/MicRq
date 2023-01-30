using MediatR;
using MicRq.Banking.Domain.Commands;
using MicRq.Banking.Domain.Events;
using MicRq.Domain.Bus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicRq.Banking.Domain.EventHandlers
{
    public class TransferCommandHandler : IRequestHandler<CreateTransferCommand, bool>
    {
        private IEventBus _bus;
        public TransferCommandHandler(IEventBus bus)
        {
            _bus = bus;
        }

        public Task<bool> Handle(CreateTransferCommand request, CancellationToken cancellationToken)
        {
            _bus.Publish(new TransferCreatedEvent(request.From,request.To,request.Amount));
            return Task.FromResult(true);
        }
    }
}
