using PersonAdmin.Dal.Interface;
using PersonAdmin.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonAdmin.Dal.Simple
{
    public class SimplePersonDao : IPersonDao
    {
        private static List<Person> persons = new List<Person>()
        {
            new Person()
            {
                Id = 1,
                FirstName = "Max",
                LastName = "Mustermann",
                DateOfBirth = DateTime.Now.AddYears(-20)
            },
            new Person()
            {
                Id = 2,
                FirstName = "Martina",
                LastName = "Musterfrau",
                DateOfBirth = DateTime.Now.AddYears(-30)
            }
        };

        public Task<IEnumerable<Person>> FindAllAsync()
        {
            return Task.FromResult<IEnumerable<Person>>(persons);
        }

        public Task<Person> FindByIdAsync(int id)
        {
            return Task.FromResult(persons.SingleOrDefault(person => person.Id == id));
        }

        public async Task<bool> UpdateAsync(Person person)
        {
            //return Task.Run(() =>
            //{
            //    var personToUpdate = FindById(person.Id);
            //    if (personToUpdate == null)
            //        return false;

            //    personToUpdate.FirstName = person.FirstName;
            //    personToUpdate.LastName = person.LastName;
            //    personToUpdate.DateOfBirth = person.DateOfBirth;

            //    return true;
            //});

            var personToUpdate = await FindByIdAsync(person.Id);
            if (personToUpdate == null)
                return false;

            personToUpdate.FirstName = person.FirstName;
            personToUpdate.LastName = person.LastName;
            personToUpdate.DateOfBirth = person.DateOfBirth;

            return true;
        }
    }
}
