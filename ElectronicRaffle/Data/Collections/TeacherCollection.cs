using Sorschia.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;

namespace ElectronicRaffle.Data.Collections
{
    public class TeacherCollection : IDataCollection<Teacher, ulong>
    {
        #region Constructors
        public TeacherCollection()
        {
            TeacherList = new List<Teacher>();
            Dependent = false;
            School = null;
        }

        internal TeacherCollection(School school)
        {
            TeacherList = new List<Teacher>();
            Dependent = true;
            School = school ?? throw new ArgumentNullException(nameof(School));
        }
        #endregion

        #region Properties
        private List<Teacher> TeacherList { get; }
        private bool Dependent { get; }
        private School School { get; }
        #endregion

        #region IDataCollection
        public Teacher this[int index]
        {
            get
            {
                if (index >= 0 && TeacherList.Any())
                {
                    return TeacherList[index];
                }
                else
                {
                    return null;
                }
            }

            set
            {
                if (index >= 0 && TeacherList.Any())
                {
                    if (Dependent && value.School != School)
                    {
                        value.School = School;
                    }

                    TeacherList[index] = value;
                }
            }
        }

        public int Count
        {
            get
            {
                return TeacherList.Count;
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return false;
            }
        }

        public void Add(Teacher item)
        {
            if (item != null)
            {
                if (Dependent && item.School != School)
                {
                    item.School = School;
                }

                TeacherList.Add(item);
            }
        }

        public void AddOrUpdate(Teacher item)
        {
            if (item != null)
            {
                if (Dependent && item.School != School)
                {
                    item.School = School;
                }

                if (Contains(item))
                {
                    var index = TeacherList.IndexOf(item);
                    TeacherList[index] = item;
                }
                else
                {
                    TeacherList.Add(item);
                }
            }
        }

        public void Clear()
        {
            TeacherList.Clear();
        }

        public bool Contains(Teacher item)
        {
            if (item != null && TeacherList.Any())
            {
                return TeacherList.Contains(item);
            }
            else
            {
                return false;
            }
        }

        public void CopyTo(Teacher[] array, int arrayIndex)
        {
            TeacherList.CopyTo(array, arrayIndex);
        }

        public Teacher GetById(ulong id)
        {
            if (id > 0 && TeacherList.Any())
            {
                return TeacherList.FirstOrDefault(teacher => teacher.Id == id);
            }
            else
            {
                return null;
            }
        }

        public IEnumerator<Teacher> GetEnumerator()
        {
            return TeacherList.GetEnumerator();
        }

        public bool Remove(Teacher item)
        {
            if (item != null && TeacherList.Any())
            {
                return TeacherList.Remove(item);
            }
            else
            {
                return false;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return TeacherList.GetEnumerator();
        } 
        #endregion
    }
}
