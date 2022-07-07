using System;
using System.Collections.Generic;
using VZ.MoneyFlow.Entities.DbSet;
using VZ.MoneyFlow.Entities.Enums;

namespace VZ.MoneyFlow.Models.Models.Dtos.Responses
{
    public class ResponseAccountDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public AccountType AccountType { get; set; }
        public string BankAccountNumber { get; set; }
        public int? LastFourDigitsOfCard { get; set; }
        public int CurrencyId { get; set; }
        public decimal Amount { get; set; }

        public List<AccountCurrency> AccountsCurrencies { get; set; }
    }
}
