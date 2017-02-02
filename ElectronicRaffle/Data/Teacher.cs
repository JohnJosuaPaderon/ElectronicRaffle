using Sorschia.Data;
using System.Text;

namespace ElectronicRaffle.Data
{
    public class Teacher : DataBase<ulong>
    {
        public Teacher(ulong id) : base(id)
        {
        }

        private string _FirstName;
        private string _MiddleName;
        private string _MiddleInitial;
        private string _LastName;
        private string _FullName;
        private string _InformalFullName;
        private string _ContactNumber;
        private School _School;
        private bool _AlreadyPicked;

        public string FirstName
        {
            get { return _FirstName; }
            set
            {
                if (_FirstName != value)
                {
                    _FirstName = value;
                    ConstructFullName();
                    ConstructInformalFullName();
                    OnPropertyChanged();
                }
            }
        }

        public string MiddleName
        {
            get { return _MiddleName; }
            set
            {
                if (_MiddleName != value)
                {
                    _MiddleName = value;
                    ConstructMiddleInitial();
                    ConstructFullName();
                    ConstructInformalFullName();
                    OnPropertyChanged();
                }
            }
        }

        public string MiddleInitial
        {
            get { return _MiddleInitial; }
        }

        public string LastName
        {
            get { return _LastName; }
            set
            {
                if (_LastName != value)
                {
                    _LastName = value;
                    ConstructFullName();
                    ConstructInformalFullName();
                    OnPropertyChanged();
                }
            }
        }

        public string FullName
        {
            get { return _FullName; }
        }

        public string InformalFullName
        {
            get { return _InformalFullName; }
        }

        public string ContactNumber
        {
            get { return _ContactNumber; }
            set
            {
                if (_ContactNumber != value)
                {
                    _ContactNumber = value;
                    OnPropertyChanged();
                }
            }
        }

        public School School
        {
            get { return _School; }
            set
            {
                if (_School != value)
                {
                    _School = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool AlreadyPicked
        {
            get { return _AlreadyPicked; }
            set
            {
                if (_AlreadyPicked != value)
                {
                    _AlreadyPicked = value;
                    OnPropertyChanged();
                }
            }
        }

        private void ConstructMiddleInitial()
        {
            var middleInitialBuilder = new StringBuilder();
            var hasMiddleName = !string.IsNullOrWhiteSpace(_MiddleName);

            if (hasMiddleName)
            {
                //var splittedMiddleName = _MiddleName.Trim().Split(' ');

                //foreach (string item in splittedMiddleName)
                //{
                //    if (item.Length > 0)
                //    {
                //        middleInitialBuilder.Append(item[0]);
                //    }
                //}
                middleInitialBuilder.Append(_MiddleName);
            }

            var middleInitial = middleInitialBuilder.ToString();

            if (_MiddleInitial != middleInitial)
            {
                _MiddleInitial = middleInitial;
                OnPropertyChanged(nameof(MiddleInitial));
            }
        }

        private void ConstructFullName()
        {
            var fullNameBuilder = new StringBuilder();
            var hasFirstName = !string.IsNullOrWhiteSpace(_FirstName);
            var hasMiddleName = !string.IsNullOrWhiteSpace(_MiddleName);
            var hasLastName = !string.IsNullOrWhiteSpace(_LastName);

            if (hasLastName)
            {
                fullNameBuilder.Append(_LastName.Trim());

                if (hasFirstName || hasMiddleName)
                {
                    fullNameBuilder.Append(", ");
                }
            }

            if (hasFirstName)
            {
                fullNameBuilder.Append(_FirstName.Trim());

                if (hasMiddleName)
                {
                    fullNameBuilder.Append(" ");
                }
            }

            if (hasMiddleName)
            {
                fullNameBuilder.Append(_MiddleName.Trim());
            }

            var fullName = fullNameBuilder.ToString();
            
            if (_FullName != fullName)
            {
                _FullName = fullName;
                OnPropertyChanged(nameof(FullName));
            }
        }

        private void ConstructInformalFullName()
        {
            var informalFullNameBuilder = new StringBuilder();
            var hasFirstName = !string.IsNullOrWhiteSpace(_FirstName);
            var hasMiddleInitial = !string.IsNullOrWhiteSpace(_MiddleInitial);
            var hasLastName = !string.IsNullOrWhiteSpace(_LastName);

            if (hasFirstName)
            {
                informalFullNameBuilder.Append(_FirstName.Trim());

                if (hasMiddleInitial || hasLastName)
                {
                    informalFullNameBuilder.Append(" ");
                }
            }

            if (hasMiddleInitial)
            {
                informalFullNameBuilder.AppendFormat("{0}.", _MiddleInitial.Trim());

                if (hasLastName)
                {
                    informalFullNameBuilder.Append(" ");
                }
            }

            if (hasLastName)
            {
                informalFullNameBuilder.Append(_LastName.Trim());
            }

            var informalFullName = informalFullNameBuilder.ToString();

            if (_InformalFullName != informalFullName)
            {
                _InformalFullName = informalFullName;
                OnPropertyChanged(nameof(InformalFullName));
            }
        }

        public static bool operator ==(Teacher left, Teacher right)
        {
            if (ReferenceEquals(left, right))
                return true;

            if ((object)left == null || (object)right == null)
                return false;

            return left.Id == right.Id;
        }
        public static bool operator !=(Teacher left, Teacher right)
        {
            return !(left == right);
        }

        public override bool Equals(object arg)
        {
            if (arg is Teacher)
            {
                return (Teacher)arg == this;
            }
            return false;
        }
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
        public override string ToString()
        {
            return _FullName;
        }
    }
}
