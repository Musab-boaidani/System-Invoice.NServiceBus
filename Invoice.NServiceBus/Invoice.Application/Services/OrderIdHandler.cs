using Invoice.Application.Features.Invoices.Commands;
using Invoice.Domain.NServiceBusMessages;
using MediatR;
using NServiceBus;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Invoice.Application.Services
{
    public class OrderIdHandler : IHandleMessages<OrderId>
    {
        private readonly IMediator _mediator;

        public OrderIdHandler(IMediator mediator)
        {
            _mediator = mediator;
          
        }

        public async Task Handle(OrderId message, IMessageHandlerContext context)
        {
            
            Console.WriteLine($"[Invoice] Message Rercieved: {message.Id}");
            CreateInvoiceCommand cic = new CreateInvoiceCommand
            {
                OrderId = message.Id
            };
            Guid id = await _mediator.Send(cic);
        }
    }
}
