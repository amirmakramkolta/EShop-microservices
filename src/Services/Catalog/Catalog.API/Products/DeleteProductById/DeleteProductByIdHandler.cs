
namespace Catalog.API.Products.DeleteProductById
{
    public record DeleteProductByIdCommand(Guid Id) : ICommand<DeleteProductByIdResult>;
    public record DeleteProductByIdResult(bool IsSuccess);

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
