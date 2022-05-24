using NServiceBus;
using System;
using System.Collections.Generic;
using System.Text;

namespace Invoice.Domain.NServiceBusMessages
{
    public  class OrderId:IMessage
    {
        public Guid Id { get; set; }
    }
}
