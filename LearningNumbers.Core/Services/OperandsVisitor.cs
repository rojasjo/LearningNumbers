using LearningNumber.Core.Models;

namespace LearningNumber.Core.Services
{
    public class OperandsVisitor : IVisitor
    {
        private readonly int _maxNumber;

        private readonly Random _random;

        private int _first;
        private int _second;

        public OperandsVisitor(int maxNumber)
        {
            _random = new Random();
            _maxNumber = maxNumber + 1;

            GenerateFirsAndSecond();
        }

        public void Visit(Division calculation)
        {
            _second = _random.Next(1, _maxNumber);

            var result = _first * _second;
            _first = result;

            AssignValueTo(calculation);
        }

        public void Visit(Subtraction calculation)
        {
            _first = _random.Next(1, _maxNumber);

            var secondIsValid = false;

            while (!secondIsValid)
            {
                _second = _random.Next(1, _maxNumber);
                secondIsValid = _first >= _second;
            }

            AssignValueTo(calculation);
        }

        public void Visit(Multiplication calculation)
        {
            AssignValueTo(calculation);
        }

        public void Visit(Sum calculation)
        {
            AssignValueTo(calculation);
        }

        private void AssignValueTo(Calculation calculation)
        {
            calculation.First = _first;
            calculation.Second = _second;
        }

        private void GenerateFirsAndSecond()
        {
            _first = _random.Next(1, _maxNumber);
            _second = _random.Next(1, _maxNumber);
        }
    }
}