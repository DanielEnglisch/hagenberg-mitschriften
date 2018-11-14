using LinqSamples.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            var customersByRevenue = from c in customers
                                     where c.Revenue > 1_000_000
                                     orderby c.Revenue descending
                                     select c;

            Print(customersByRevenue);

            Console.WriteLine("-----------------------------------------------");
            Console.WriteLine("Kunden mit A..");

            var customersWithA = from c in customers
                                 where c.Name.StartsWith("A")
                                 orderby c.Name
                                 select c;

            Print(customersWithA);

            Console.WriteLine("-----------------------------------------------");
            Console.WriteLine("Kunden in Österreich");

            var customersInAustria = from c in customers
                                 where c.Location.Country.Equals("Österreich")
                                 select c;

            Print(customersInAustria);

            Console.WriteLine("-----------------------------------------------");
            Console.WriteLine("Top 3 umsatzstärkste Kunden");


            var topThreeCustomers = (from c in customers
                                     orderby c.Revenue descending
                                     select c).Take(3);

            Print(topThreeCustomers);

            Console.WriteLine("-----------------------------------------------");
            Console.WriteLine("Durchschnittlicher Umsatz von A-Kunden");

            var averageRevenue = (from c in customers
                                  where c.Rating == Rating.A
                                  select c.Revenue).Average();

            Console.WriteLine($"Average:  + { averageRevenue: N2} Euro");

            Console.WriteLine("-----------------------------------------------");
            Console.WriteLine("Umsatz je Land");

            var revenuePerCountry = from c in customers
                                    group c by c.Location.Country into g
                                    select new
                                    {
                                        Country = g.Key,
                                        AverageRevenue = g.Average(c => c.Revenue)
                                    };


            foreach (var c in revenuePerCountry)
            {
                Console.WriteLine($"{c.Country}: {c.AverageRevenue:N2} Euro");
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
