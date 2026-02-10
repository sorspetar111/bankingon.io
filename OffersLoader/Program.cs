using Microsoft.EntityFrameworkCore;
using OffersLoader.Data;
using OffersLoader.Services;

Console.WriteLine("=== Offers CSV Loader (EF Core + SQL Server) ===\n");

// Configuration
const string connectionString = "Server=(localdb)\\mssqllocaldb;Database=BankingDb;Trusted_Connection=true;";
const string csvFilePath = "offers.csv";

// Setup DbContext
var optionsBuilder = new DbContextOptionsBuilder<OffersContext>();
optionsBuilder.UseSqlServer(connectionString);

try
{
    using (var context = new OffersContext(optionsBuilder.Options))
    {
        // Ensure database is created
        context.Database.EnsureCreated();
        Console.WriteLine("✓ Database ensured created.\n");

        // Check if data already exists
        int existingCount = context.Offers.Count();
        if (existingCount > 0)
        {
            Console.WriteLine($"Database already contains {existingCount} offers.");
            Console.Write("Proceed with reload? (y/n): ");
            var response = Console.ReadLine() ?? "n";
            if (response.ToLower() == "y")
            {
                context.Offers.RemoveRange(context.Offers);
                context.SaveChanges();
                Console.WriteLine("Cleared existing data.\n");
            }
            else
            {
                Console.WriteLine("Exiting without changes.");
                return 0;
            }
        }

        // Load CSV
        Console.WriteLine($"Loading {csvFilePath}...");
        var offers = CsvHelper.DeserializeFromCsv(csvFilePath);
        Console.WriteLine($"✓ Loaded {offers.Count} offers from CSV.\n");

        if (offers.Count == 0)
        {
            Console.WriteLine("No records to insert.");
            return 0;
        }

        // Insert into database
        context.Offers.AddRange(offers);
        context.SaveChanges();
        Console.WriteLine($"✓ Inserted {offers.Count} offers into database.\n");

        // Display loaded data
        Console.WriteLine("Offers in database:");
        Console.WriteLine(new string('-', 80));
        Console.WriteLine("{0,-8} {1,-15} {2,-20} {3,-20} {4,-20}",
            "ID", "Monthly Fee", "New Contracts", "Same Contracts", "Cancelled Contracts");
        Console.WriteLine(new string('-', 80));

        foreach (var offer in context.Offers.OrderBy(o => o.OfferId))
        {
            Console.WriteLine("{0,-8} {1,-15:F2} {2,-20} {3,-20} {4,-20}",
                offer.OfferId,
                offer.MonthlyFee,
                offer.NewContractsForMonth,
                offer.SameContractsForMonth,
                offer.CancelledContractsForMonth);
        }

        Console.WriteLine(new string('-', 80));
        Console.WriteLine($"\n✓ Process completed successfully!");
    }
}
catch (Exception ex)
{
    Console.WriteLine($"\n✗ Error: {ex.Message}");
    if (ex.InnerException != null)
        Console.WriteLine($"  Inner: {ex.InnerException.Message}");
    return 1;
}

return 0;
