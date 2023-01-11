using System.Text.Json;
using DemoFinance.Domain;
using HttpClient = System.Net.Http.HttpClient;

namespace DemoFinance.Infrastructure;

public class ExchangeRatesProvider : IExchangeRatesProvider
{
    public async Task<ExchangeRate> Get(Currency from, Currency to)
    {
        if (from != Currency.PLN)
        {
            throw new ArgumentException("NBP API is only for PLN as a source currency.");
        }
        
        using HttpClient client = new();
        client.BaseAddress = new Uri("http://api.nbp.pl/"); // todo: < config
        var response = await client.GetAsync($"/api/exchangerates/rates/a/{to.ToString().ToLower()}/");
        response.EnsureSuccessStatusCode(); // todo: codes handling
        var content = await response.Content.ReadAsStringAsync();
        
        var exchangeRate = JsonSerializer.Deserialize<ExchangeRateResponse>(content, new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true
        }); // todo: checking content if exists

        return ExchangeRate.Create(from, to, exchangeRate.Rates.First().Mid);
    }
}

public class ExchangeRateResponse
{
    public Rate[] Rates { get; set; } = null!;

    public class Rate
    {
        public decimal Mid { get; set; }

        public string No { get; set; }

        public DateTime EffectiveDate { get; set; }
    }
}