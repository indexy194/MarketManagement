using CoreBusiness;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Reflection;

namespace Plugins.DataStore.SQL
{
    public class MarketContext : IdentityDbContext
    {
        public MarketContext(DbContextOptions<MarketContext> options) : base(options)
        {
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<WorkingHours> WorkingHours { get; set; }
        public DbSet<EmploymentTerms> EmploymentTerms { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //config moderBuilder
            //eg: ProductConfiguration.Configure(modelBuilder);
            modelBuilder.HasDefaultSchema("dbo");
            UserConfiguration.Configure(modelBuilder);
            CategoryConfiguration.Configure(modelBuilder);
            ProductConfiguration.Configure(modelBuilder);
            EmploymentTermsConfiguration.Configure(modelBuilder);
            WorkingHoursConfiguration.Configure(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            
            base.OnModelCreating(modelBuilder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
            CancellationToken cancellationToken = default)
        {
            OnBeforeSaving();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
        protected virtual void OnBeforeSaving()
        {
            //foreach (var entry in ChangeTracker.Entries())
            //{
                
            //    if (entry.State == EntityState.Added)
            //    {
                    
            //        ((CommonEntity)entry.Entity).CreatedAt = DateTime.UtcNow;
            //    }
            //    ((CommonEntity)entry.Entity).UpdatedAt = DateTime.UtcNow;
            //}
        }
    }
}
