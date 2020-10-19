using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace currencies.Services
{
    public static class CurrencyLookup
    {
        private static IDictionary<string, string> _currencies;

        static CurrencyLookup()
        {
            _currencies = new Dictionary<string, string>();

            GenerateCurrencyList();
        }

        public static string GetCurrencyName(string currencyCode)
        {
            return _currencies[currencyCode];
        }

        private static void GenerateCurrencyList()
        {
            CultureInfo[] cultureInfos = CultureInfo.GetCultures(CultureTypes.SpecificCultures);

            //var duplicates = cultureInfos.Where(x => x.IsNeutralCulture)
            //    .Select(x => new RegionInfo(x.LCID))
            //    .GroupBy(x => x.ISOCurrencySymbol)
            //    .Where(x => x.ToArray().Length > 1);

            foreach (CultureInfo cultureInfo in cultureInfos)
            {
                if (cultureInfo.IsNeutralCulture)
                    continue;

                RegionInfo region = new RegionInfo(cultureInfo.LCID);


                if (_currencies.ContainsKey(region.ISOCurrencySymbol))
                    continue;

                _currencies.Add(region.ISOCurrencySymbol, region.CurrencyEnglishName);

            }
        }
    }
}
