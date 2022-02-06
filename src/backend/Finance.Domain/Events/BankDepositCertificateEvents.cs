using Finance.Common.FixInc;
using Finance.Framework;

namespace Finance.Domain.Events
{
    public static class BankDepositCertificateEvents
    {
        public static class V1
        {
            public record BankDepositCertificateCreated(
                Guid Id,
                string Name,
                string? Description,
                string? FinancialInstituteCode,
                FinancialInstituteType FinancialInstituteType,
                DateTime InitialDepositDate,
                decimal InitialDepositValue,
                decimal CurrentGrossValue,
                InterestType InterestType,
                RateIndex RateIndex,
                decimal InterestRate,
                DateTime DueDate,
                DateTime? GracePeriodFinalDate,
                LiquidityType LiquidityType,
                DateTime CreationDate) : IEvent;

            public record ValuesCalculated(
                Guid Id,
                int DaysPassed,
                decimal GrossIncome,
                decimal TaxOnFinancialOperationsRate,
                decimal TaxOnFinancialOperationsValue,
                decimal IncomeTaxRate,
                decimal IncomeTaxValue,
                decimal NetValue,
                DateTime LastCalculationDate) : IEvent;
        }
    }
}