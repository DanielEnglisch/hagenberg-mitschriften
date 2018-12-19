using PersonAdmin.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PersonAdmin.DalInterface
{
    public interface IPersonDao
    {
        Task<IEnumerable<Person>> FindAllAsync();

        Task<Person> FindByIdAsync(int id);

        Task<bool> UpdateAsync(Person person);
    }
}