﻿namespace Basket.API.Models
{
    public class ShoppingCartItem
    {
        public int Quanitity { get; set; }
        public string Color { get; set; }
        public decimal Price { get; set; }
        public string ProductName { get; set; }
        public Guid ProductId { get; set; }
    }
}
