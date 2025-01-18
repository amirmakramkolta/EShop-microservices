using BuildingBlocks.Pagination;

namespace Ordering.Application.Orders.Queries.GetOrders
{
    public record GetOrdersQuery(PaginationRequest PaginationRequest) : IQuery<GetOrdersResult>;
    public record GetOrdersResult(PaginatedResult<OrderDto> Orders);

    public class GetOrdersQueryValidator : AbstractValidator<GetOrdersQuery>
    {
        public GetOrdersQueryValidator()
        {
            RuleFor(x => x.PaginationRequest.PageIndex).GreaterThanOrEqualTo(1).WithMessage("Invalid Page index");
            RuleFor(x => x.PaginationRequest.PageSize).GreaterThanOrEqualTo(1).WithMessage("Invalid Page size");
        }
    }
}
