namespace ElectronicRaffle.Data
{
    public class ApplicantStatus
    {
        public static ApplicantStatus Qualified { get; } = new ApplicantStatus("Qualified");
        public static ApplicantStatus NotQualified { get; } = new ApplicantStatus("Not Qualified");
        public static ApplicantStatus HiredOnTheSpot { get; } = new ApplicantStatus("Hired On The Spot");
        public static ApplicantStatus forFurtherInterview { get; } = new ApplicantStatus("For Further Interview");

        private ApplicantStatus(string value)
        {
            Value = value;
        }

        public string Value { get; }

        public static implicit operator ApplicantStatus(string value)
        {
            return string.IsNullOrWhiteSpace(value) ? null : new ApplicantStatus(value);
        }

        public static implicit operator string(ApplicantStatus value)
        {
            return value?.Value;
        }

        public static bool operator ==(ApplicantStatus left, ApplicantStatus right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(ApplicantStatus left, ApplicantStatus right)
        {
            return !(left == right);
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (GetType() != obj.GetType()) return false;

            var value = obj as ApplicantStatus;
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
