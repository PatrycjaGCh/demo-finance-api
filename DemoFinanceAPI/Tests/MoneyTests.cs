using System.Runtime.CompilerServices;
using DemoFinance.Domain;
using DemoFinance.Domain.Exceptions;
using FluentAssertions;

namespace Tests;

public class MoneyTests
{
    [Fact]
    public void Domain_Exception_Should_Be_Thrown_When_Money_Value_Is_Less_Than_Zero()
    {
        Action creatingMoney = () => Money.Create(-10, Currency.EUR);

        creatingMoney.Should().Throw<DomainException>();
    }

    [Fact]
    public void Money_Should_Be_Created_For_Given_Amount_And_Currency()
    {
        //arrange
        var amount = 51.5m;
        var currency = Currency.EUR;
        
        //act
        var result = Money.Create(amount, currency);

        //arrange
        result.Amount.Should().Be(amount);
        result.Currency.Should().Be(currency);
    }
    
}