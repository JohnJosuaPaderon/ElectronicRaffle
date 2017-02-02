using ElectronicRaffle.Data.Collections;
using Sorschia.Data;

namespace ElectronicRaffle.Data
{
    public class School : DataBase<ulong>
    {
        public School(ulong id) : base(id)
        {
            Teachers = new TeacherCollection(this);
        }

        private string _Name;
        
        public string Name
        {
            get { return _Name; }
            set
            {
                if (_Name != value)
                {
                    _Name = value;
                    OnPropertyChanged();
                }
            }
        }
        public TeacherCollection Teachers { get; }

        public static bool operator ==(School left, School right)
        {
            if (ReferenceEquals(left, right))
                return true;

            if ((object)left == null || (object)right == null)
                return false;

            return left.Id == right.Id;
        }
        public static bool operator !=(School left, School right)
        {
            return !(left == right);
        }

        public override bool Equals(object arg)
        {
            if (arg is School)
            {
                return (School)arg == this;
            }
            return false;
        }
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
        public override string ToString()
        {
            return _Name;
        }
    }
}
