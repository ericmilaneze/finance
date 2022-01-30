using Finance.Framework;

namespace Finance.Domain.FixInc.ChkAcc
{
    public class CheckingAccountName
    {
        private const string MandatoryExceptionMessage = "Checking account's name is mandatory.";
        private const string SizeExceptionMessage = "Checking account's name should have between 3 and 20 characters.";

        private readonly string _value;

        internal CheckingAccountName(string value)
        {
            _value = value;
        }

        public static CheckingAccountName Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new DomainException(MandatoryExceptionMessage);

            if (value.Length < 3 || value.Length > 20)
                throw new DomainException(SizeExceptionMessage);

            return new CheckingAccountName(value);
        }

        public static implicit operator string(CheckingAccountName value) =>
            value?._value ?? string.Empty;

        public static implicit operator CheckingAccountName(string value) =>
            Create(value);

        public override string ToString()
        {
            return _value;
        }
    }
}