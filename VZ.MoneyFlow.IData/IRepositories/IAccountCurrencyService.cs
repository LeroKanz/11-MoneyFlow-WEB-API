using System;
using System.Threading.Tasks;
using VZ.MoneyFlow.Entities.DbSet;
using VZ.MoneyFlow.Models.Models.Dtos;

namespace VZ.MoneyFlow.IData.IRepositories
{
    public interface IAccountCurrencyService
    {
        Task AddAsync(AccountCurrencyDto entity);
        Task UpdateAsync(AccountCurrency entity);
        Task DeleteAsync(Guid id);
    }
}
