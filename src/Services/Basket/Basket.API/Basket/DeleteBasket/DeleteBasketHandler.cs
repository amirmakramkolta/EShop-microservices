
namespace Basket.API.Basket.DeleteBasket
{
    public record DeleteBasketCommand(string Username):ICommand<Unit>;
    public class DeleteBasketCommandValidator : AbstractValidator<DeleteBasketCommand>
    {
        public DeleteBasketCommandValidator()
        {
            RuleFor(x=>x.Username).NotEmpty().WithMessage("Username is required");
        }
    }
    public class DeleteBasketCommandHandler(IBasketIRepository repository) : ICommandHandler<DeleteBasketCommand, Unit>
    {
        public async Task<Unit> Handle(DeleteBasketCommand command, CancellationToken cancellationToken)
        {
            await repository.DeleteShoppingCart(command.Username, cancellationToken);
            return Unit.Value;
        }
    }
}
