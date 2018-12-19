using Common.Dal.Ado;
using PersonAdmin.Dal.Ado;
using PersonAdmin.Dal.Interface;
using PersonAdmin.Dal.Simple;
using PersonAdmin.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace PersonAdmin.Client
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //IPersonDao personDao = new SimplePersonDao();
            IPersonDao personDao = new AdoPersonDao(
                DefaultConnectionFactory.FromConfiguration("PersonDbConnection"));

            await TestFindAllAsync(personDao);
            Console.WriteLine();
            await TestFindByIdAsync(personDao);
            Console.WriteLine();
            await TestUpdateAsync(personDao);

            await TestTransactions(personDao);

            Console.WriteLine("Finished");

            Console.ReadLine();
        }

        private static async Task TestTransactions(IPersonDao personDao)
        {
            Console.WriteLine("Test Transactions");

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

            try
            {
                using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    await personDao.UpdateAsync(person1);
                    await personDao.UpdateAsync(person2);

                    transaction.Complete();
                }
            }
            catch (Exception)
            {                
            }

            await TestFindAllAsync(personDao);
        }

        public static async Task TestUpdateAsync(IPersonDao personDao)
        {
            Console.WriteLine("Test Update");

            var person = new Person()
            {
                Id = 1,
                FirstName = "Daniel",
                LastName = "Sklenitzka",
                DateOfBirth = new DateTime(1987, 6, 11)
            };

            await personDao.UpdateAsync(person);

            Console.WriteLine(await personDao.FindByIdAsync(person.Id));
        }

        public static async Task TestFindByIdAsync(IPersonDao personDao)
        {
            Console.WriteLine("Test FindById");

            Person person1 = await personDao.FindByIdAsync(1);
            Console.WriteLine($"FindById(1) -> {person1?.ToString()}");

            Person person2 = await personDao.FindByIdAsync(-1);
            Console.WriteLine($"FindById(-1) -> {person2?.ToString()}");
        }

        public static async Task TestFindAllAsync(IPersonDao personDao)
        {
            Console.WriteLine("Test FindAll");
            foreach (var person in await personDao.FindAllAsync())
            {
                Console.WriteLine($"{person.Id,5} | {person.FirstName,-10} | {person.LastName,-15} | {person.DateOfBirth,10:yyyy-MM-dd}");
            }
        }
    }
}
