using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Threading.Tasks;
using VZ.MoneyFlow.EFData.Data;
using VZ.MoneyFlow.Entities.DbSet;
using VZ.MoneyFlow.IData.IRepositories;

namespace VZ.MoneyFlow.EFData.Repositories
{
    public class AccountCurrencyRepository : GenericRepository<AccountCurrency>, IAccountCurrencyRepository
    {
        private readonly AppDbContext _context;
        public AccountCurrencyRepository(AppDbContext options) : base(options)
        {
            _context = options;
        }
        
        public async Task UpdateAsync(AccountCurrency account)
        { 
            EntityEntry entityEntry = _context.Entry<AccountCurrency>(account);
            entityEntry.State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await _context.Set<AccountCurrency>().FindAsync(id);
            EntityEntry entityEntry = _context.Entry<AccountCurrency>(entity);
            entityEntry.State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }
    }
}
