using System;

namespace BankingFees
{
    public static class InputReader
    {
        public static CustomerUsage ReadUsageFromConsole()
        {
            string plan = PromptString("Plan (Standard/Gold/Premium)");
            bool hasSalary = PromptBool("Has salary transfer? (y/n)");
            bool isStudent = PromptBool("Is student? (y/n)");
            int internalTransfers = PromptInt("Internal transfers count");
            int interbankTransfers = PromptInt("Interbank transfers count");
            int immediateTransfers = PromptInt("Immediate transfers count");
            int atmSame = PromptInt("ATM withdrawals (same bank)");
            int atmOther = PromptInt("ATM withdrawals (other bank)");
            int atmAbroad = PromptInt("ATM withdrawals (abroad)");
            int cardPayments = PromptInt("Card payments count");
            decimal overdraft = PromptDecimal("Overdraft used (amount)");
            decimal otherFees = PromptDecimal("Other fees");
            decimal discounts = PromptDecimal("Discounts");

            return new CustomerUsage(plan, hasSalary, isStudent, internalTransfers, interbankTransfers, immediateTransfers, atmSame, atmOther, atmAbroad, cardPayments, overdraft, otherFees, discounts);
        }

        private static string PromptString(string prompt)
        {
            Console.Write($"{prompt}: ");
            return Console.ReadLine() ?? string.Empty;
        }

        private static bool PromptBool(string prompt)
        {
            while (true)
            {
                var v = PromptString(prompt + "");
                if (string.IsNullOrWhiteSpace(v)) return false;
                v = v.Trim().ToLowerInvariant();
                if (v is "y" or "yes" or "да") return true;
                if (v is "n" or "no" or "не") return false;
                Console.WriteLine("Please answer y/n");
            }
        }

        private static int PromptInt(string prompt)
        {
            while (true)
            {
                var s = PromptString(prompt + ": ");
                if (int.TryParse(s, out var n)) return n;
                Console.WriteLine("Enter a valid integer (0 if none). Try again.");
            }
        }

        private static decimal PromptDecimal(string prompt)
        {
            while (true)
            {
                var s = PromptString(prompt + "");
                if (decimal.TryParse(s, System.Globalization.NumberStyles.Number, System.Globalization.CultureInfo.InvariantCulture, out var d)) 
                    return d;
                
                // Try local culture fallback
                if (decimal.TryParse(s, out d)) 
                    return d;
                
                Console.WriteLine("Enter a valid decimal number (use dot or comma). Try again.");
            }
        }
    }
}
