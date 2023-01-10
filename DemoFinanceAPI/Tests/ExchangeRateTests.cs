using DemoFinance.Domain;
using DemoFinance.Domain.Exceptions;
using FluentAssertions;

namespace Tests;

public class ExchangeRateTests
{
    [Fact]
    public void Exchange_Rate_Should_Be_Created_For_Given_Currencies_And_Rate_Value()
    {
        //arrange
        var from = Currency.EUR;
        var to = Currency.USD;
        var rateValue = 3.54m;
        
        //act
        var result = ExchangeRate.Create(from, to, rateValue);
        
        //assert
        result.From.Should().Be(from);
        result.To.Should().Be(to);
        result.Value.Should().Be(rateValue);
    }

    [Fact]
    public void Domain_Exception_Should_Be_Thrown_When_Given_Currencies_Are_Equal()
    {
        Action createExchangeRate = () => ExchangeRate.Create(Currency.EUR, Currency.EUR, 3.5m);

        createExchangeRate.Should().Throw<DomainException>();
    }
    
}