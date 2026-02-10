using System;
using System.Collections.Generic;

namespace BankingFees
{
    public interface IFeeRule
    {
        decimal Apply(CustomerUsage usage, Plan plan, decimal runningTotal);
    }

    public class FeeCalculator
    {
        private readonly IReadOnlyList<IFeeRule> _rules;
        public FeeCalculator(IEnumerable<IFeeRule> rules) => _rules = new List<IFeeRule>(rules);

        public decimal CalculateTotal(CustomerUsage usage, Plan plan)
        {
            decimal total = 0m;
            foreach (var rule in _rules)
            {
                total = rule.Apply(usage, plan, total);
            }
            return Decimal.Round(total, 2, MidpointRounding.AwayFromZero);
        }
    }

    // Monthly fee: waived only for Gold when HasSalary
    public class MonthlyFeeRule : IFeeRule
    {
        public decimal Apply(CustomerUsage usage, Plan plan, decimal runningTotal)
        {
            if (usage.HasSalary && plan.Name.Equals("Gold", StringComparison.OrdinalIgnoreCase))
                return runningTotal; // waived
            return runningTotal + plan.MonthlyFee;
        }
    }

    public class InternalTransferRule : IFeeRule
    {
        public decimal Apply(CustomerUsage usage, Plan plan, decimal runningTotal)
        {
            var extra = Math.Max(0, usage.InternalTransfers - plan.InternalFreeCount);
            return runningTotal + (extra * plan.InternalExtraFee);
        }
    }

    public class InterbankRule : IFeeRule
    {
        public decimal Apply(CustomerUsage usage, Plan plan, decimal runningTotal)
            => runningTotal + usage.InterbankTransfers * plan.InterbankFee;
    }

    public class ImmediateTransferRule : IFeeRule
    {
        private const decimal FeePer = 2.50m;
        public decimal Apply(CustomerUsage usage, Plan plan, decimal runningTotal)
            => runningTotal + usage.ImmediateTransfers * FeePer;
    }

    public class AtmRule : IFeeRule
    {
        public decimal Apply(CustomerUsage usage, Plan plan, decimal runningTotal)
        {
            // Same bank free
            var otherFee = usage.AtmOtherBank * plan.AtmOtherBankFee;
            var abroadFee = usage.AtmAbroad * plan.AtmAbroadFee;
            return runningTotal + otherFee + abroadFee;
        }
    }

    public class CardPaymentsRule : IFeeRule
    {
        public decimal Apply(CustomerUsage usage, Plan plan, decimal runningTotal)
        {
            if (usage.CardPayments < 5 && plan.CardPaymentPenaltyApplies && !plan.Name.Equals("Premium", StringComparison.OrdinalIgnoreCase))
            {
                return runningTotal + 2.00m;
            }
            return runningTotal;
        }
    }

    public class OverdraftRule : IFeeRule
    {
        public decimal Apply(CustomerUsage usage, Plan plan, decimal runningTotal)
        {
            if (usage.OverdraftUsed > 0m)
            {
                return runningTotal + Decimal.Round(usage.OverdraftUsed * 0.05m, 2);
            }
            return runningTotal;
        }
    }

    public class OtherFeesRule : IFeeRule
    {
        public decimal Apply(CustomerUsage usage, Plan plan, decimal runningTotal)
            => runningTotal + usage.OtherFees;
    }

    public class DiscountsRule : IFeeRule
    {
        public decimal Apply(CustomerUsage usage, Plan plan, decimal runningTotal)
            => runningTotal - usage.Discounts;
    }

    public class StudentDiscountRule : IFeeRule
    {
        // Student discount: reduce final sum by 20%
        private const decimal StudentDiscountPercentage = 0.20m;

        public decimal Apply(CustomerUsage usage, Plan plan, decimal runningTotal)
        {
            if (usage.IsStudent)
            {
                // Final sum is reduced by 20% (keep 80%)
                var discountedAmount = runningTotal * (1m - StudentDiscountPercentage);
                return Decimal.Round(discountedAmount, 2, MidpointRounding.AwayFromZero);
            }
            return runningTotal;
        }
    }
}
