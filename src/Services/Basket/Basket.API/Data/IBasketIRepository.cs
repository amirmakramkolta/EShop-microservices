namespace Basket.API.Data
{
    public interface IBasketIRepository
    {
        Task<ShoppingCart> GetShoppingCart(string UserName, CancellationToken cancellationToken = default);
        Task StoreShoppingCart(ShoppingCart Cart, CancellationToken cancellationToken = default);
        Task<bool> DeleteShoppingCart(string UserName, CancellationToken cancellationToken = default);
    }
}
