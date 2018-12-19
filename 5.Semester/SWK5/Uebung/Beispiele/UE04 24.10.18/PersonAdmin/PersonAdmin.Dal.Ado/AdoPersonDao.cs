using Common.Dal.Ado;
using PersonAdmin.DalInterface;
using PersonAdmin.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace PersonAdmin.Dal.Ado
{
    public class AdoPersonDao : IPersonDao
    {
        private readonly AdoTemplate template;

        public AdoPersonDao(IConnectionFactory factory)
        {
            this.template = new AdoTemplate(factory);
        }

        private static Person MapRow(IDataRecord row)
        {
            return new Person()
            {
                Id = (int)row["id"],
                FirstName = (string)row["first_name"],
                LastName = (string)row["last_name"],
                DateOfBirth = (DateTime)row["date_of_birth"]
            };
        }

        public IEnumerable<Person> FindAll()
        {
            return this.template.Query("select * from person", MapRow);
        }

        public Person FindById(int id)
        {
            return this.template.Query(
                "select * from person where id = @id",
                MapRow,
                new Parameter("@id", id)).SingleOrDefault();
        }

        public async Task<bool> UpdateAsync(Person person)
        {
            // mit @ bei string kann man mehrzeilig schreiben
            return await this.template.ExecuteAsync(@"update person
                                        set first_name=@fn,
                                            last_name =@ln,
                                            date_of_birth=@dob
                                        where id = @id",
                                        new Parameter("@id", person.Id),
                                        new Parameter("@fn", person.FirstName),
                                        new Parameter("@ln", person.LastName),
                                        new Parameter("@dob", person.DateOfBirth)) == 1;
        }
    }
}