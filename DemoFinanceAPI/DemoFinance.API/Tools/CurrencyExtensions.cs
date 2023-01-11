using System.Globalization;
using DemoFinance.Domain;

namespace DemoFinance.API.Tools;

public static class CurrencyExtensions
{
    public static string GetWithCurrencyFormatted(this decimal amount, Currency currency) // to do: tests
    {
        var cultureInfo = CultureInfo.GetCultures(CultureTypes.SpecificCultures)
            .First(x => string.Equals((new RegionInfo(x.Name)).ISOCurrencySymbol, currency.ToString().ToUpper()));

        return amount.ToString("C", cultureInfo);
    }
}