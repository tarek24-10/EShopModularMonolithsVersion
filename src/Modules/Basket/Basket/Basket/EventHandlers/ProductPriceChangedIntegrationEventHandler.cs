using MassTransit;
using Microsoft.Extensions.Logging;
using Shared.Messaging.Events;

namespace Basket.Basket.EventHandlers
{
    public class ProductPriceChangedIntegrationEventHandler(ILogger logger, ISender sender)
        : IConsumer<ProductPriceChangedIntegrationEvent>
    {
        public Task Consume(ConsumeContext<ProductPriceChangedIntegrationEvent> context)
        {
            logger.LogInformation("Integration Event handled: {IntegrationEvent}", context.Message.GetType().Name);
            return Task.CompletedTask;
        }
    }
}
