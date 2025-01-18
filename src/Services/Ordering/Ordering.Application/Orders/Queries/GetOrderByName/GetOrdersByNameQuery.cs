namespace Ordering.Application.Orders.Queries.GetOrderByName
{
    public record GetOrdersByNameQuery(string Name) : IQuery<GetOrdersByNameResult>;
    public record GetOrdersByNameResult(IEnumerable<OrderDto> Orders);

    public class GetOrderByNameQueryValidator : AbstractValidator<GetOrdersByNameQuery>
    {
        public GetOrderByNameQueryValidator()
        {
            RuleFor(x=>x.Name).NotEmpty().WithMessage("OrderName is required");
        }
    }
}
