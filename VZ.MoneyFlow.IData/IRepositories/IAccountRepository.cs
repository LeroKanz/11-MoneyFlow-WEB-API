using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VZ.MoneyFlow.Entities.DbSet;

namespace VZ.MoneyFlow.IData.IRepositories
{
    public interface IAccountRepository : IGenericRepository<Account>
    {
        Task<IEnumerable<Account>> GetAllAsync(string userId);
        Task<Account> GetByIdAsync(Guid id, string userId);
        Task UpdateGuidAsync(Guid id, Account account);
        Task DeleteAsync(Guid id);


    }
}
