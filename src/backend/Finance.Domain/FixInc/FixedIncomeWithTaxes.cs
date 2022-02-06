using Finance.Domain.ValueObjects;
using Finance.Framework;

namespace Finance.Domain.FixInc
{
    public abstract class FixedIncomeWithTaxes : AggregateRoot
    {
        public DateTime InitialDepositDate { get; protected set; }
        public Money? InitialDepositValue { get; protected set; }
        public Money? CurrentGrossValue { get; protected set; }

        protected FixedIncomeWithTaxes(Guid id)
            : base(id)
        {
        }

        protected int CalculateDaysPassed()
        {
            return GetInitialDate().Subtract(InitialDepositDate.Date).Days;

            static DateTime GetInitialDate() =>
                DateTime.UtcNow.DayOfWeek switch
                {
                    DayOfWeek.Saturday => DateTime.UtcNow.AddDays(2).Date,
                    DayOfWeek.Sunday => DateTime.UtcNow.AddDays(1).Date,
                    _ => DateTime.UtcNow.Date,
                };
        }

        protected Money CalculateGrossIncome() =>
            (CurrentGrossValue ?? 0) - (InitialDepositValue ?? 0);

        protected static decimal CalculateTaxOnFinancialOperationsRate(int daysPassed)
        {
            var couldGet = GetTaxesByDay().TryGetValue(daysPassed, out decimal tax);

            if (couldGet)
                return tax;

            return 0M;

            static Dictionary<int, decimal> GetTaxesByDay() =>
                new()
                {
                    { 0, 100M },
                    { 1, 96M },
                    { 2, 93M },
                    { 3, 90M },
                    { 4, 86M },
                    { 5, 83M },
                    { 6, 80M },
                    { 7, 76M },
                    { 8, 73M },
                    { 9, 70M },
                    { 10, 66M },
                    { 11, 63M },
                    { 12, 60M },
                    { 13, 56M },
                    { 14, 53M },
                    { 15, 50M },
                    { 16, 46M },
                    { 17, 43M },
                    { 18, 40M },
                    { 19, 36M },
                    { 20, 33M },
                    { 21, 30M },
                    { 22, 26M },
                    { 23, 23M },
                    { 24, 20M },
                    { 25, 16M },
                    { 26, 13M },
                    { 27, 10M },
                    { 28, 6M },
                    { 29, 3M },
                    { 30, 0M }
                };
        }

        protected static Money CalculateTaxOnFinancialOperations(Money grossIncome, decimal taxOnFinancialOperationsRate) =>
            grossIncome * taxOnFinancialOperationsRate;

        protected decimal CalculateIncomeTaxRate()
        {
            var daysPassed = DateTime.UtcNow.Date.Subtract(InitialDepositDate.Date).Days;

            if (daysPassed <= 180)
                return 22.5M;

            if (daysPassed > 180 && daysPassed <= 360)
                return 20M;

            if (daysPassed > 360 && daysPassed <= 720)
                return 17.5M;

            return 15M;
        }

        protected static Money CalculateIncomeTax(Money grossIncome, Money taxOnFinancialOperations, decimal incomeTaxRate) =>
            (grossIncome - taxOnFinancialOperations) * incomeTaxRate;

        protected Money CalculateNetValue(Money taxOnFinancialOperations, Money incomeTax) =>
            (CurrentGrossValue ?? 0) - taxOnFinancialOperations - incomeTax;
    }
}