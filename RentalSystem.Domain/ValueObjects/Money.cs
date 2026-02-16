namespace RentalSystem.Domain.ValueObjects
{
    public sealed class Money
    {
        public decimal Value { get; private set; }

        private Money() { } // EF Core

        public Money(decimal value)
        {
            if (value < 0) throw new ArgumentException("Money cannot be negative", nameof(value));
            Value = value;
        }

        public static Money operator +(Money a, Money b) => new Money(a.Value + b.Value);
        public static Money operator -(Money a, Money b)
        {
            var result = a.Value - b.Value;
            if (result < 0) throw new InvalidOperationException("Resulting Money cannot be negative");
            return new Money(result);
        }

        public override string ToString() => Value.ToString("F2");
    }

}
