using System;
using System.Collections.Generic;
using System.Text;
using NServiceBus;

namespace Invoice.Domain.NServiceBusMessages
{
    public class InvoiceId:IMessage
    {
        public Guid Id { get; set; }
    }
}
