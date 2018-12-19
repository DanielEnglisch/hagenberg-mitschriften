using LinqSamples.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LinqSamples
{
    class Program
    {
        static void Main(string[] args)
        {
            var repository = new CustomerRepository();
            var customers = repository.GetCustomers();

            Console.WriteLine("-----------------------------------------------");
            Console.WriteLine("Kunden mit > 1 Mio Jahresumsatz");

            var customerByRevenue = from c in customers
                                    where c.Revenue > 1_000_000
                                    orderby c.Revenue descending
                                    select c;

            Print(customerByRevenue);

            Console.WriteLine("-----------------------------------------------");
            Console.WriteLine("Kunden mit A..");

            var customerWithA = from c in customers
                                where c.Name.StartsWith("A")
                                orderby c.Name
                                select c;
            Print(customerWithA);

            Console.WriteLine("-----------------------------------------------");
            Console.WriteLine("Kunden in Österreich");

            var customersInAustria = from c in customers
                                     where c.Location.Country == "Österreich"
                                     select c;
            Print(customersInAustria);

            Console.WriteLine("-----------------------------------------------");
            Console.WriteLine("Top 3 umsatzstärkste Kunden");

            var customersTop3 = (from c in customers
                                orderby c.Revenue descending
                                select c).Take(3);
            Print(customersTop3);

            Console.WriteLine("-----------------------------------------------");
            Console.WriteLine("Durchschnittlicher Umsatz von A-Kunden");

            var avg = (from c in customers
                       where c.Rating == Rating.A
                       select c.Revenue).Average();
            Console.WriteLine($"Avg Rating A: {avg} EURO");

            Console.WriteLine("-----------------------------------------------");
            Console.WriteLine("Umsatz je Land");

            var revenuePerCountry = from c in customers
                                    group c by c.Location.Country into g
                                    select new
                                    {
                                        Country = g.Key,
                                        AverageRev = g.Average(c => c.Revenue)
                                    };

            // var is needed since the return is an IEnumerable of an anonymus type
            foreach (var c in revenuePerCountry)
            {
                Console.WriteLine($"Country: {c.Country}, revenue: {c.AverageRev}");
            }

            Console.WriteLine("Finished. Press any key.");
            Console.ReadKey();
        }

        private static void Print(IEnumerable<Customer> customers)
        {
            foreach (var customer in customers)
            {
                Console.WriteLine(customer);
            }
        }
    }
}
