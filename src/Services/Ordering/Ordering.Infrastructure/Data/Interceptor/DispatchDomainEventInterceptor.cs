﻿using MediatR;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Ordering.Infrastructure.Data.Interceptor
{
    public class DispatchDomainEventInterceptor(IMediator mediator) : SaveChangesInterceptor
    {
        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            DispatchDomainEvent(eventData.Context).GetAwaiter().GetResult();
            return base.SavingChanges(eventData, result);
        }

        public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            await DispatchDomainEvent(eventData.Context);
            return await base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        public async Task DispatchDomainEvent(DbContext? context)
        {
            if (context == null) return;

            var aggregates = context.ChangeTracker
                .Entries<IAggregate>()
                .Where(a => a.Entity.Events.Any())
                .Select(a => a.Entity);

            var DomainEvents = aggregates.SelectMany(a => a.Events).ToList();

            aggregates.ToList().ForEach(a => a.ClearDomainEvent());

            foreach(var DomainEvent in DomainEvents)
            {
                await mediator.Publish(DomainEvent);
            }
        }

    }
}
