using PersonAdmin.DalInterface;
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
                Id = 2,
                        FirstName = "Andi",
                        LastName = "MusterMann",
                        DateOfBirth = DateTime.Now.AddYears(-30)
            }
        };

        public IEnumerable<Person> FindAll()
        {
            return persons;
        }

        public Person FindById(int id)
        {
            return persons.Where(person => person.Id == id).FirstOrDefault();
        }

        public Task<bool> UpdateAsync(Person person)
        {
            var personToUpdate = this.FindById(person.Id);
            if(personToUpdate == null)
            {
                return Task.FromResult(false);
            }

            personToUpdate.FirstName = person.FirstName;
            personToUpdate.LastName = person.LastName;
            personToUpdate.DateOfBirth = person.DateOfBirth;

            return Task.FromResult(true);
        }

        
    }
}
