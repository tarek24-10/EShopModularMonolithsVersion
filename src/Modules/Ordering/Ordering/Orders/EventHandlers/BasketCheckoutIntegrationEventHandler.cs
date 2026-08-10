using MassTransit;
using Microsoft.Extensions.Logging;
using Ordering.Orders.Features.CreateOrder;
using Shared.Messaging.Events;

namespace Ordering.Orders.EventHandlers
{
    public class BasketCheckoutIntegrationEventHandler 
        (ISender sender, ILogger<BasketCheckoutIntegrationEventHandler> logger)
        : IConsumer<BasketCheckoutIntegrationEvent>
    {
        public async Task Consume(ConsumeContext<BasketCheckoutIntegrationEvent> context)
        {
            logger.LogInformation("Integration event handled: {IntegrationEvent}", context.Message.GetType().Name);
            var CreateOrderCommand = MapToCreateOrderCommand(context.Message);
            await sender.Send(CreateOrderCommand);
        }

        public CreateOrderCommand MapToCreateOrderCommand(BasketCheckoutIntegrationEvent message)
        {
            var addressDto = new AddressDTO(
                message.FirstName,
                message.LastName,
                message.EmailAddress,
                message.AddressLine,
                message.Country,
                message.State,
                message.ZipCode
            );

            var paymentDto = new PaymentDTO(
                message.CardName,
                message.CardNumber,
                message.Expiration,
                message.Cvv,
                message.PaymentMethod
            );

            var orderId = Guid.NewGuid(); // Generate a new order ID

            var orderDto = new OrderDTO(
                Id:orderId,
                CustomerId: message.CustomerId,
                OrderName: message.UserName,
                ShippingAddress: addressDto,
                BillingAddress: addressDto, // Assuming billing address is the same as shipping address
                Payment: paymentDto,
                Items: new List<OrderItemDTO>()
                {
                    new OrderItemDTO
                    (
                        orderId,
                        Guid.NewGuid(),
                        2,
                        500
                    ),
                    new OrderItemDTO
                    (
                        orderId,
                        Guid.NewGuid(),
                        1,
                        400
                    )
                }
            );

            return new CreateOrderCommand(orderDto);
        }
    }
}
