using AutoMapper;
using VZ.MoneyFlow.Entities.DbSet;
using VZ.MoneyFlow.Models.Models.Dtos;
using VZ.MoneyFlow.Models.Models.Dtos.Requests;
using VZ.MoneyFlow.Models.Models.Dtos.Responses;

namespace VZ.MoneyFlow.EFData.Data
{
    public class AppMapper : Profile
    {
        public AppMapper()
        {
            CreateMap<RequestAccountDto, Account>().ReverseMap();
            CreateMap<ResponseAccountDto, Account>().ReverseMap();
            CreateMap<Account, AccountDto>().ReverseMap();
            CreateMap<RequestAccountDto, AccountDto>().ReverseMap();
            CreateMap<RequestAccountDto, ResponseAccountDto>().ReverseMap();
            CreateMap<RequestCreateCategoryDto, Category>().ReverseMap();
            CreateMap<RequestUpdateCategoryDto, Category>().ReverseMap();
            CreateMap<AccountCurrency, AccountCurrencyDto>().ReverseMap();
            CreateMap<Currency, CurrencyDto>().ReverseMap();
        }
    }
}
