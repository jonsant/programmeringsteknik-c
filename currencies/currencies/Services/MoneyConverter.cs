using System;
using System.Collections.Generic;
using System.IO;
using currencies.Models;

namespace currencies.Services
{
    class MoneyConverter
    {
        private readonly string _targetCurrency;

        private readonly IDictionary<string, ExchangeRate> _exchangeRates;
        public MoneyConverter(string filePath, string targetCurrency)
        {
            _exchangeRates = new Dictionary<string, ExchangeRate>(StringComparer.OrdinalIgnoreCase);
            _targetCurrency = targetCurrency;

            ReadFile(filePath);
        }

        public Money ConvertToTargetCurrency(Money money)
        {
            ExchangeRate rate = _exchangeRates[money.Currency];

            decimal convertedAmount = money.Amount * rate.ConversionRate;

            return new Money(convertedAmount, _targetCurrency);
        }

        public Money ConvertFromTargetCurrency(decimal amount, string currency)
        {
            ExchangeRate rate = _exchangeRates[currency];

            decimal convertedAmount = amount / rate.ConversionRate;

            return new Money(convertedAmount, currency);
        }

        private void ReadFile(string filePath)
        {

            using (Stream fileStream = File.OpenRead(filePath))
            {
                using (StreamReader reader = new StreamReader(fileStream))
                {
                    string header = reader.ReadLine();

                    while (!reader.EndOfStream)
                    {
                        try
                        {

                            string line = reader.ReadLine();
                            ExchangeRate rate = ExchangeRateParser.Parse(line);

                            _exchangeRates.Add(rate.Currency, rate);

                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }

                    }

                }
            }
        }
    }
}
