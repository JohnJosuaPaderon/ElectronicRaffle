using MySql.Data.MySqlClient;
using Sorschia.Data;
using Sorschia.DatabaseUtilities;
using System;
using System.Data;

namespace ElectronicRaffle.Data.Processes
{
    public sealed class InsertTeacher : IDataProcess<Teacher>
    {
        #region Constructor
        public InsertTeacher(Teacher teacher)
        {
            if (teacher == null)
            {
                throw new ArgumentNullException(nameof(teacher));
            }

            Teacher = teacher;
            Utilities = new MySqlUtilities(Configuration.ConnectionString);
        }
        #endregion

        #region Properties
        private Teacher Teacher;
        private MySqlUtilities Utilities;
        #endregion

        #region Helpers
        private MySqlCommand CreateCommand(MySqlConnection connection, MySqlTransaction transaction)
        {
            var command = Utilities.CreateProcedureCommand("InsertTeacher", connection, transaction);
            command.Parameters.Add(Utilities.CreateOutParameter("@_Id"));
            command.Parameters.AddWithValue("@_FirstName", Teacher.FirstName);
            command.Parameters.AddWithValue("@_MiddleName", Teacher.MiddleName);
            command.Parameters.AddWithValue("@_LastName", Teacher.LastName);
            command.Parameters.AddWithValue("@_ContactNumber", Teacher.ContactNumber);
            command.Parameters.AddWithValue("@_SchoolId", Teacher.School.Id);
            return command;
        }

        private void GetOutParameters(MySqlCommand command)
        {
            var id = Convert.ToUInt64(command.Parameters["@_Id"].Value);
            var firstName = Teacher.FirstName;
            var middleName = Teacher.MiddleName;
            var lastName = Teacher.LastName;
            var contactNumber = Teacher.ContactNumber;
            var school = Teacher.School;

            Teacher = new Teacher(id)
            {
                FirstName = firstName,
                MiddleName = middleName,
                LastName = lastName,
                ContactNumber = contactNumber,
                School = school
            };
        }
        #endregion

        #region IDataProcess
        public void Dispose()
        {
            Utilities = null;
        }

        internal Teacher Execute(MySqlConnection connection, MySqlTransaction transaction)
        {
            using (var command = CreateCommand(connection, transaction))
            {
                command.ExecuteNonQuery();
                GetOutParameters(command);
                return Teacher;
            }
        }

        public Teacher Execute()
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
                            return Teacher;
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
