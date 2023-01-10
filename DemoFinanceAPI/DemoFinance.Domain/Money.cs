using DemoFinance.Domain.BuildingBlocks;
using DemoFinance.Domain.Exceptions;

namespace DemoFinance.Domain;

public class Money : ValueObject
{
    public decimal Amount { get; }
    public Currency Currency { get; }

    private Money(decimal amount, Currency currency)
    {
        Amount = amount;
        Currency = currency;
    }

    public static Money Create(decimal amount, Currency currency)
    {
        if (amount < 0)
        {
            throw new DomainException("Money amount cannot be less than zero.");
        }

        return new Money(amount, currency);
    }

    public Money Exchange(ExchangeRate exchangeRate)
    {
        if (exchangeRate.From != this.Currency)
        {
            throw new DomainException(
                $"Money currency ({this.Currency}) and source currency from given exchange rate ({exchangeRate.From}) do not match.");
        }

        return new Money(this.Amount * exchangeRate.Value, exchangeRate.To);
    }
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Amount;
        yield return Currency;
    }
}

public enum Currency // ISO 4217
{
    PLN,
    USD,
    EUR
}