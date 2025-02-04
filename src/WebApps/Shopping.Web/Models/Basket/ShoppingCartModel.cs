namespace Shopping.Web.Models.Basket
{
    public class ShoppingCartModel
    {
        public string UserName { get; set; }
        public List<ShoppingCartItemModel> Items { get; set; } = new();
        public decimal TotalPrice => Items.Sum(x => x.Price * x.Quanitity);
    }

    public class ShoppingCartItemModel
    {
        public int Quanitity { get; set; }
        public string Color { get; set; }
        public decimal Price { get; set; }
        public string ProductName { get; set; }
        public Guid ProductId { get; set; }
    }

    public record GetBasketResponse(ShoppingCartModel Cart);
    public record StoreBasketRequest(ShoppingCartModel Cart);
    public record StoreBasketResponse(string Username);

}
