using Finance.Framework;

namespace Finance.Domain.FixInc.ChkAcc
{
    public record CheckingAccountName
    {
        private const int MinLength = 3;
        private const int MaxLength = 20;
        private const string MandatoryExceptionMessage = "Checking account's name is mandatory.";
        private const string SizeExceptionMessage = "Checking account's name should have between {0} and {1} characters.";

        public string Value { get; }

        internal CheckingAccountName(string value)
        {
            Value = value;
        }

        public static CheckingAccountName Create(string value)
        {
            Validate(value);

            return new CheckingAccountName(value);
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

        public static implicit operator string(CheckingAccountName value) =>
            value?.Value ?? string.Empty;

        public static implicit operator CheckingAccountName(string value) =>
            Create(value);

        public override string ToString() =>
            Value;
    }
}