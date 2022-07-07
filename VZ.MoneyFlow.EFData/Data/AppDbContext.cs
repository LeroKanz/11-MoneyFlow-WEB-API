using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VZ.MoneyFlow.Entities.DbSet;

namespace VZ.MoneyFlow.EFData.Data
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base (options)
        {
        }

        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Operation> Operations { get; set; }        
        public DbSet<Transfer> Transfers { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<CurrencyExchange> CurrencyExchanges { get; set; }
        public DbSet<AccountCurrency> AccountsCurrencies { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AccountCurrency>().HasKey(accountCurrency => new
            {
                accountCurrency.AccountId,
                accountCurrency.CurrencyId
            });
            modelBuilder.Entity<AccountCurrency>().HasOne(account => account.Account)
                .WithMany(accountCurrency => accountCurrency.AccountsCurrencies).HasForeignKey(account => account.AccountId)
                .IsRequired();
            modelBuilder.Entity<AccountCurrency>().HasOne(currency => currency.Currency)
                .WithMany(accountCurrency => accountCurrency.AccountsCurrencies).HasForeignKey(currency => currency.CurrencyId);


            base.OnModelCreating(modelBuilder);
        }
    }
}
  