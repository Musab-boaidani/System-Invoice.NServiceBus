using System;
using System.Collections.Generic;
using System.Text;
using NServiceBus;

namespace System.Domain.NServiceBusMessages
{
    [Serializable]
    public class InvoiceId: IMessage
    {
        public Guid Id { get; set; }
    }
}
