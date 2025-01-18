namespace Ordering.Application.Orders.Queries.GetOrders
{
    public class GetOrdersQueryHandler(IApplicationDbContext dbContext) : IQueryHandler<GetOrdersQuery, GetOrdersResult>
    {
        public async Task<GetOrdersResult> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
        {
            var pageIndex = request.PaginationRequest.PageIndex;
            var pageSize = request.PaginationRequest.PageSize;

            var totalCount = await dbContext.Orders.LongCountAsync(cancellationToken);

            var Orders = await dbContext.Orders
                .Include(x => x.OrderItems)
                .AsNoTracking()
                .OrderBy(x=>x.OrderName.Value)
                .Skip(pageSize * (pageIndex - 1))
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return new GetOrdersResult(new(pageIndex, pageSize, totalCount, Orders.ToOrderDtoList()));
        }
    }
}
