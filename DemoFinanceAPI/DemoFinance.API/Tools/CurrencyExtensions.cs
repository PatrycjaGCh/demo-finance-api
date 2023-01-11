using System.Globalization;
using DemoFinance.Domain;

namespace DemoFinance.API.Tools;

public static class CurrencyExtensions
{
    public static string GetWithFormattedCurrency(this decimal amount, Currency currency) // todo: tests
    {
        var cultureInfo = CultureInfo.GetCultures(CultureTypes.SpecificCultures)
            .First(x => string.Equals((new RegionInfo(x.Name)).ISOCurrencySymbol, currency.ToString().ToUpper()));

        return amount.ToString("C", cultureInfo);
    }
}