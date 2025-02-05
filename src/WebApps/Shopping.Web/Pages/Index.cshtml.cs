namespace Shopping.Web.Pages
{
    public class IndexModel(ICatalogService catalogService, IBasketService basketService, ILogger<IndexModel> logger) : PageModel
    {
        public IEnumerable<ProductModel> ProductList = new List<ProductModel>();

        public async Task<IActionResult> OnGetAsync()
        {
            logger.LogInformation("Index page visited");
            var result = await catalogService.GetProducts();

            ProductList = result.Products;

            return Page();
        }

        public async Task<IActionResult> OnPostAddToCartAsync(Guid productId)
        {
            logger.LogInformation($"Add to cart button clicked");

            var productResponse = await catalogService.GetProduct(productId);

            var basket = await basketService.LoadUserBasket();
            if(basket.Items.Any(x=>x.ProductId == productId))
            {
                basket.Items.First(x => x.ProductId == productId).Quanitity++;
            }
            else
            {
                basket.Items.Add(new ShoppingCartItemModel
                {
                    ProductId = productId,
                    ProductName = productResponse.Product.Name,
                    Price = productResponse.Product.Price,
                    Quanitity = 1,
                    Color = "Black"
                });
            }

            await basketService.StoreBasket(new StoreBasketRequest(basket));
            return RedirectToPage("Cart");
        }
    }
}
