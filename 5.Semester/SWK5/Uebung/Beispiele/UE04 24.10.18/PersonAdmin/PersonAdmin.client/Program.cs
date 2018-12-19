using Common.Dal.Ado;
using PersonAdmin.Dal.Ado;
using PersonAdmin.DalInterface;
using PersonAdmin.Domain;
using System;
using System.Threading.Tasks;
using System.Transactions;

namespace PersonAdmin.client
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            Console.WriteLine("-- Person Admin --");
            //IPersonDao personDao = new SimplePersonDao();
            IPersonDao personDao = new AdoPersonDao(DefaultConnectionFactory.FromConfiguration("PersonDbConnection"));

            TestAll(personDao);
            Console.WriteLine();
            TestFindById(personDao);
            Console.WriteLine();
            await TestUpdate(personDao);

            Console.WriteLine();
            await TestTransactions(personDao);

            Console.WriteLine("Finished");
            Console.ReadLine();
        }

        // async methoden müssen entweder void oder Task<T> als Rückgabewert haben
        // async void vermeiden, da bei diesem Beispiel Finished vor TestUpdate Ausgabe rausgeschrieben wird
        // dadurch sieht es für den Benutzer aus als ob TestUpdate bereits abgeschlossen ist, was nicht der Fall ist!
        // mit Task als Rückgabewert kann wieder das keyword await verwendet werden.
        private async static Task TestUpdate(IPersonDao personDao)
        {
            Console.WriteLine("> Test Update <");

            var person = new Person()
            {
                Id = 1,
                FirstName = "Andreas",
                LastName = "Android",
                DateOfBirth = new DateTime(1996, 1, 17)
            };

            await personDao.UpdateAsync(person);

            Console.WriteLine(personDao.FindById(person.Id));
        }

        private static void TestFindById(IPersonDao personDao)
        {
            Console.WriteLine("> Test FindById <");
            Person person1 = personDao.FindById(1);
            Console.WriteLine($"FindById(1) -> {person1?.ToString()}");

            Person person2 = personDao.FindById(-1);
            Console.WriteLine($"FindById(-1) -> {person2?.ToString()}");
        }

        public static void TestAll(IPersonDao personDao)
        {
            Console.WriteLine("> Test FindAll <");
            foreach (var person in personDao.FindAll())
            {
                Console.WriteLine($" {person.Id,5} | {person.FirstName,-10} | {person.LastName,-15} | {person.DateOfBirth,10:yyyy-MM-dd} ");
            }
        }

        private static async Task TestTransactions(IPersonDao personDao)
        {
            var person1 = new Person()
            {
                Id = 1,
                FirstName = "After",
                LastName = "Transaction",
                DateOfBirth = DateTime.Now
            };

            var person2 = new Person()
            {
                Id = 2,
                FirstName = "After",
                LastName = "Transaction",
                DateOfBirth = DateTime.Now
            };

            // mit einer transaktion können inkonsistente Zustände vermieden werden
            using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                await personDao.UpdateAsync(person1);
                await personDao.UpdateAsync(person2);

                // falls eine exception vor Complete auftritt
                // wird die Transaktion nicht commited
                // in der Dispose Methode wird dann die transaction commited
                transaction.Complete();
            }

            TestAll(personDao);
        }
    }
}