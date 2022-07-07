using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VZ.MoneyFlow.Entities.DbSet;
using VZ.MoneyFlow.IData.IRepositories;
using VZ.MoneyFlow.Models.Models.Dtos;
using VZ.MoneyFlow.Models.Models.Dtos.Requests;
using VZ.MoneyFlow.Models.Models.Dtos.Responses;

namespace VZ.MoneyFlow.EFData.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IMapper _mapper;
        private readonly IAccountCurrencyService _accountCurrencyService;

        public AccountService(IAccountRepository accountRepository, IMapper mapper, IAccountCurrencyService accountCurrencyService)
        {
            _accountRepository = accountRepository;
            _mapper = mapper;
            _accountCurrencyService = accountCurrencyService;
        }

        public async Task<IEnumerable<ResponseAccountDto>> GetAllAsync(string userId)
        {
            var result = await _accountRepository.GetAllAsync(userId);
            var response = _mapper.Map<List<ResponseAccountDto>>(result);
            return response;
        }

        public async Task<ResponseAccountDto> GetByIdAsync(Guid id, string userId)
        {
            var result = await _accountRepository.GetByIdAsync(id, userId);
            var response = _mapper.Map<ResponseAccountDto>(result);
            return response;
        }

        public async Task AddAsync(AccountDto accountDto)
        {
            var account = _mapper.Map<Account>(accountDto);
            await _accountRepository.AddAsync(account);
        }

        public async Task UpdateGuidAsync(Guid id, ResponseAccountDto account)
        {
            var newAccount = _mapper.Map<Account>(account);
            await _accountRepository.UpdateGuidAsync(id, newAccount);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _accountRepository.DeleteAsync(id);
        }
    }
}
