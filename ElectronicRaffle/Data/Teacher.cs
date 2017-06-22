using Sorschia.Data;
using System;
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
        private string _Address;
        private Gender _Gender;
        private DateTime _BirthDate;
        private EducationalAttainment _EducationalAttainment;
        private bool _Member4Ps;
        private string _HouseholdNumber;

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

        public string Address
        {
            get { return _Address; }
            set
            {
                if (_Address != value)
                {
                    _Address = value;
                    OnPropertyChanged();
                }
            }
        }

        public Gender Gender
        {
            get { return _Gender; }
            set
            {
                if (_Gender != value)
                {
                    _Gender = value;
                    OnPropertyChanged();
                }
            }
        }

        public DateTime BirthDate
        {
            get { return _BirthDate; }
            set
            {
                if (_BirthDate != value)
                {
                    _BirthDate = value;
                    OnPropertyChanged();
                }
            }
        }

        public EducationalAttainment EducationalAttainment
        {
            get { return _EducationalAttainment; }
            set
            {
                if (_EducationalAttainment != value)
                {
                    _EducationalAttainment = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool Member4Ps
        {
            get { return _Member4Ps; }
            set
            {
                if (_Member4Ps != value)
                {
                    _Member4Ps = value;
                    OnPropertyChanged();
                }
            }
        }

        public string HouseholdNumber
        {
            get { return _HouseholdNumber; }
            set
            {
                if (_HouseholdNumber != value)
                {
                    _HouseholdNumber = value;
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
                var splittedMiddleName = _MiddleName.Trim().Split(' ');

                foreach (string item in splittedMiddleName)
                {
                    if (item.Length > 0)
                    {
                        var c = item[0];

                        if (char.IsLetter(c))
                        {
                            middleInitialBuilder.Append(c);
                        }
                    }
                }
                //middleInitialBuilder.Append(_MiddleName);
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
            return Equals(left, right);
        }

        public static bool operator !=(Teacher left, Teacher right)
        {
            return !(left == right);
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (GetType() != obj.GetType()) return false;

            var value = obj as Teacher;
            return Id.Equals(value.Id);
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
