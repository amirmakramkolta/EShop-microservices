using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Basket.API.Data
{
    public class CachedBasketRepository(IBasketIRepository repository, IDistributedCache cache) : IBasketIRepository
    {
        public async Task<ShoppingCart> GetShoppingCart(string UserName, CancellationToken cancellationToken = default)
        {
            var cachedBasket = await cache.GetStringAsync(UserName, cancellationToken);
            if (!string.IsNullOrEmpty(cachedBasket))
                return JsonSerializer.Deserialize<ShoppingCart>(cachedBasket);

            var basket = await repository.GetShoppingCart(UserName, cancellationToken);
            await cache.SetStringAsync(UserName, JsonSerializer.Serialize(basket), cancellationToken);

            return basket;
        }

        public async Task StoreShoppingCart(ShoppingCart Cart, CancellationToken cancellationToken = default)
        {
            await repository.StoreShoppingCart(Cart, cancellationToken);
            await cache.SetStringAsync(Cart.UserName, JsonSerializer.Serialize(Cart), cancellationToken);
        }
        public async Task<bool> DeleteShoppingCart(string UserName, CancellationToken cancellationToken = default)
        {
            await repository.DeleteShoppingCart(UserName, cancellationToken);
            await cache.RemoveAsync(UserName, cancellationToken);

            return true;
        }
    }
}
