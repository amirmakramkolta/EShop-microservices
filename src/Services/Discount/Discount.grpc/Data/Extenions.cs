using Microsoft.EntityFrameworkCore;

namespace Discount.grpc.Data
{
    public static class Extenions
    {
        public static async Task<IApplicationBuilder> UseAutoMigrationAsync(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            using var dbcontext = scope.ServiceProvider.GetRequiredService<DiscountContext>();
            await dbcontext.Database.MigrateAsync();
            return app;
        }
    }
}
