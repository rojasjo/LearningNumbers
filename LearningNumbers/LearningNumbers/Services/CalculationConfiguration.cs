namespace LearningNumbers.Services
{
    public class CalculationConfiguration
    {
        public bool CanDivide { get; set; }

        public bool CanMultiply { get; set; }

        public bool CanSubtract { get; set; }

        public bool CanSum { get; set; }

        public int MaximumNumber { get; set; }

        public bool AnyOperatorEnabled()
        {
            return CanSum || CanDivide || CanMultiply || CanSubtract;
        }
        
        
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj.GetType() != GetType())
            {
                return false;
            }

            return Equals((CalculationConfiguration) obj);
        }

        protected bool Equals(CalculationConfiguration other)
        {
            return CanDivide == other.CanDivide && CanMultiply == other.CanMultiply &&
                   CanSubtract == other.CanSubtract && CanSum == other.CanSum && MaximumNumber == other.MaximumNumber;
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = CanDivide.GetHashCode();
                hashCode = (hashCode * 397) ^ CanMultiply.GetHashCode();
                hashCode = (hashCode * 397) ^ CanSubtract.GetHashCode();
                hashCode = (hashCode * 397) ^ CanSum.GetHashCode();
                hashCode = (hashCode * 397) ^ MaximumNumber;
                return hashCode;
            }
        }
    }
}