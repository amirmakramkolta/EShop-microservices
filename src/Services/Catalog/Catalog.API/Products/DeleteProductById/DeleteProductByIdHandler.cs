
namespace Catalog.API.Products.DeleteProductById
{
    public record DeleteProductByIdCommand(Guid Id) : ICommand<DeleteProductByIdResult>;
    public record DeleteProductByIdResult(bool IsSuccess);

    public class DeleteProductByIdCommandVaildator : AbstractValidator<DeleteProductByIdCommand>
    {
        public DeleteProductByIdCommandVaildator()
        {
            RuleFor(x=>x.Id).NotEmpty().WithMessage("ID is required");
        }
    }

    internal class DeleteProductByIdCommandHandler(IDocumentSession session, ILogger<DeleteProductByIdCommandHandler> logger) : ICommandHandler<DeleteProductByIdCommand, DeleteProductByIdResult>
    {
        public async Task<DeleteProductByIdResult> Handle(DeleteProductByIdCommand command, CancellationToken cancellationToken)
        {
            session.Delete<Product>(command.Id);
            await session.SaveChangesAsync(cancellationToken);

            return new DeleteProductByIdResult(true);
        }
    }
}
