namespace Ordering.Domain.ValueObjects
{
    public record Payment
    {
        public string CardName { get; } = default!;
        public string CardNumber { get; } = default!;
        public string Experation { get; } = default!;
        public string CVV { get; } = default!;
        public int PaymentMethod { get; } = default!;

        protected Payment() { }
        private Payment
            (
            string cardName,
            string cardNumber,
            string experation,
            string cvv,
            int paymentMethod
            )
        {
            CardName = cardName;
            CardNumber = cardNumber;
            Experation = experation;
            CVV = cvv;
            PaymentMethod = paymentMethod;
        }

        public static Payment Of
            (
            string cardName,
            string cardNumber,
            string experation,
            string cvv,
            int paymentMethod
            )
        {
            ArgumentException.ThrowIfNullOrWhiteSpace( cardNumber);
            ArgumentException.ThrowIfNullOrWhiteSpace( cardName);
            ArgumentException.ThrowIfNullOrWhiteSpace( experation);
            ArgumentException.ThrowIfNullOrWhiteSpace( cvv);
            ArgumentOutOfRangeException.ThrowIfNotEqual(cvv.Length, 3);
            //ArgumentException.ThrowIfNullOrWhiteSpace( paymentMethod);

            return new Payment(cardName, cardNumber, experation, cvv, paymentMethod);
        }
    }
}
