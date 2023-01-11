using DemoFinance.Domain;
using MediatR;

namespace DemoFinance.Application;

public record GetExchangedMoneyQuery(decimal Amount, Currency From, Currency To) : IRequest<MoneyDto> { }

public class GetExchangedMoneyQueryHandler : IRequestHandler<GetExchangedMoneyQuery, MoneyDto>
{
    private readonly IExchangeRatesProvider _exchangeRatesProvider;

    public GetExchangedMoneyQueryHandler(IExchangeRatesProvider exchangeRatesProvider) 
        => _exchangeRatesProvider = exchangeRatesProvider;

    public async Task<MoneyDto> Handle(GetExchangedMoneyQuery request, CancellationToken cancellationToken)
    {
        var exchangeRate = await _exchangeRatesProvider.Get(request.From, request.To);
        
        var exchangedMoney = Money.Create(request.Amount, request.From).Exchange(exchangeRate);
        
        return new MoneyDto {Amount = exchangedMoney.Amount, Currency = exchangedMoney.Currency};
    }
}

public class MoneyDto
{
    public decimal Amount { get; set; }
    public Currency Currency { get; set; }
}