using Sorschia.Data;
using System.Collections.Generic;
using System.Linq;
using System.Collections;

namespace ElectronicRaffle.Data.Collections
{
    public class SchoolCollection : IDataCollection<School, ulong>
    {
        #region Constructor
        public SchoolCollection()
        {
            SchoolList = new List<School>();
        }
        #endregion

        #region Properties
        private List<School> SchoolList { get; }
        #endregion

        #region IDataCollection
        public School this[int index]
        {
            get
            {
                var tempSchoolList = new List<School>(SchoolList);
                if (index >= 0 && tempSchoolList.Any())
                {
                    return SchoolList[index];
                }
                else
                {
                    return null;
                }
            }

            set
            {
                var tempSchoolList = new List<School>(SchoolList);
                if (index >= 0 && tempSchoolList.Any())
                {
                    SchoolList[index] = value;
                }
            }
        }

        public int Count
        {
            get
            {
                return SchoolList.Count();
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return false;
            }
        }

        public void Add(School item)
        {
            if (item != null)
            {
                SchoolList.Add(item);
            }
        }

        public void AddOrUpdate(School item)
        {
            if (item != null)
            {
                if (Contains(item))
                {
                    var index = SchoolList.IndexOf(item);
                    SchoolList[index] = item;
                }
                else
                {
                    SchoolList.Add(item);
                }
            }
        }

        public void Clear()
        {
            SchoolList.Clear();
        }

        public bool Contains(School item)
        {
            lock (SchoolList)
            {
                var tempSchoolList = new List<School>(SchoolList);
                if (item != null && tempSchoolList.Any())
                {
                    return SchoolList.Contains(item);
                }
                else
                {
                    return false;
                } 
            }
        }

        public void CopyTo(School[] array, int arrayIndex)
        {
            SchoolList.CopyTo(array, arrayIndex);
        }

        public School GetById(ulong id)
        {
            var tempSchoolList = new List<School>(SchoolList);
            if (id > 0 && tempSchoolList.Any())
            {
                return SchoolList.FirstOrDefault(school => school.Id == id);
            }
            else
            {
                return null;
            }
        }

        public IEnumerator<School> GetEnumerator()
        {
            return SchoolList.GetEnumerator();
        }

        public bool Remove(School item)
        {
            if (item != null)
            {
                return SchoolList.Remove(item);
            }
            else
            {
                return false;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        } 
        #endregion
    }
}
