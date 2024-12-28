using Marten.Schema;
using System.Linq;

namespace Catalog.API.Data
{
    public class CatalogInitialData : IInitialData
    {
        public async Task Populate(IDocumentStore store, CancellationToken cancellation)
        {
            using var session = await store.LightweightSerializableSessionAsync();
            if(await session.Query<Product>().AnyAsync())
                return;

            session.Store<Product>(GetPreproductConfig());
            await session.SaveChangesAsync(cancellation);
        }

        private static List<Product> GetPreproductConfig() => new List<Product>()
        {
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Product Name 1",
                Description = "Product Description 1",
                ImageFile = "Product Image 1",
                Price = 1,
                Category = ["c1","c2"]
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Product Name 2",
                Description = "Product Description 2",
                ImageFile = "Product Image 2",
                Price = 2,
                Category = ["c2","c3"]
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Product Name 3",
                Description = "Product Description 3",
                ImageFile = "Product Image 3",
                Price = 3,
                Category = ["c3","c4"]
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Product Name 4",
                Description = "Product Description 4",
                ImageFile = "Product Image 4",
                Price = 4,
                Category = ["c4","c5"]
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Product Name 5",
                Description = "Product Description 5",
                ImageFile = "Product Image 5",
                Price = 5,
                Category = ["c5","c6"]
            }
        };
    }

    
}
