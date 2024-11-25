using Microsoft.EntityFrameworkCore;
using RapidPayAPI.Data.Entities;
using System.Data.Common;

namespace RapidPayAPI.Data.Database
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Card> Cards => Set<Card>();
        public virtual DbSet<PaymentTransaction> Transactions => Set<PaymentTransaction>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Card>(entity =>
            {
                entity.ToTable("Card");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.HolderName)
                    .IsRequired(false)
                    .HasMaxLength(255);

                entity.Property(e => e.CardNumberLastFourDigits)
                    .IsRequired()
                    .HasMaxLength(4);

                entity.Property(e => e.CardToken)
                    .IsRequired();

                entity.Property(e => e.Balance)
                    .IsRequired();
            });

            modelBuilder.Entity<PaymentTransaction>(entity =>
            {
                entity.ToTable("PaymentTransaction");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.CardToken)
                    .IsRequired();

                entity.Property(e => e.Amount)
                    .IsRequired();

                entity.Property(e => e.Fee)
                    .IsRequired();

                entity.Property(e => e.TotalAmount)
                    .IsRequired();

                entity.Property(e => e.Description)
                    .IsRequired(false)
                    .HasMaxLength(255);
            });
        }

    }
}
