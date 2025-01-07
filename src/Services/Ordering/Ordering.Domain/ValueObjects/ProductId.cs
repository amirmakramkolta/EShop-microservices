namespace Ordering.Domain.ValueObjects
{
    public record ProductId
    {
        private ProductId(Guid value) => Value = value;
        public Guid Value { get; set; }

        public static ProductId Of(Guid value)
        {
            if (value == Guid.Empty)
            {
                throw new DomainException("Product ID cannot be null");
            }

            return new ProductId(value);
        }
    }
}
