using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ordering.Infrastructure.Data.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(x=>x.Id);
            builder.Property(x => x.Id).HasConversion(
                orderId => orderId.Value,
                dbId => OrderId.Of(dbId)
                );


        }
    }
}
