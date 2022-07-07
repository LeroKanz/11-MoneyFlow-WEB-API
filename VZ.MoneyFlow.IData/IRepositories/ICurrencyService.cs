using System.Threading.Tasks;
using VZ.MoneyFlow.Models.Models.Dtos;

namespace VZ.MoneyFlow.IData.IRepositories
{
    public interface ICurrencyService
    {
        Task<CurrencyDto> GetByTypeAsync(int currencyId);
    }
}
