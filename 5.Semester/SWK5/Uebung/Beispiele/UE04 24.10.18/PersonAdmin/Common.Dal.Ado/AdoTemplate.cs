using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;

namespace Common.Dal.Ado
{
    public class AdoTemplate
    {
        private readonly IConnectionFactory connectionFactory;

        public AdoTemplate(IConnectionFactory connectionFactory)
        {
            this.connectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));
        }

        public IEnumerable<T> Query<T>(string sql, RowMapper<T> rowMapper, params Parameter[] parameters)
        {
            // mit using wird connection automatisch geschlossen
            using (DbConnection connection = connectionFactory.CreateConnection())
            {
                // durch IConnectionFactory wird bei CreateConnection die Verbindung schon geöffnet
                // Connectionstring angeben und Datenbank Verbindung öffnen
                // connection.ConnectionString = connectionFactory.ConnectionString;
                // connection.Open();

                // sql commands
                using (DbCommand command = connection.CreateCommand())
                {
                    command.CommandText = sql;

                    AddParameters(command, parameters);

                    var items = new List<T>();

                    // IDataReader ist vergleichbar mit Iterator über die Zeilen
                    using (IDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Call delegate
                            // IDataReader leitet von IDataRecord ab
                            // Mit IDataRecord kann nicht zur nächsten Zeile gewechselt werden
                            items.Add(rowMapper(reader));
                        }
                    }
                    return items;
                }
            }
        }

        public async Task<int> ExecuteAsync(string sql, params Parameter[] parameters)
        {
            using (DbConnection connection = connectionFactory.CreateConnection())
            using (DbCommand command = connection.CreateCommand())
            {
                command.CommandText = sql;
                AddParameters(command, parameters);

                return await command.ExecuteNonQueryAsync();
            }
        }

        private void AddParameters(DbCommand command, Parameter[] parameters)
        {
            foreach (var parameter in parameters)
            {
                DbParameter dbParameter = command.CreateParameter();
                dbParameter.ParameterName = parameter.Name;
                dbParameter.Value = parameter.Value;

                command.Parameters.Add(dbParameter);
            }
        }
    }
}