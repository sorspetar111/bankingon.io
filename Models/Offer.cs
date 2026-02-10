using System.ComponentModel.DataAnnotations;

namespace Console
{
    public class Offer
    {
        [Key]
        public int OfferId { get; set; }

        [Required]
        public decimal MonthlyFee { get; set; }

        [Required]
        public int NewContractsForMonth { get; set; }

        [Required]
        public int SameContractsForMonth { get; set; }

        [Required]
        public int CancelledContractsForMonth { get; set; }
    }
}
