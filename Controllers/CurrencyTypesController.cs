using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using VZ.MoneyFlow.Entities.Enums;

namespace VZ.MoneyFlow.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrencyTypesController : ControllerBase
    {
        [HttpGet]
        public Dictionary<int, string> GetAllCurrencyTypes()
        {            
            var currencyTypes = Enum.GetValues(typeof(CurrencyType)).Cast<CurrencyType>()
               .ToDictionary(t => (int)t, t => t.ToString());
            return currencyTypes;
        }
    }
}
