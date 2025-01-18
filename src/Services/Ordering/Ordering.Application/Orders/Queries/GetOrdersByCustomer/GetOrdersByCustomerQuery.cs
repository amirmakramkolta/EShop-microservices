namespace Ordering.Application.Orders.Queries.GetOrdersByCustomer
{
    public record GetOrdersByCustomerQuery(Guid CustomwrId) : IQuery<GetOrdersByCustomerResult>;
    public record GetOrdersByCustomerResult(IEnumerable<OrderDto> Orders);

    public class GetOrdersByCustomerQueryValidator : AbstractValidator<GetOrdersByCustomerQuery>
    {
        public GetOrdersByCustomerQueryValidator()
        {
            RuleFor(x => x.CustomwrId).NotEmpty().WithMessage("CustomerId is required");
        }
    }
}
