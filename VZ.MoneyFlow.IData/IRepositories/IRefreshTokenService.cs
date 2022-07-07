using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using VZ.MoneyFlow.Models.Models.Dtos.Requests;
using VZ.MoneyFlow.Models.Models.Dtos.Responses;

namespace VZ.MoneyFlow.IData.IRepositories
{
    public interface IRefreshTokenService
    {
        Task<ResponseAuthResultDto> VerifyAndGenerateToken(RequestTokenDto tokenRequest);
        Task<ResponseAuthResultDto> GenerateJwtToken(IdentityUser user);
    }
}
