﻿using System;

namespace VZ.MoneyFlow.Models.Models.Dtos
{
    public class AccountCurrencyDto
    {
        public decimal Amount { get; set; }
        public Guid AccountId { get; set; }
        public int CurrencyId { get; set; }

        public AccountCurrencyDto(decimal amount, Guid accountId, int currencyId)
        {
            Amount = amount;
            AccountId = accountId;
            CurrencyId = currencyId;
        }
    }
}
