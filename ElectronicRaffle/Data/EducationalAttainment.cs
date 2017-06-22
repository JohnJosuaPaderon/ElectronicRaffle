namespace ElectronicRaffle.Data
{
    public class EducationalAttainment
    {
        public static EducationalAttainment Elementary { get; } = new EducationalAttainment(nameof(Elementary));
        public static EducationalAttainment HighSchool { get; } = new EducationalAttainment(nameof(HighSchool));
        public static EducationalAttainment College { get; } = new EducationalAttainment(nameof(College));

        private EducationalAttainment(string value)
        {
            Value = value;
        }

        public string Value { get; }

        public static implicit operator EducationalAttainment(string arg)
        {
            return !string.IsNullOrWhiteSpace(arg) ? new EducationalAttainment(arg) : null;
        }

        public static implicit operator string(EducationalAttainment arg)
        {
            return arg?.Value;
        }

        public static bool operator ==(EducationalAttainment left, EducationalAttainment right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(EducationalAttainment left, EducationalAttainment right)
        {
            return !(left == right);
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (GetType() != obj.GetType()) return false;

            var value = obj as EducationalAttainment;
            return Value.Equals(value.Value);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public override string ToString()
        {
            return Value;
        }
    }
}
