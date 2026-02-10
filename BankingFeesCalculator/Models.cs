using System;
using System.Collections.Generic;

namespace Console
{
    public record CustomerUsage(
        string PlanName,
        bool HasSalary,
        bool IsStudent,
        int InternalTransfers,
        int InterbankTransfers,
        int ImmediateTransfers,
        int AtmSameBank,
        int AtmOtherBank,
        int AtmAbroad,
        int CardPayments,
        decimal OverdraftUsed,
        decimal OtherFees,
        decimal Discounts
    );

    public class Plan
    {
        public string Name { get; init; } = string.Empty;
        public decimal MonthlyFee { get; init; }
        public int InternalFreeCount { get; init; }
        public decimal InternalExtraFee { get; init; }
        public decimal InterbankFee { get; init; }
        public decimal AtmOtherBankFee { get; init; }
        public decimal AtmAbroadFee { get; init; }
        public bool CardPaymentPenaltyApplies { get; init; }
    }

    public static class PlanRegistry
    {
        public static Dictionary<string, Plan> DefaultPlans()
        {
            return new Dictionary<string, Plan>(StringComparer.OrdinalIgnoreCase)
            {
                ["Standard"] = new Plan
                {
                    Name = "Standard",
                    MonthlyFee = 5.99m,
                    InternalFreeCount = 3,
                    InternalExtraFee = 0.20m,
                    InterbankFee = 1.20m,
                    AtmOtherBankFee = 0.50m,
                    AtmAbroadFee = 3.00m,
                    CardPaymentPenaltyApplies = true
                },
                ["Gold"] = new Plan
                {
                    Name = "Gold",
                    MonthlyFee = 9.99m,
                    InternalFreeCount = int.MaxValue,
                    InternalExtraFee = 0m,
                    InterbankFee = 0.80m,
                    AtmOtherBankFee = 0.20m,
                    AtmAbroadFee = 2.00m,
                    CardPaymentPenaltyApplies = true
                },
                ["Premium"] = new Plan
                {
                    Name = "Premium",
                    MonthlyFee = 19.99m,
                    InternalFreeCount = int.MaxValue,
                    InternalExtraFee = 0m,
                    InterbankFee = 0m,
                    AtmOtherBankFee = 0m,
                    AtmAbroadFee = 0m,
                    CardPaymentPenaltyApplies = false
                }
            };
        }
    }
}
