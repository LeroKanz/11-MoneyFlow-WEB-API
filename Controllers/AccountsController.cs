using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using VZ.MoneyFlow.IData.IRepositories;
using VZ.MoneyFlow.Models.Models.Dtos;
using VZ.MoneyFlow.Models.Models.Dtos.Requests;

namespace VZ.MoneyFlow.API.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : BaseAuthController
    {
        private readonly IAccountService _accountService;
        private readonly ICurrencyService _currencyService;
        private readonly IAccountCurrencyService _accountCurrencyService;
        private readonly IMapper _mapper;

        public AccountsController(IAccountService accountService, UserManager<IdentityUser> userManager, IMapper mapper,
            ICurrencyService currencyService, IAccountCurrencyService accountCurrencyService) : base(userManager)
        {
            _accountService = accountService;
            _currencyService = currencyService;
            _accountCurrencyService = accountCurrencyService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAccounts()
        {
            var userId = GetUserId();
            var listOfResponseAccountDto = await _accountService.GetAllAsync(userId);
            return Ok(listOfResponseAccountDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAccount(Guid id)
        {
            var userId = GetUserId();
            var responseAccountDto = await _accountService.GetByIdAsync(id, userId);

            if (responseAccountDto == null) return NotFound();
            if (responseAccountDto != null) 
            {                
                return Ok(responseAccountDto);
            }
            return BadRequest("Provided data is invalid");
        }

        [HttpPost]
        public async Task<IActionResult> CreateAccount(RequestAccountDto accountDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Provided data is invalid");
            }
            var userId = GetUserId();

            var newAccountDto = _mapper.Map<AccountDto>(accountDto);
            newAccountDto.UserId = userId;

            await _accountService.AddAsync(newAccountDto);

            var currencyDto = await _currencyService.GetByTypeAsync(accountDto.CurrencyId);
            var accountCurrencyDto = new AccountCurrencyDto(accountDto.Amount, newAccountDto.Id, currencyDto.Id);

            await _accountCurrencyService.AddAsync(accountCurrencyDto);
            return Ok(newAccountDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditAccount(Guid id, RequestAccountDto requestAccountDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Provided data is invalid");
            }
            var userId = GetUserId();
            var responseAccountDto = await _accountService.GetByIdAsync(id, userId);

            if (responseAccountDto == null) return NotFound();
            if (responseAccountDto != null)
            {
                var updatedAccount = _mapper.Map(requestAccountDto, responseAccountDto);

                foreach (var c in updatedAccount.AccountsCurrencies)
                {
                    c.Amount = requestAccountDto.Amount;
                    await _accountCurrencyService.UpdateAsync(c);
                }

                await _accountService.UpdateGuidAsync(id, updatedAccount);
                return Ok(updatedAccount);
            }
            return BadRequest("Provided data is invalid");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccount(Guid id)
        {
            var userId = GetUserId();

            var account = await _accountService.GetByIdAsync(id, userId);
            if (account == null) return NotFound();
            if (account != null)
            {
                await _accountService.DeleteAsync(id);
                return Ok("Deleted successfully");
            }
            return BadRequest("Provided data is invalid");
        }
    }
}
