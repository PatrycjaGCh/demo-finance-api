using System.Diagnostics;
using System.Globalization;
using DemoFinance.Application;
using DemoFinance.Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DemoFinance.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExchangeController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ExchangeController(IMediator mediator) 
            => _mediator = mediator;

        // GET: api/exchange/PLN
        [HttpGet("/PLN")]
        public async Task<IActionResult> Exchange(Currency targetCurrency, decimal amount)
        {
            var exchangedMoney = await _mediator.Send(new GetExchangedMoneyQuery(amount, Currency.PLN, targetCurrency));

            var cultureInfo = CultureInfo.GetCultures(CultureTypes.SpecificCultures)
                .First(x => string.Equals((new RegionInfo(x.Name)).ISOCurrencySymbol, exchangedMoney.Currency.ToString().ToUpper()));
            
            return Ok(exchangedMoney.Amount.ToString("C", cultureInfo));
        }
        
        // general to do in app: exceptions handling, for example exceptions middleware
    }
}
