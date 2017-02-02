using ElectronicRaffle.Data.Repositories;
using MySql.Data.MySqlClient;
using Sorschia.Data;
using Sorschia.DatabaseUtilities;
using System.Data;
using System.Data.Common;

namespace ElectronicRaffle.Data.Processes
{
    public sealed class GenerateRandomTeacherPick : IDataProcess<Teacher>
    {
        #region Constructor
        public GenerateRandomTeacherPick()
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
            var command = Utilities.CreateProcedureCommand("GenerateRandomTeacherPick", connection);
            return command;
        }

        private Teacher FromReader(DbDataReader reader)
        {
            var id = reader.GetUInt64("Id");

            if (id > 0)
            {
                return new Teacher(id)
                {
                    FirstName = reader.GetString("FirstName"),
                    MiddleName = reader.GetString("MiddleName"),
                    LastName = reader.GetString("LastName"),
                    ContactNumber = reader.GetString("ContactNumber"),
                    School = SchoolRepository.GetById(reader.GetUInt64("SchoolId")),
                    AlreadyPicked = false
                };
            }
            else
            {
                return null;
            }
        }
        #endregion

        #region IDataProcess
        public void Dispose()
        {
            Utilities = null;
        }

        internal Teacher Execute(MySqlConnection connection)
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

        public Teacher Execute()
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
        #endregion
    }
}
