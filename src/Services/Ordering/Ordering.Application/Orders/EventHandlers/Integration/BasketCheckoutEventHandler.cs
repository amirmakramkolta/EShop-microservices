﻿using BuildingBlocks.Messaging.Events;
using MassTransit;
using Ordering.Application.Orders.Commands.CreateOrder;
using Ordering.Domain.Enums;

namespace Ordering.Application.Orders.EventHandlers.Integration
{
    public class BasketCheckoutEventHandler
        (ISender sender, ILogger<BasketCheckoutEventHandler> logger): IConsumer<BasketCheckoutEvent>
    {
        public async Task Consume(ConsumeContext<BasketCheckoutEvent> context)
        {
            logger.LogInformation("Intgration Event handled:{IntergrationEvent}", context.Message.GetType().Name);

            var command = MapToCreateOrderCommand(context.Message);
            await sender.Send(command);
        }

        public CreateOrderCommand MapToCreateOrderCommand(BasketCheckoutEvent message)
        {
            var addressDto = new AddressDto(message.FirstName,
                message.LastName,
                message.EmailAddress,
                message.AddressLine,
                message.Country,
                message.State,
                message.ZipCode);

            var payment = new PaymentDto(message.CardName,
                message.CardNumber,
                message.Expiration,
                message.Cvv,
                message.PaymentMethod);

            var orderId = Guid.NewGuid();

            var OrderDto = new OrderDto(
                orderId,
                message.CustomerId,
                message.UserName,
                addressDto,
                addressDto,
                payment,
                OrderStatus.Pending,
                [
                    new OrderItemDto(orderId,new Guid("5334c996-8457-4cf0-815c-ed2b77c4ff61"),2,180),
                    new OrderItemDto(orderId,new Guid("c67d6323-e8b1-4bdf-9a75-b0d0d2e7e914"),2,100),
                    ]
                );

            return new CreateOrderCommand(OrderDto);
        }
    }
}
