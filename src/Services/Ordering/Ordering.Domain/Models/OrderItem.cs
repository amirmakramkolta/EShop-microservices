﻿using Ordering.Domain.Abstructions;

namespace Ordering.Domain.Models
{
    public class OrderItem : Entity<OrderItemId>
    {
        internal OrderItem(
            OrderId orderId,
            ProductId productId,
            int quantity,
            decimal price
            )
        {
            OrderId = orderId;
            ProductId = productId;
            Quantity = quantity;
            Price = price;
        }
        public OrderId OrderId { get; set; }
        public ProductId ProductId { get; set; }
        public decimal Price { get; private set; } = default!;
        public int Quantity { get; private set; } = default!;
    }
}
