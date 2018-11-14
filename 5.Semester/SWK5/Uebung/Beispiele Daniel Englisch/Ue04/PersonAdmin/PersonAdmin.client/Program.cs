using Common.Dal.Ado;
using PersonAdmin.Dal.Ado;
using PersonAdmin.Dal.Simple;
using PersonAdmin.DalInterface;
using PersonAdmin.Domain;
using System;
using System.Threading.Tasks;
using System.Transactions;

namespace PersonAdmin.client
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("-- Person Admin --");
            //IPersonDao personDao = new SimplePersonDao();
            IPersonDao personDao = new AdoPersonDao(DefaultConnectionFactory.FromConfiguration("PersonDbConnection"));
            TestAll(personDao);
            TestById(personDao);

            TestUpdate(personDao);

            TestTransaction(personDao);

            Console.ReadLine();
        }

        private static async Task TestTransaction(IPersonDao personDao)
        {
            var person1 = new Person
            {
                Id = 1,
                FirstName = "Janesch",
                LastName = "Killa",
                DateOfBirth = DateTime.Now
            };

            var person2 = new Person
            {
                Id = 2,
                FirstName = "Franz",
                LastName = "Hobla",
                DateOfBirth = DateTime.Now
            };

            // Start of a transaction
            using(var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                await personDao.UpdateAsync(person1);
                await personDao.UpdateAsync(person2);

                // Commit transaction
                transaction.Complete();
            }
            // When transaction goes out of scope without Complete(), changes are rolled back

            TestAll(personDao);
        }

        public static void TestAll(IPersonDao personDao)
        {
            Console.WriteLine("Test FindAll");
            foreach (var person in personDao.FindAll())
            {
                Console.WriteLine($" {person.Id,5} | {person.FirstName, -10} | {person.LastName, - 15} | {person.DateOfBirth, 10:yyyy-MM-dd} ");
            }
        }

        public static void TestById(IPersonDao personDao)
        {
            Console.WriteLine("Test FindById");

            Person person1 = personDao.FindById(1);
            Console.WriteLine($"FindById(1) -> {person1?.ToString()}");

            Person person2 = personDao.FindById(-1);
            Console.WriteLine($"FindById(-1) -> {person2?.ToString()}");
        }

        public async static Task TestUpdate(IPersonDao personDao)
        {
            Console.WriteLine("Test Update");

            var person = new Person
            {
                Id = 1,
                FirstName = "Daniel",
                LastName = "Englisch",
                DateOfBirth = new DateTime(1996, 12, 14)
            };

            await personDao.UpdateAsync(person);

            Console.WriteLine(personDao.FindById(person.Id));
                
        }
    }
}
