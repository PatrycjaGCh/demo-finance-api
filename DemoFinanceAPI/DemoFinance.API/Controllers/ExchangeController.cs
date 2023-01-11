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
        [HttpGet("/PLN/{targetCurrency}/{amount}")]
        
        public async Task<IActionResult> Exchange(Currency targetCurrency, decimal amount)
        {
            return Ok(await _mediator.Send(new GetExchangedMoneyQuery(amount, Currency.PLN, targetCurrency)));
        }
    }
}
