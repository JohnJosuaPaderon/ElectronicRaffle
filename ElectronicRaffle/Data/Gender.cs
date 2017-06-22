namespace ElectronicRaffle.Data
{
    public class Gender
    {
        public static Gender Male { get; } = new Gender(nameof(Male));
        public static Gender Female { get; } = new Gender(nameof(Female));

        private Gender(string value)
        {
            Value = value;
        }

        public string Value { get; }

        public static bool operator ==(Gender left, Gender right)
        {
            return Equals(left, right);
        }

        public static implicit operator Gender(string arg)
        {
            return !string.IsNullOrWhiteSpace(arg) ? new Gender(arg) : null;
        }

        public static implicit operator string(Gender arg)
        {
            return arg?.Value;
        }

        public static bool operator !=(Gender left, Gender right)
        {
            return !(left == right);
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (GetType() != obj.GetType()) return false;

            var value = obj as Gender;
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
