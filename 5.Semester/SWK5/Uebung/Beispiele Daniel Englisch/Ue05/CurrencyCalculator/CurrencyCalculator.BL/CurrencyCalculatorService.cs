using System;
using System.Collections.Generic;

namespace CurrencyCalculator.BL
{
    internal class CurrencyCalculatorService : ICurrencyCalculator
    {
        private class Entry
        {
            public String Name;      // long form of currency name
            public String Country;   // country currency is used in
            public double Rate;      // current rate of exchange

            public Entry(String name, String country, double rate)
            {
                this.Name = name;
                this.Country = country;
                this.Rate = rate;
            }
        }

        // currencyTable maps each currency string to an Entry object
        private IDictionary<String, Entry> currencyTable = new SortedDictionary<string, Entry>();

        public CurrencyCalculatorService()
        {
            // initialize currencyTable
            currencyTable.Add("USD", new Entry("Dollar", "USA", 1.158165));
            currencyTable.Add("AUD", new Entry("Dollar", "Australia", 1.51197462));
            currencyTable.Add("BRL", new Entry("Real", "Brazil", 3.79061319));
            currencyTable.Add("GBP", new Entry("Pound", "GB", 0.883380623));
            currencyTable.Add("CAD", new Entry("Dollar", "Canada", 1.49094361));
            currencyTable.Add("CNY", new Entry("Yuan", "China", 7.70502219));
            currencyTable.Add("DKK", new Entry("Krone", "Denmark", 7.44015957));
            currencyTable.Add("HKD", new Entry("Dollar", "Hong Kong", 9.03898384));
            currencyTable.Add("INR", new Entry("Rupee", "India", 75.3229058));
            currencyTable.Add("JPY", new Entry("Yen", "Japan", 131.834377));
            currencyTable.Add("MYR", new Entry("Ringgit", "Malysia", 4.91234953));
            currencyTable.Add("MXN", new Entry("Peso", "Mexico", 22.2045093));
            currencyTable.Add("EUR", new Entry("Euro", "Europe", 1.0));
        }

        private double EuroRate(String currencyString)
        {
            Entry e;
            if (currencyTable.TryGetValue(currencyString, out e))
                return e.Rate;
            else
                throw new ArgumentException("invalid currency " + currencyString);
        }

        public IEnumerable<CurrencyData> GetCurrencyData()
        {
            List<CurrencyData> data = new List<CurrencyData>(currencyTable.Count);
            foreach (KeyValuePair<string, Entry> pair in currencyTable)
                data.Add(new CurrencyData(pair.Key, pair.Value.Name, pair.Value.Country));
            return data;
        }

        public double RateOfExchange(string sourceCurrency, string targetCurrency)
        {
            return EuroRate(targetCurrency) / EuroRate(sourceCurrency);
        }

        public double Convert(double value, string sourceCurrency, string targetCurrency)
        {
            return value * RateOfExchange(sourceCurrency, targetCurrency);
        }

        public IEnumerable<RangeCurrencyData> MonthlyRatesOfExchange(
                                                string sourceCurrency, string targetCurrency,
                                                DateTime from, DateTime to)
        {
            DateTime fromDate = new DateTime(from.Year, from.Month, 1);
            DateTime toDate = new DateTime(to.Year, to.Month, 1);

            Random rand = new Random(fromDate.GetHashCode() + sourceCurrency.GetHashCode() +
                                     targetCurrency.GetHashCode());
            double rate = RateOfExchange(sourceCurrency, targetCurrency);

            var list = new List<RangeCurrencyData>();
            for (DateTime date = fromDate; date < toDate; date = date.AddMonths(1))
            {
                if (sourceCurrency == targetCurrency)
                    list.Add(new RangeCurrencyData
                    {
                        From = date,
                        To = date.AddMonths(1),
                        AverageRate = 1.0
                    });
                else
                    list.Add(new RangeCurrencyData
                    {
                        From = date,
                        To = date.AddMonths(1),
                        AverageRate = rate + 0.5 * rate * rand.NextDouble()
                    });
            }

            return list;
        }
    }
}
