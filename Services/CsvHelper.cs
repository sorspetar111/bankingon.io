using OffersLoader.Models;
using System.Globalization;

namespace OffersLoader.Services
{
    public static class CsvHelper
    {
        /// <summary>
        /// Deserializes offers from a CSV file.
        /// Expected format: offerId;monthlyFee;newContractsForMonth;sameContractsForMonth;CancelledContractsForMonth
        /// </summary>
        public static List<Offer> DeserializeFromCsv(string filePath)
        {
            var offers = new List<Offer>();

            if (!File.Exists(filePath))
                throw new FileNotFoundException($"CSV file not found: {filePath}");

            var lines = File.ReadAllLines(filePath);

            // Skip header if present
            int startLine = lines.Length > 0 && lines[0].Contains("offerId") ? 1 : 0;

            for (int i = startLine; i < lines.Length; i++)
            {
                var line = lines[i].Trim();
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                try
                {
                    var parts = line.Split(';');
                    if (parts.Length < 5)
                    {
                        Console.WriteLine($"Warning: Line {i + 1} has fewer than 5 columns, skipping.");
                        continue;
                    }

                    var offer = new Offer
                    {
                        OfferId = int.Parse(parts[0].Trim()),
                        MonthlyFee = ParseDecimal(parts[1].Trim()),
                        NewContractsForMonth = int.Parse(parts[2].Trim()),
                        SameContractsForMonth = int.Parse(parts[3].Trim()),
                        CancelledContractsForMonth = int.Parse(parts[4].Trim())
                    };

                    offers.Add(offer);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error parsing line {i + 1}: {ex.Message}");
                }
            }

            return offers;
        }

        /// <summary>
        /// Serializes offers to a CSV file.
        /// </summary>
        public static void SerializeToCsv(List<Offer> offers, string filePath)
        {
            using (var writer = new StreamWriter(filePath))
            {
                // Write header
                writer.WriteLine("offerId;monthlyFee;newContractsForMonth;sameContractsForMonth;CancelledContractsForMonth");

                // Write data
                foreach (var offer in offers)
                {
                    var monthlyFeeStr = offer.MonthlyFee.ToString("0.00", CultureInfo.InvariantCulture).Replace(".", ",");
                    writer.WriteLine($"{offer.OfferId};{monthlyFeeStr};{offer.NewContractsForMonth};{offer.SameContractsForMonth};{offer.CancelledContractsForMonth}");
                }
            }
        }

        /// <summary>
        /// Parses a decimal value that may use either comma or dot as separator.
        /// </summary>
        private static decimal ParseDecimal(string value)
        {
            // Try invariant culture first (dot separator)
            if (decimal.TryParse(value, NumberStyles.Number, CultureInfo.InvariantCulture, out var result))
                return result;

            // Try European culture (comma separator)
            if (decimal.TryParse(value, NumberStyles.Number, new CultureInfo("bg-BG"), out result))
                return result;

            throw new FormatException($"Cannot parse decimal value: {value}");
        }
    }
}
