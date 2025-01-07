namespace Ordering.Domain.ValueObjects
{
    public record OrderItemId
    {
        private OrderItemId(Guid value) => Value = value;
        public Guid Value { get; set; }

        public static OrderItemId Of(Guid value)
        {
            if (value == Guid.Empty)
            {
                throw new DomainException("Order Item ID cannot be null");
            }

            return new OrderItemId(value);
        }
    }
}
