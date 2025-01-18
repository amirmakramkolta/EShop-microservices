namespace Ordering.Application.Orders.EventHandlers
{
    public class OrderUpdaatedEventHandler(ILogger<OrderUpdaatedEventHandler> logger) : INotificationHandler<OrderUpdateEvent>
    {
        public async Task Handle(OrderUpdateEvent notification, CancellationToken cancellationToken)
        {
            logger.LogInformation("Domain Event Handled: {DomainEvent}", notification.GetType().Name);
        }
    }
}
