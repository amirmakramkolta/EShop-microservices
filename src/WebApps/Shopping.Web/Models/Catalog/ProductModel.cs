﻿namespace Shopping.Web.Models.Catalog
{
    public class ProductModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<string> Category { get; set; }
        public string ImageFile { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
    }

    public record GetProductsResponse(IEnumerable<ProductModel> Products);
    public record GetProductsByCategoryResponse(IEnumerable<ProductModel> Products);
    public record GetProductsByIdResponse(ProductModel Product);

}
