using MySql.Data.MySqlClient;
using Sorschia.Data;
using Sorschia.DatabaseUtilities;
using System;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;

namespace ElectronicRaffle.Data.Processes
{
    public sealed class GetSchoolById : IDataProcess<School>
    {
        #region Constructor
        public GetSchoolById(ulong schoolId)
        {
            if (schoolId == 0)
            {
                throw new ArgumentException("Cannot be zero.", nameof(SchoolId));
            }

            SchoolId = schoolId;
            Utilities = new MySqlUtilities(Configuration.ConnectionString);
        }
        #endregion

        #region Properties
        private ulong SchoolId;
        private MySqlUtilities Utilities;
        #endregion

        #region Helpers
        private MySqlCommand CreateCommand(MySqlConnection connection)
        {
            var command = Utilities.CreateProcedureCommand("GetSchoolById", connection);
            command.Parameters.AddWithValue("@_SchoolId", SchoolId);
            return command;
        }

        private School FromReader(DbDataReader reader)
        {
            return new School(SchoolId)
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

        internal School Execute(MySqlConnection connection)
        {
            using (var command = CreateCommand(connection))
            {
                using (var reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        reader.Read();
                        return FromReader(reader);
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }

        internal async Task<School> ExecuteAsync(MySqlConnection connection)
        {
            using (var command = CreateCommand(connection))
            {
                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (reader.HasRows)
                    {
                        await reader.ReadAsync();
                        return FromReader(reader);
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }

        public School Execute()
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

        public async Task<School> ExecuteAsync()
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
