namespace Finance.Domain.ValueObjects
{
    public record Money
    {
        public decimal Value { get; }

        internal Money(decimal value)
        {
            Value = value;
        }

        public static Money Create(decimal value) =>
            new(value);

        public static implicit operator Money(decimal value) => Create(value);
        public static implicit operator decimal(Money money) => money?.Value ?? default;
        public static implicit operator decimal?(Money money) => money?.Value;

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}