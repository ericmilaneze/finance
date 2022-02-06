using Finance.Common.FixInc;
using Finance.Domain.Events;
using Finance.Domain.ValueObjects;
using Finance.Framework;

namespace Finance.Domain.FixInc.Bdc
{
    public class BankDepositCertificate : FixedIncomeWithTaxes
    {
        public AccountName? Name { get; private set; }
        public string? Description { get; private set; }
        public string? FinancialInstituteCode { get; private set; }
        public FinancialInstituteType FinancialInstituteType { get; private set; }
        public Money? NetValue { get; private set; }
        public InterestType InterestType { get; private set; }
        public RateIndex RateIndex { get; private set; }
        public decimal InterestRate { get; private set; }
        public DateTime DueDate { get; private set; }
        public DateTime? GracePeriodFinalDate { get; private set; }
        public LiquidityType LiquidityType { get; private set; }
        public BankDepositCertificateState State { get; private set; }

        protected BankDepositCertificate(Guid id)
            : base(id)
        {
        }

        private BankDepositCertificate(
            Guid id,
            AccountName name,
            string? description,
            string? financialInstituteCode,
            FinancialInstituteType financialInstituteType,
            DateTime initialDepositDate,
            Money initialDepositValue,
            Money currentGrossValue,
            InterestType interestType,
            RateIndex rateIndex,
            decimal interestRate,
            DateTime dueDate,
            DateTime? gracePeriodFinalDate,
            LiquidityType liquidityType) : this(id)
        {
            RaiseEvent(
                new BankDepositCertificateEvents.V1.BankDepositCertificateCreated(
                    id,
                    name,
                    description,
                    financialInstituteCode,
                    financialInstituteType,
                    initialDepositDate,
                    initialDepositValue,
                    currentGrossValue,
                    interestType,
                    rateIndex,
                    interestRate,
                    dueDate,
                    gracePeriodFinalDate,
                    liquidityType,
                    DateTime.UtcNow));
        }

        public static BankDepositCertificate Create(Guid id,
            AccountName name,
            string? description,
            string? financialInstituteCode,
            FinancialInstituteType financialInstituteType,
            DateTime initialDepositDate,
            Money initialDepositValue,
            Money currentGrossValue,
            InterestType interestType,
            RateIndex rateIndex,
            decimal interestRate,
            DateTime dueDate,
            DateTime? gracePeriodFinalDate,
            LiquidityType liquidityType)
        {
            var bankDepositeCertificate = new BankDepositCertificate(
                id,
                name,
                description,
                financialInstituteCode,
                financialInstituteType,
                initialDepositDate,
                initialDepositValue,
                currentGrossValue,
                interestType,
                rateIndex,
                interestRate,
                dueDate,
                gracePeriodFinalDate,
                liquidityType);

            bankDepositeCertificate.CalculateValues();

            return bankDepositeCertificate;
        }

        protected void CalculateValues()
        {
            var daysPassed = CalculateDaysPassed();
            var grossIncome = CalculateGrossIncome();
            var taxOnFinancialOperationsRate = CalculateTaxOnFinancialOperationsRate(daysPassed);
            var taxOnFinancialOperationsValue = CalculateTaxOnFinancialOperations(grossIncome, taxOnFinancialOperationsRate);
            var incomeTaxRate = CalculateIncomeTaxRate();
            var incomeTaxValue = CalculateIncomeTax(grossIncome, taxOnFinancialOperationsValue, incomeTaxRate);
            var netValue = CalculateNetValue(taxOnFinancialOperationsValue, incomeTaxValue);

            RaiseEvent(
                new BankDepositCertificateEvents.V1.ValuesCalculated(
                    Id,
                    daysPassed,
                    grossIncome,
                    taxOnFinancialOperationsRate,
                    taxOnFinancialOperationsValue,
                    incomeTaxRate,
                    incomeTaxValue,
                    netValue,
                    DateTime.UtcNow));
        }

        public override void HandleEvent(IEvent @event)
        {
            throw new NotImplementedException();
        }

        protected override void ValidateState()
        {
            throw new NotImplementedException();
        }
    }
}