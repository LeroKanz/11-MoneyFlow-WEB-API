using AutoMapper;
using System;
using System.Threading.Tasks;
using VZ.MoneyFlow.Entities.DbSet;
using VZ.MoneyFlow.IData.IRepositories;
using VZ.MoneyFlow.Models.Models.Dtos;

namespace VZ.MoneyFlow.EFData.Services
{
    public class AccountCurrencyService : IAccountCurrencyService
    {
        private readonly IAccountCurrencyRepository _accountCurrencyRepository;
        private readonly IMapper _mapper;
        public AccountCurrencyService(IAccountCurrencyRepository accountCurrencyRepository, IMapper mapper)
        {
            _accountCurrencyRepository = accountCurrencyRepository;
            _mapper = mapper;
        }

        public async Task AddAsync(AccountCurrencyDto accountCurrency)
        {
            var newAC = _mapper.Map<AccountCurrency>(accountCurrency);
            await _accountCurrencyRepository.AddAsync(newAC);
        }

        public async Task UpdateAsync(AccountCurrency accountCurrency)
        {
            await _accountCurrencyRepository.UpdateAsync(accountCurrency);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _accountCurrencyRepository.DeleteAsync(id);
        }
    }
}
