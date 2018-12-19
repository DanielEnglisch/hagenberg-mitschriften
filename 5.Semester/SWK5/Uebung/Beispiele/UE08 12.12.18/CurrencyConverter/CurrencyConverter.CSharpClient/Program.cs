using CurrencyConverter.Domain;
using CurrencyConverter.Proxy;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace CurrencyConverter.CSharpClient
{
    class Program
    {
        private const string baseUri = "http://localhost:56114";
        private static readonly string converterServiceUri =
            $"{baseUri}/api/currencies";

        // async main is only available in a C# 7 minor version
        // can be set under Properties -> Build -> Advanced
        static async Task Main(string[] args)
        {
            //await TestRestApiAsync();
            await TestSwaggerClientAsync();
            Console.ReadLine();
        }

        private static async Task TestRestApiAsync()
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await client.GetAsync(converterServiceUri);
            response.EnsureSuccessStatusCode();

            //var body = await response.Content.ReadAsStringAsync();
            //Console.WriteLine(body);

            var currencies = await response.Content.ReadAsAsync<IEnumerable<Domain.CurrencyData>>();

            foreach (var currency in currencies)
            {
                Console.WriteLine(currency);
            }

            Console.WriteLine();

            response = await client.GetAsync(converterServiceUri + "/ATS");

            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                Console.WriteLine("Currency ATS not found");
            }
            else
            {
                var currency = await response.Content.ReadAsAsync<Domain.CurrencyData>();
                Console.WriteLine(currency);
            }
        }

        private static async Task TestSwaggerClientAsync()
        {
            var client = new HttpClient();
            var converterProxy = new ConverterProxy(client);

            foreach (var symbol in new[] { "USD", "test" })
            {
                try
                {
                    SwaggerResponse<Proxy.CurrencyData> data =
                        await converterProxy.GetBySymbolAsync(symbol);

                    Console.WriteLine($"{data.Result.Name} ({data.Result.Symbol}, {data.Result.Country}, {data.Result.EuroRate})");
                }
                catch (SwaggerException se)
                {
                    Console.WriteLine($"{se.StatusCode}: {se.Message}");
                }
            }
        }
    }
}
