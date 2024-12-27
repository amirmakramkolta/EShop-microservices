namespace Catalog.API.Products.CreateProduct
{
    public record CreateProductCommand(string Name, string Description, List<string> Category, decimal Price, string ImageFile) : ICommand<CreateProductResult>;
    public record CreateProductResult(Guid Id);
    internal class CreateProductCommandHandler(IDocumentSession session) : ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            // Business logic
            //Create product
            var NewProduct = new Product()
            {
                Name = command.Name,
                Description = command.Description,
                Category = command.Category,
                Price = command.Price,
                ImageFile = command.ImageFile
            };

            // Save to database

            session.Store(NewProduct);
            await session.SaveChangesAsync(cancellationToken);

            //Return data
            return new CreateProductResult(NewProduct.Id);
        }
    }
}
