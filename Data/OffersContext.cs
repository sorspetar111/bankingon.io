using Microsoft.EntityFrameworkCore;
using OffersLoader.Models;

namespace OffersLoader.Data
{
    public class OffersContext : DbContext
    {
        public DbSet<Offer> Offers { get; set; } = null!;

        public OffersContext(DbContextOptions<OffersContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Offer>(entity =>
            {
                entity.ToTable("Offers");
                entity.HasKey(e => e.OfferId);

                entity.Property(e => e.OfferId)
                    .ValueGeneratedNever()
                    .HasColumnName("OfferId");

                entity.Property(e => e.MonthlyFee)
                    .HasColumnType("DECIMAL(10, 2)")
                    .HasColumnName("MonthlyFee");

                entity.Property(e => e.NewContractsForMonth)
                    .HasColumnName("NewContractsForMonth");

                entity.Property(e => e.SameContractsForMonth)
                    .HasColumnName("SameContractsForMonth");

                entity.Property(e => e.CancelledContractsForMonth)
                    .HasColumnType("INT")
                    .HasColumnName("CancelledContractsForMonth");
            });
        }
    }
}
