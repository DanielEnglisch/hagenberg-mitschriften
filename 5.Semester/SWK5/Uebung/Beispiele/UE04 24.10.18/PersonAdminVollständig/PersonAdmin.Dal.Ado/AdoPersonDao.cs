using Common.Dal.Ado;
using PersonAdmin.Dal.Interface;
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

        public AdoPersonDao(IConnectionFactory connectionFactory)
        {
            this.template = new AdoTemplate(connectionFactory);
        }

        private Person MapRow(IDataRecord row)
        {
            return new Person()
            {
                Id = (int)row["id"],
                FirstName = (string)row["first_name"],
                LastName = (string)row["last_name"],
                DateOfBirth = (DateTime)row["date_of_birth"]
            };
        }

        public async Task<IEnumerable<Person>> FindAllAsync()
        {
            return await this.template.QueryAsync("select * from person", MapRow);
        }

        public async Task<Person> FindByIdAsync(int id)
        {
            var result = await this.template.QueryAsync(
                "select * from person where id = @id",
                MapRow,
                new Parameter("@id", id));

            return result.SingleOrDefault();
        }

        public async Task<bool> UpdateAsync(Person person)
        {
            return await this.template.ExecuteAsync(@"update person
                                                      set first_name=@fn,
                                                          last_name = @ln,
                                                          date_of_birth = @dob
                                                      where id=@id",
                                                      new Parameter("@id", person.Id),
                                                      new Parameter("@fn", person.FirstName),
                                                      new Parameter("@ln", person.LastName),
                                                      new Parameter("@dob", person.DateOfBirth)) == 1;
        }
    }
}
