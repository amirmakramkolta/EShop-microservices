﻿namespace Ordering.Application.Orders.Queries.GetOrdersByCustomer
{
    public class GetOrderByCustomerQueryHandler(IApplicationDbContext dbContext) : IQueryHandler<GetOrdersByCustomerQuery, GetOrdersByCustomerResult>
    {
        public async Task<GetOrdersByCustomerResult> Handle(GetOrdersByCustomerQuery request, CancellationToken cancellationToken)
        {
            var orders = await dbContext.Orders
                .Include(x => x.OrderItems)
                .AsNoTracking()
                .Where(x => x.CustomerId == CustomerId.Of(request.CustomerId))
                .OrderBy(x => x.OrderName.Value)
                .ToListAsync(cancellationToken);

            return new GetOrdersByCustomerResult(orders.ToOrderDtoList());
        }
    }
}
