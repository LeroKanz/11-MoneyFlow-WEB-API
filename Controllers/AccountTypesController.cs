using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using VZ.MoneyFlow.Entities.Enums;

namespace VZ.MoneyFlow.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountTypesController : ControllerBase
    {
        [HttpGet]
        public Dictionary<int, string> GetAllAccountTypes()
        {
            var accountTypes = Enum.GetValues(typeof(AccountType)).Cast<AccountType>()
               .ToDictionary(t => (int)t, t => t.ToString());
            return accountTypes;
        }
    }
}
