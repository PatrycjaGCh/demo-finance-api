using DemoFinance.Domain;

namespace DemoFinance.Infrastructure;

public class ExchangeRatesProvider : IExchangeRatesProvider
{
    public Task<ExchangeRate> Get(Currency from, Currency to)
    {
        return Task.FromResult(ExchangeRate.Create(Currency.PLN, Currency.EUR, 3));
    }
}