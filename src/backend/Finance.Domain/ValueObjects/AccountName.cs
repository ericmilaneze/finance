using Finance.Framework;

namespace Finance.Domain.ValueObjects
{
    public record AccountName
    {
        private const int MinLength = 3;
        private const int MaxLength = 20;
        private const string MandatoryExceptionMessage = "Account's name is mandatory.";
        private const string SizeExceptionMessage = "Account's name should have between {0} and {1} characters.";

        public string Value { get; }

        internal AccountName(string value)
        {
            Value = value;
        }

        public static AccountName Create(string value)
        {
            Validate(value);

            return new AccountName(value);
        }

        private static void Validate(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new DomainException(MandatoryExceptionMessage);

            if (value.Length < MinLength || value.Length > MaxLength)
            {
                throw new DomainException(
                    string.Format(SizeExceptionMessage, MinLength, MaxLength));
            }
        }

        public static implicit operator string(AccountName value) =>
            value?.Value ?? string.Empty;

        public static implicit operator AccountName(string value) =>
            Create(value);

        public override string ToString() =>
            Value;
    }
}