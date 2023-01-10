using DemoFinance.Domain;
using DemoFinance.Domain.Exceptions;
using FluentAssertions;

namespace DemoFinance.UnitTests;

public class ExchangeTests
{
    [Fact]
    public void Domain_Exception_Should_Be_Thrown_When_Current_Currency_And_Exchange_Rate_Source_Currency_Do_Not_Match()
    {
        //arrange
        var money = Money.Create(120.5m, Currency.EUR);
        var exchangeRate = ExchangeRate.Create(Currency.PLN, Currency.EUR, 0.21m);

        //act
        Action exchange = () => money.Exchange(exchangeRate);

        //assert
        exchange.Should().Throw<DomainException>();
    }

    [Fact]
    public void Money_Should_Be_Exchanged_For_Given_Exchange_Rate()
    {
        //arrange
        var money = Money.Create(120.5m, Currency.PLN);
        var exchangeRate = ExchangeRate.Create(Currency.PLN, Currency.EUR, 0.21m);

        //act
        var result = money.Exchange(exchangeRate);

        //assert
        result.Amount.Should().Be(25.305m);
        result.Currency.Should().Be(Currency.EUR);
    }
}