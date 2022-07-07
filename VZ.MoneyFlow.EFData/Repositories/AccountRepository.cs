using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VZ.MoneyFlow.EFData.Data;
using VZ.MoneyFlow.Entities.DbSet;
using VZ.MoneyFlow.IData.IRepositories;

namespace VZ.MoneyFlow.EFData.Repositories
{
    public class AccountRepository : GenericRepository<Account>, IAccountRepository
    {
        private readonly AppDbContext _context;
        public AccountRepository(AppDbContext options) : base(options)
        {
            _context = options;
        }

        public async Task<IEnumerable<Account>> GetAllAsync(string userId)
        {
            var result = await _context.Accounts.Where(c => c.UserId == userId).Include(c => c.User).Include(c => c.AccountsCurrencies).ToListAsync();
            return result;
        }
        public async Task<Account> GetByIdAsync(Guid id, string userId)
        {
            var result = await _context.Accounts.Where(c => c.UserId == userId).Include(c => c.User).Include(c => c.AccountsCurrencies).FirstOrDefaultAsync(acc => acc.Id == id);
            return result;
        } 

        public async Task UpdateGuidAsync(Guid id, Account account)
        { 
            EntityEntry entityEntry = _context.Entry<Account>(account);
            entityEntry.State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await _context.Set<Account>().FindAsync(id);
            EntityEntry entityEntry = _context.Entry<Account>(entity);
            entityEntry.State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }
    }
}
