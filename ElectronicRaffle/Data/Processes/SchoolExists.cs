using MySql.Data.MySqlClient;
using Sorschia.Data;
using Sorschia.DatabaseUtilities;
using System;
using System.Data;

namespace ElectronicRaffle.Data.Processes
{
    public sealed class SchoolExists : IDataProcess<bool>
    {
        #region Constructor
        public SchoolExists(string schoolName)
        {
            if (string.IsNullOrWhiteSpace(schoolName))
            {
                throw new ArgumentException("Cannot be null or white space.", nameof(SchoolName));
            }

            SchoolName = schoolName;
            Utilities = new MySqlUtilities(Configuration.ConnectionString);
        }
        #endregion

        #region Properties
        private string SchoolName;
        private MySqlUtilities Utilities;
        #endregion

        #region Helpers
        private MySqlCommand CreateCommand(MySqlConnection connection)
        {
            var command = new MySqlCommand("SELECT SchoolExists(@_SchoolName);", connection);
            command.Parameters.AddWithValue("@_SchoolName", SchoolName);
            return command;
        }
        #endregion

        #region IDataProcess
        public void Dispose()
        {
            SchoolName = null;
            Utilities = null;
        }

        public bool Execute(MySqlConnection connection)
        {
            using (var command = CreateCommand(connection))
            {
                return Convert.ToBoolean(command.ExecuteScalar());
            }
        }

        public bool Execute()
        {
            using (var connection = Utilities.EstablishConnection())
            {
                if (connection.State == ConnectionState.Open)
                {
                    return Execute(connection);
                }
                else
                {
                    return false;
                }
            }
        } 
        #endregion
    }
}
