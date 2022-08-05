using System.Collections.Generic;
using System.Linq;
using LearningNumbers.Models;

namespace LearningNumbers.Services
{
    public class CalculationConfiguration
    {
        private readonly Operator[] _operators;

        private int _maximumNumber;

        public int MaximumNumber
        {
            get => _maximumNumber;
            set
            {
                if (value < 10)
                {
                    _maximumNumber = 10;
                }
                else if (value > 10000)
                {
                    _maximumNumber = 10000;
                }
                else
                {
                    _maximumNumber = value;
                }
            }
        }

        public IEnumerable<Operator> Operators => _operators;

        public CalculationConfiguration(IEnumerable<Operator> operators)
        {
            _operators = OperatorsIsEmpty(operators) ? new[] {Operator.Sum} : operators.ToArray();
        }

        private static bool OperatorsIsEmpty(IEnumerable<Operator> operators)
        {
            return operators == null || !operators.Any();
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
            return _operators.SequenceEqual(other._operators) && _maximumNumber == other._maximumNumber;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = _operators.GetHashCode();
                hashCode = (hashCode * 397) ^ _maximumNumber;
                return hashCode;
            }
        }
    }
}