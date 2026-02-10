using BankingFees;

Console.WriteLine("Banking Fees Calculator (NET 8)");

// Gather input
var usage = InputReader.ReadUsageFromConsole();

// Setup plans (data-driven so new plans can be added without changing business logic)
var plans = PlanRegistry.DefaultPlans();
if (!plans.TryGetValue(usage.PlanName, out var plan))
{
    Console.WriteLine($"Unknown plan '{usage.PlanName}'. Available: {string.Join(", ", plans.Keys)}");
    return 1;
}

// Build fee rules (extensible via IFeeRule)
var rules = new List<IFeeRule>
{
    new MonthlyFeeRule(),
    new InternalTransferRule(),
    new InterbankRule(),
    new ImmediateTransferRule(),
    new AtmRule(),
    new CardPaymentsRule(),
    new OverdraftRule(),
    new OtherFeesRule(),
    new DiscountsRule(),
    // student discount applied last
    new StudentDiscountRule()
};

var calculator = new FeeCalculator(rules);
var total = calculator.CalculateTotal(usage, plan);

Console.WriteLine($"Total monthly fees: {total:C2}");
return 0;
