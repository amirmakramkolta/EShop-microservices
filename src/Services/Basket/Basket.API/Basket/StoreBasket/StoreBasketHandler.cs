using Discount.Grpc;

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
    public class StoreBasketCommandHandler(IBasketIRepository repository, DiscountProtoService.DiscountProtoServiceClient discountProto) 
        : ICommandHandler<StoreBasketCommand, StoreBasketResult>
    {
        public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
        {
            await DeductDiscount(command.Cart, cancellationToken);
            await repository.StoreShoppingCart(command.Cart, cancellationToken);
            return new StoreBasketResult(command.Cart.UserName);
        }

        private async Task DeductDiscount(ShoppingCart Cart, CancellationToken cancellationToken = default)
        {
            foreach (var item in Cart.Items)
            {
                var grpcRequest = new GetDiscountRequest() { ProductName = item.ProductName };
                var coupon = await discountProto.GetDiscountAsync(grpcRequest, cancellationToken: cancellationToken);
                item.Price -= coupon.Amount;
            }
        }
    }
}
