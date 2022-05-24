using NServiceBus;
using System;
using System.Collections.Generic;
using System.Text;

namespace System.Domain.NServiceBusMessages
{
    [Serializable]
    public class OrderId: IMessage
    {
        public Guid Id { get; set; }
    }
}
