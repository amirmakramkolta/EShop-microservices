using BuildingBlocks.Messaging.Events;
using MassTransit;

namespace Basket.API.Basket.CheckoutBasket
{
    public record CheckoutBasketCommand(CheckoutBasketDto dto) : ICommand<CheckoutBasketResult>;
    public record CheckoutBasketResult(bool IsSuccess);
    public class CheckoutBasketCommandValidator : AbstractValidator<CheckoutBasketCommand>
    {
        public CheckoutBasketCommandValidator()
        {
            RuleFor(x=>x.dto).NotNull().NotEmpty().WithMessage("BasketCheckoutDto is required");
            RuleFor(x=>x.dto.UserName).NotNull().NotEmpty().WithMessage("Username  is required");
        }
    }
    public class CheckoutBasketCommandHandler(IBasketIRepository repository, IPublishEndpoint publishEndpoint)
        : ICommandHandler<CheckoutBasketCommand, CheckoutBasketResult>
    {
        public async Task<CheckoutBasketResult> Handle(CheckoutBasketCommand request, CancellationToken cancellationToken)
        {
            var basket = await repository.GetShoppingCart(request.dto.UserName, cancellationToken);
            if(basket == null)
            {
                return new CheckoutBasketResult(false);
            }
            var eventMessage = request.dto.Adapt<BasketCheckoutEvent>();
            eventMessage.TotalPrice = basket.TotalPrice;
            await publishEndpoint.Publish(eventMessage, cancellationToken);

            await repository.DeleteShoppingCart(request.dto.UserName, cancellationToken);

            return new CheckoutBasketResult(true);
        }
    }
}
