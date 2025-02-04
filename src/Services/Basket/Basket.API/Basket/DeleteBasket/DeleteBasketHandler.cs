
namespace Basket.API.Basket.DeleteBasket
{
    public record DeleteBasketCommand(string Username):ICommand<DeleteBasketResult>;
    public record DeleteBasketResult(bool IsSuccess);
    public class DeleteBasketCommandValidator : AbstractValidator<DeleteBasketCommand>
    {
        public DeleteBasketCommandValidator()
        {
            RuleFor(x=>x.Username).NotEmpty().WithMessage("Username is required");
        }
    }
    public class DeleteBasketCommandHandler(IBasketIRepository repository) : ICommandHandler<DeleteBasketCommand, DeleteBasketResult>
    {
        public async Task<DeleteBasketResult> Handle(DeleteBasketCommand command, CancellationToken cancellationToken)
        {
            await repository.DeleteShoppingCart(command.Username, cancellationToken);
            return new DeleteBasketResult(true);
        }
    }
}
