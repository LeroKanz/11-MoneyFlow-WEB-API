using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VZ.MoneyFlow.Models.Models.Dtos;
using VZ.MoneyFlow.Models.Models.Dtos.Requests;
using VZ.MoneyFlow.Models.Models.Dtos.Responses;

namespace VZ.MoneyFlow.IData.IRepositories
{
    public interface IAccountService
    {
        Task<IEnumerable<ResponseAccountDto>> GetAllAsync(string userId);
        Task<ResponseAccountDto> GetByIdAsync(Guid id, string userId);
        Task AddAsync(AccountDto entity);
        Task UpdateGuidAsync(Guid id, ResponseAccountDto account);
        Task DeleteAsync(Guid id);
    }
}
