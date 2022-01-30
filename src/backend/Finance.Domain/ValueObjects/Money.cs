namespace Finance.Domain.ValueObjects
{
    public class Money
    {
        private readonly decimal _value;

        internal Money(decimal value)
        {
            _value = value;
        }

        public static Money Create(decimal value) =>
            new(value);

        public static implicit operator Money(decimal value) => Create(value);
        public static implicit operator decimal(Money money) => money?._value ?? default;
        public static implicit operator decimal?(Money money) => money?._value;

        public override string ToString()
        {
            return _value.ToString();
        }
    }
}