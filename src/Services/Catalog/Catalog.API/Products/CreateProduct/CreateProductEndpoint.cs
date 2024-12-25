namespace Catalog.API.Products.CreateProduct
{
    public record CreateProductRequestType(string Name, string Description, List<string> Category, decimal Price, string ImageFile);
    public record CreateProductResponseType(Guid Id);
    public class CreateProductEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/products", async (CreateProductRequestType request, ISender sender) =>
            {
                var command = request.Adapt<CreateProductCommand>();
                var result = await sender.Send(command);

                var response = result.Adapt<CreateProductResponseType>();

                return Results.Created($"/products/{response.Id}", response);
            })
                .WithName("CreateProducts")
                .Produces<CreateProductResponseType>(StatusCodes.Status201Created)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Product Created")
                .WithDescription("Product Created");
        }
    }
}
