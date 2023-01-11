namespace DemoFinance.Domain;

public interface IExchangeRatesProvider
{
    Task<ExchangeRate> Get(Currency from, Currency to); // there could be parameter with date for example // for past dates - cache
}