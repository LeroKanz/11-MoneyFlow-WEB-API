using AutoMapper;
using System.Threading.Tasks;
using VZ.MoneyFlow.EFData.Data;
using VZ.MoneyFlow.IData.IRepositories;
using VZ.MoneyFlow.Models.Models.Dtos;

namespace VZ.MoneyFlow.EFData.Services
{
    public class CurrencyService : ICurrencyService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public CurrencyService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CurrencyDto> GetByTypeAsync(int currencyId)
        {
            var result = await _context.Currencies.FindAsync(currencyId);
            var currency = _mapper.Map<CurrencyDto>(result);
            return currency;
        }       
    }
}
