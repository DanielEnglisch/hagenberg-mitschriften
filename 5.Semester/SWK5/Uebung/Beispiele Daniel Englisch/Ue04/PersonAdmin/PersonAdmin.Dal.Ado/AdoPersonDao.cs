using PersonAdmin.DalInterface;
using PersonAdmin.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.Common;
using System.Data;
using Common.Dal.Ado;

namespace PersonAdmin.Dal.Ado
{
    public class AdoPersonDao : IPersonDao
    {
        private readonly AdoTemplate template;

        public AdoPersonDao(IConnectionFactory connetionFactory)
        {
            this.template = new AdoTemplate(connetionFactory);
        }

        // Maps a generic data record to a person
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
            return this.template.Query("SELECT * FROM person", MapRow);
        }

        public Person FindById(int id)
        {
            return this.template.Query("SELECT * FROM PERSON WHERE ID = @id", MapRow,
                    new Parameter{
                        Name = "@id",
                        Value = id
                        }

                ).SingleOrDefault();
        }

        public async Task<bool> UpdateAsync(Person person)
        {
            return await this.template.ExecuteAsync(@"UPDATE person set first_name=@fn, last_name=@ln, date_of_birth=@dob
                                   where id=@id",
                                   new Parameter("fn", person.FirstName),
                                   new Parameter("ln", person.LastName),
                                   new Parameter("dob", person.DateOfBirth),
                                   new Parameter("id", person.Id)
            ) == 1;
                
        }

        
    }
}
