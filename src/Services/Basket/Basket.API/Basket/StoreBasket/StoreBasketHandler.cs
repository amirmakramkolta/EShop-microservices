namespace Basket.API.Basket.StoreBasket
{
    public record StoreBasketCommand(ShoppingCart Cart) : ICommand<StoreBasketResult>;
    public record StoreBasketResult(string Username); 

    public class StoreBasketCommandValidator : AbstractValidator<StoreBasketCommand>
    {
        public StoreBasketCommandValidator()
        {
            RuleFor(x => x.Cart).NotEmpty().WithMessage("Cart cannot be null");
            RuleFor(x => x.Cart.UserName).NotEmpty().WithMessage("Username is required");
            RuleFor(x => x.Cart.Items).NotEmpty().WithMessage("Items are required");
        }
    }
    public class StoreBasketCommandHandler(IBasketIRepository repository) : ICommandHandler<StoreBasketCommand, StoreBasketResult>
    {
        public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
        {
            await repository.StoreShoppingCart(command.Cart, cancellationToken);
            return new StoreBasketResult(command.Cart.UserName);
        }
    }
}
