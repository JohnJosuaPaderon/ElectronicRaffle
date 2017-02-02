using ElectronicRaffle.Data.Collections;
using ElectronicRaffle.Data.Processes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicRaffle.Data.Repositories
{
    public static class TeacherRepository
    {
        static TeacherRepository()
        {
            Teachers = new TeacherCollection();
        }

        private static TeacherCollection Teachers { get; }

        private static Teacher Manage(Teacher teacher)
        {
            Teachers.AddOrUpdate(teacher);
            return teacher;
        }

        private static IEnumerable<Teacher> Manage(IEnumerable<Teacher> teacherEnumerable)
        {
            if (teacherEnumerable != null && teacherEnumerable.Any())
            {
                Parallel.ForEach(teacherEnumerable, teacher => Manage(teacher));
            }

            return teacherEnumerable;
        }

        public static Teacher Insert(Teacher teacher)
        {
            using (var process = new InsertTeacher(teacher))
            {
                return Manage(process.Execute());
            }
        }

        public static Teacher GenerateRandomPick()
        {
            using (var process = new GenerateRandomTeacherPick())
            {
                return Manage(process.Execute());
            }
        }

        public static bool Exists(string firstName, string middleName, string lastName)
        {
            if (!string.IsNullOrWhiteSpace(firstName) && !string.IsNullOrWhiteSpace(lastName))
            {
                var exists = Teachers.Any(teacher => teacher.FirstName == firstName && teacher.MiddleName == middleName && teacher.LastName == lastName);

                if (!exists)
                {
                    using (var process = new TeacherExists(firstName, middleName, lastName))
                    {
                        exists = process.Execute();
                    }
                }

                return exists;
            }
            else
            {
                return false;
            }
        }
    }
}
