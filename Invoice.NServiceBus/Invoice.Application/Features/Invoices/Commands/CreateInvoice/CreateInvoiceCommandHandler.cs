using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using AutoMapper;
using System.Threading.Tasks;
using System.Threading;
using Invoice.Application.Contracts;
using Invoice.Domain;
using NServiceBus;
using Invoice.Domain.NServiceBusMessages;
using Invoice.Application.Services;

namespace Invoice.Application.Features.Invoices.Commands
{
    public class CreateInvoiceCommandHandler:IRequestHandler<CreateInvoiceCommand,Guid>
    {
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IMessageSession _messageSession;



        public CreateInvoiceCommandHandler(IInvoiceRepository invoiceRepository, IMessageSession messageSession)
        {
            _invoiceRepository = invoiceRepository;
            _messageSession = messageSession;
        }

        public async Task<Guid> Handle(CreateInvoiceCommand request, CancellationToken cancellationToken)
        {
            
            var invoiceId=await _invoiceRepository.CreatInvoiceeAsync(request.OrderId);
            InvoiceId iId = new InvoiceId { Id = invoiceId };
            //await _messageSession.Send(iId).ConfigureAwait(false);
            return invoiceId;
        }
    }
}
