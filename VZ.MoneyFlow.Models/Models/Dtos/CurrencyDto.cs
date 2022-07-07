using System.Collections.Generic;
using VZ.MoneyFlow.Entities.DbSet;
using VZ.MoneyFlow.Entities.Enums;

namespace VZ.MoneyFlow.Models.Models.Dtos
{
    public class CurrencyDto
    {
        public int Id { get; set; }
        public CurrencyType CurrencyType { get; set; }

        //Relationships
        public List<AccountCurrency> AccountsCurrencies { get; set; }
    }
}
