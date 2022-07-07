using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using VZ.MoneyFlow.Entities.Enums;

namespace VZ.MoneyFlow.Entities.DbSet
{
    public class Account
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
