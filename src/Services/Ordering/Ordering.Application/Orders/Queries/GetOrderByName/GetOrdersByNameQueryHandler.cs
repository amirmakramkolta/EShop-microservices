namespace Ordering.Application.Orders.Queries.GetOrderByName
{
    public class GetOrdersByNameQueryHandler(IApplicationDbContext dbContext) : IQueryHandler<GetOrdersByNameQuery, GetOrdersByNameResult>
    {
        public async Task<GetOrdersByNameResult> Handle(GetOrdersByNameQuery request, CancellationToken cancellationToken)
        {
            var orders = await dbContext.Orders
                .Include(x=>x.OrderItems)
                .AsNoTracking()
                .Where(x=>x.OrderName.Value.Contains(request.Name))
                .OrderBy(x=>x.OrderName.Value)
                .ToListAsync(cancellationToken);

            return new GetOrdersByNameResult(orders.ToOrderDtoList());
        }
    }
}
