using MySql.Data.MySqlClient;
using Sorschia.Data;
using Sorschia.DatabaseUtilities;
using System;
using System.Data;

namespace ElectronicRaffle.Data.Processes
{
    public sealed class InsertSchool : IDataProcess<School>
    {
        #region Constructor
        public InsertSchool(School school)
        {
            School = school ?? throw new ArgumentNullException("Cannot be null.", nameof(School));
            Utilities = new MySqlUtilities(Configuration.ConnectionString);
        }
        #endregion

        #region Properties
        private School School;
        private MySqlUtilities Utilities;
        #endregion

        #region Helpers
        private MySqlCommand CreateCommand(MySqlConnection connection, MySqlTransaction transaction)
        {
            var command = Utilities.CreateProcedureCommand("InsertSchool", connection);
            command.Parameters.Add(Utilities.CreateOutParameter("@_Id"));
            command.Parameters.AddWithValue("@_Name", School.Name);
            return command;
        }

        private void GetOutParameters(MySqlCommand command)
        {
            var id = Convert.ToUInt64(command.Parameters["@_Id"].Value);
            var name = School.Name;

            School = new School(id)
            {
                Name = name
            };
        }
        #endregion

        #region IDataProcess
        public void Dispose()
        {
            Utilities = null;
        }

        internal School Execute(MySqlConnection connection, MySqlTransaction transaction)
        {
            using (var command = CreateCommand(connection, transaction))
            {
                command.ExecuteNonQuery();
                GetOutParameters(command);
                return School;
            }
        }

        public School Execute()
        {
            using (var connection = Utilities.EstablishConnection())
            {
                if (connection.State == ConnectionState.Open)
                {
                    using (var transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            Execute(connection, transaction);
                            transaction.Commit();
                            return School;
                        }
                        catch (Exception)
                        {
                            transaction.Rollback();
                            throw;
                        }
                    }
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
