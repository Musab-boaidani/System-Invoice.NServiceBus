using MediatR;
using NServiceBus;
using System;
using System.Collections.Generic;
using System.Domain.NServiceBusMessages;
using System.Text;
using System.Threading.Tasks;

namespace System.Application.Services
{
    public class InvoiceIdHandler : IHandleMessages<InvoiceId>
    {
       
        public Task Handle(InvoiceId message, IMessageHandlerContext context)
        {
            Console.WriteLine($"[System] Message Rercieved: {message.Id}");
            return Task.CompletedTask;
        }
    }
}
