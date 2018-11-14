using PersonAdmin.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PersonAdmin.DalInterface
{
    public interface IPersonDao
    {
        IEnumerable<Person> FindAll();

        Person FindById(int id);

        Task<bool> UpdateAsync(Person person);
    }
}
