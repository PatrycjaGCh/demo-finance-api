using DemoFinance.Domain.BuildingBlocks;
using DemoFinance.Domain.Exceptions;

namespace DemoFinance.Domain;

public class ExchangeRate : ValueObject
{
    public Currency From { get; }
    public Currency To { get; }
    public decimal Value { get; }

    private ExchangeRate(Currency from, Currency to, decimal value)
    {
        From = from;
        To = to;
        Value = value;
    }

    public static ExchangeRate Create(Currency from, Currency to, decimal value)
    {
        if (from == to)
        {
            throw new DomainException($"Source currency ({from}) and target currency ({to}) cannot be the same.");
        }
        
        return new ExchangeRate(from, to, value);
    }
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return From;
        yield return To;
        yield return Value;
    }
}