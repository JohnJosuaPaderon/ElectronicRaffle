using MySql.Data.MySqlClient;
using Sorschia.Data;
using Sorschia.DatabaseUtilities;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;

namespace ElectronicRaffle.Data.Processes
{
    public sealed class GetSchoolArray : IDataProcess<School[]>
    {
        #region Constructor
        public GetSchoolArray()
        {
            Utilities = new MySqlUtilities(Configuration.ConnectionString);
        }
        #endregion

        #region Properties
        private MySqlUtilities Utilities;
        #endregion

        #region Helpers
        private MySqlCommand CreateCommand(MySqlConnection connection)
        {
            var command = Utilities.CreateProcedureCommand("GetSchoolList", connection);
            return command;
        }

        private School FromReader(DbDataReader reader)
        {
            return new School(reader.GetUInt64("Id"))
            {
                Name = reader.GetString("Name")
            };
        }
        #endregion

        #region IDataProcess
        public void Dispose()
        {
            Utilities = null;
        }

        internal School[] Execute(MySqlConnection connection)
        {
            using (var command = CreateCommand(connection))
            {
                using (var reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        var schoolList = new List<School>();

                        while (reader.Read())
                        {
                            schoolList.Add(FromReader(reader));
                        }

                        return schoolList.ToArray();
                    }

                    else
                    {
                        return null;
                    }
                }
            }
        }

        internal async Task<School[]> ExecuteAsync(MySqlConnection connection)
        {
            using (var command = CreateCommand(connection))
            {
                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (reader.HasRows)
                    {
                        var schoolList = new List<School>();

                        while (await reader.ReadAsync())
                        {
                            schoolList.Add(FromReader(reader));
                        }

                        return schoolList.ToArray();
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }

        public School[] Execute()
        {
            using (var connection = Utilities.EstablishConnection())
            {
                if (connection.State == ConnectionState.Open)
                {
                    return Execute(connection);
                }
                else
                {
                    return null;
                }
            }
        } 

        public async Task<School[]> ExecuteAsync()
        {
            using (var connection = await Utilities.EstablishConnectionAsync())
            {
                if (connection.State == ConnectionState.Open)
                {
                    return await ExecuteAsync(connection);
                }
                else
                {
                    return null;
                }
            }
        }
        #endregion
    }
}
