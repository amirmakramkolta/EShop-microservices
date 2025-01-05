namespace Ordering.Domain.ValueObjects
{
    public record Payment
    {
        public string CardName { get; } = default!;
        public string CardNaumber { get; } = default!;
        public string Experation { get; } = default!;
        public string CVV { get; } = default!;
        public string PaymentMethod { get; } = default!;
    }
}
