using MySql.Data.MySqlClient;
using Sorschia.Data;
using Sorschia.DatabaseUtilities;
using System;
using System.Data;

namespace ElectronicRaffle.Data.Processes
{
    public sealed class TeacherExists : IDataProcess<bool>
    {
        #region Constructor
        public TeacherExists(string firstName, string middleName, string lastName)
        {
            if (string.IsNullOrWhiteSpace(firstName))
            {
                throw new ArgumentException("Cannot be null or white space.", nameof(firstName));
            }

            if (string.IsNullOrWhiteSpace(lastName))
            {
                throw new ArgumentException("Cannot be null or white space.", nameof(lastName));
            }

            FirstName = firstName;
            MiddleName = middleName;
            LastName = lastName;
            Utilities = new MySqlUtilities(Configuration.ConnectionString);
        }
        #endregion

        #region Properties
        private string FirstName;
        private string MiddleName;
        private string LastName;
        private MySqlUtilities Utilities;
        #endregion

        #region Helpers
        private MySqlCommand CreateCommand(MySqlConnection connection)
        {
            var command = new MySqlCommand("SELECT TeacherExists(@_FirstName, @_MiddleName, @_LastName);", connection);
            command.Parameters.AddWithValue("@_FirstName", FirstName);
            command.Parameters.AddWithValue("@_MiddleName", MiddleName);
            command.Parameters.AddWithValue("@_LastName", LastName);
            return command;
        }
        #endregion

        #region IDataProcess
        public void Dispose()
        {
            FirstName = null;
            MiddleName = null;
            LastName = null;
            Utilities = null;
        }

        internal bool Execute(MySqlConnection connection)
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
