using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.Context
{
    public class SMDbContext : DbContext
    {
        public SMDbContext(DbContextOptions<SMDbContext> options) : base(options) 
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tag>().ToTable("Tags");
            modelBuilder.Entity<MoneyFormat>().ToTable("MoneyFormats");
            modelBuilder.Entity<Currency>().ToTable("Currencies");
            modelBuilder.Entity<Person>().ToTable("People");
            modelBuilder.Entity<Transaction>().ToTable("Transactions");
            modelBuilder.Entity<TagTransaction>().ToTable("TagTransactions");
            modelBuilder.Entity<Debt>().ToTable("Debt");
        }

        public virtual DbSet<MoneyFormat> MoneyFormats { get; set; }
        public virtual DbSet<Currency> Currencies { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }
        public virtual DbSet<Person> People { get; set; }
        public virtual DbSet<Transaction> Transactions { get; set; }
        public virtual DbSet<TagTransaction> TagTransactions { get; set; }
        public virtual DbSet<Debt> Debt { get; set; }
    }
}
