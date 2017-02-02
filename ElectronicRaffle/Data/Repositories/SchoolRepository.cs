using ElectronicRaffle.Data.Collections;
using ElectronicRaffle.Data.Processes;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElectronicRaffle.Data.Repositories
{
    public static class SchoolRepository
    {
        static SchoolRepository()
        {
            Schools = new SchoolCollection();
        }

        private static SchoolCollection Schools { get; }

        private static School Manage(School school)
        {
            Schools.AddOrUpdate(school);
            return school;
        }

        private static IEnumerable<School> Manage(IEnumerable<School> schoolEnumerable)
        {
            if (schoolEnumerable != null && schoolEnumerable.Any())
            {
                Parallel.ForEach(schoolEnumerable, (school) => Manage(school));
            }

            return schoolEnumerable;
        }

        public static bool Exists(string schoolName)
        {
            if (!string.IsNullOrWhiteSpace(schoolName))
            {
                var exists = Schools.Any(school => school.Name == schoolName);

                if (!exists)
                {
                    using (var process = new SchoolExists(schoolName))
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

        public static School Insert(School school)
        {
            using (var process = new InsertSchool(school))
            {
                return Manage(process.Execute());
            }
        }

        public static School GetById(ulong schoolId)
        {
            if (schoolId > 0)
            {
                var school = Schools.GetById(schoolId);

                if (school == null)
                {
                    using (var process = new GetSchoolById(schoolId))
                    {
                        school = Manage(process.Execute());
                    }
                }

                return school;
            }
            else
            {
                return null;
            }
        }

        public static School[] GetArray()
        {
            using (var process = new GetSchoolArray())
            {
                return Manage(process.Execute())?.ToArray();
            }
        }

        public static async Task<School[]> GetArrayAsync()
        {
            using (var process = new GetSchoolArray())
            {
                return Manage(await process.ExecuteAsync())?.ToArray();
            }
        }
    }
}
