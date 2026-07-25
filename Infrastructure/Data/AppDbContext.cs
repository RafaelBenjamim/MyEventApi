using Microsoft.EntityFrameworkCore;
using MyEventApi.Core.Entities;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace MyEventApi.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<StoreEntity> Stores { get; set; }
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<EventEntity> Events { get; set; }
        public DbSet<RegistrationEntity> Registrations { get; set; }
        public DbSet<PaymentEntity> Payments { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Slug e Email devem ser únicos
            modelBuilder.Entity<StoreEntity>()
                .HasIndex(s => s.Slug)
                .IsUnique();

            modelBuilder.Entity<UserEntity>()
                .HasIndex(u => u.Email)
                .IsUnique();

            // Decimal precision (evita warning do EF sobre precisão do PostgreSQL)
            modelBuilder.Entity<EventEntity>()
                .Property(e => e.Price)
                .HasPrecision(10, 2);

            modelBuilder.Entity<PaymentEntity>()
                .Property(p => p.AmountPaid)
                .HasPrecision(10, 2);

            // Registration → Payment é 1-para-1 (nem toda inscrição tem pagamento ainda)
            modelBuilder.Entity<RegistrationEntity>()
                .HasOne(r => r.Payment)
                .WithOne(p => p.Registration)
                .HasForeignKey<PaymentEntity>(p => p.RegistrationId);
        }
    }
}
