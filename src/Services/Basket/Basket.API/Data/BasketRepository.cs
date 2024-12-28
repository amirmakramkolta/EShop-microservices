namespace Basket.API.Data
{
    public class BasketRepository(IDocumentSession session) : IBasketIRepository
    {
        public async Task<ShoppingCart> GetShoppingCart(string UserName, CancellationToken cancellationToken = default)
        {
            var data = await session.LoadAsync<ShoppingCart>(UserName, cancellationToken);
            return data ?? throw new BasketNotFoundException(UserName);
        }
        public async Task StoreShoppingCart(ShoppingCart Cart, CancellationToken cancellationToken = default)
        {
            session.Store<ShoppingCart>(Cart);
            await session.SaveChangesAsync(cancellationToken);
        }

        public async Task<bool> DeleteShoppingCart(string UserName, CancellationToken cancellationToken = default)
        {
            session.Delete<ShoppingCart>(UserName);
            await session.SaveChangesAsync(cancellationToken);
            return true;
        }

    }
}
