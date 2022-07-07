using System;
using System.Threading.Tasks;
using VZ.MoneyFlow.Entities.DbSet;

namespace VZ.MoneyFlow.IData.IRepositories
{
    public interface IAccountCurrencyRepository : IGenericRepository<AccountCurrency>
    {
        Task UpdateAsync(AccountCurrency account);
        Task DeleteAsync(Guid id);


    }
}
