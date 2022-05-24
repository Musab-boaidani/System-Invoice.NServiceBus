using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using MediatR;
using AutoMapper;
using System.Application.Contracts;
using System.Threading.Tasks;
using System.Threading;
using System.Domain;
using NServiceBus;
using System.Domain.NServiceBusMessages;
using System.Application.Services;
using Newtonsoft.Json;

namespace System.Application.Features.Orders.Commands.CreateOrder
{
    public class CreateOrderCommandHandler:IRequestHandler<CreateOrderCommand,Guid>
    {
        private readonly IOrderAsyncRepository _OrderRepository;
        private readonly IMapper _mapper;
        private readonly IMessageSession _messageSession;

        public CreateOrderCommandHandler(IOrderAsyncRepository OrderRepository, IMapper mapper, IMessageSession messageSession)
        {
            this._OrderRepository = OrderRepository;
            this._mapper = mapper;
            _messageSession = messageSession;
        }
        public async Task<Guid> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            Order order = _mapper.Map<Order>(request);
            
         
            order=await _OrderRepository.AddAsync(order);

            var FullOrder=await _OrderRepository.CreateOrderProducts(request, order.Id);
            OrderId OId= new OrderId { Id = FullOrder.Id };
            Console.WriteLine(OId.Id);
            var OIdString=JsonConvert.SerializeObject(OId);
            MyNServiceBus.SendMessage(
                  machineName: "localhost",
                    queueName: "Invoice",
                    userName: "admin",
                    password: "123456",
                    messageBody: "test",
                    headers: new Dictionary<string, object>
                    {
                        {
                            "NServiceBus.EnclosedMessageTypes", "Invoice.Domain.NServiceBusMessages.OrderId"
                        }
                    }
                );
            //await _messageSession.Send(OId).ConfigureAwait(false);

            return order.Id;
        }
    }
}
