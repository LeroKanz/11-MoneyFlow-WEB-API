using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using VZ.MoneyFlow.Entities.DbSet;
using VZ.MoneyFlow.Entities.Enums;

namespace VZ.MoneyFlow.Models.Models.Dtos
{
    public class AccountDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public AccountType AccountType { get; set; }
        public string BankAccountNumber { get; set; }
        public int? LastFourDigitsOfCard { get; set; }

        //Relationships
        public string UserId { get; set; }
        public IdentityUser User { get; set; }

        public List<AccountCurrency> AccountsCurrencies { get; set; }
    }
}
