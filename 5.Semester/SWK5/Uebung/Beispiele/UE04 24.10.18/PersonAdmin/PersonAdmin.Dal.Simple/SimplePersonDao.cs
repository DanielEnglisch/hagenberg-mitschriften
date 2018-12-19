using PersonAdmin.DalInterface;
using PersonAdmin.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonAdmin.Dal.Simple
{
    public class SimplePersonDao : IPersonDao
    {
        private static List<Person> persons = new List<Person>
        {
            new Person()
            {
                Id = 1,
                FirstName = "Max",
                LastName = "MusterMann",
                DateOfBirth = DateTime.Now.AddYears(-20)
            },
            new Person()
            {
                Id = 1,
                        FirstName = "Andi",
                        LastName = "MusterMann",
                        DateOfBirth = DateTime.Now.AddYears(-30)
            }
        };

        public Task<IEnumerable<Person>> FindAll()
        {
            return Tasks.FromResult<IEnumerable<Person>(persons);
        }

        public Task<Person> FindById(int id)
        {
            return persons.Where(person => person.Id == id).FirstOrDefault();
        }

        public Task<bool> UpdateAsync(Person person)
        {
            /*
            return Task.Run(() =>
            {
                var personToUpdate = FindById(person.Id);
                if (person == null) return false;

                personToUpdate.FirstName = person.FirstName;
                personToUpdate.LastName = person.LastName;
                personToUpdate.DateOfBirth = person.DateOfBirth;

                return true;
            )};*/

            var personToUpdate = FindById(person.Id);
            if (person == null) return Task.FromResult(false);

            personToUpdate.FirstName = person.FirstName;
            personToUpdate.LastName = person.LastName;
            personToUpdate.DateOfBirth = person.DateOfBirth;

            // mit task.fromresult wird ein task erzeugt der nicht startet und ein Ergebnis hat
            return Task.FromResult(true);
        }
    }
}