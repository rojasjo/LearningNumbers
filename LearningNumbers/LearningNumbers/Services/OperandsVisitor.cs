using System;
using LearningNumbers.Models;

namespace LearningNumbers.Services
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
            _maxNumber = maxNumber;
            
            GenerateFirsAndSecond();
        }

        public void Visit(Division calculation)
        {
            var isValid = _second > 0;

            while (!isValid)
            {
                _second = _random.Next(_maxNumber + 1);
                isValid = _second > 0;
            }

            var result = _first * _second;
            _first = result;

            AssignValueTo(calculation);
        }

        public void Visit(Subtraction calculation)
        {
            var firstIsValid = _first > 0;

            while (!firstIsValid)
            {
                _first = _random.Next(_maxNumber + 1);
                firstIsValid = _first > 0;
            }

            var secondIsValid = _first >= _second;

            while (!secondIsValid)
            {
                _second = _random.Next(_maxNumber + 1);
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
            _first = _random.Next(_maxNumber + 1);
            _second = _random.Next(_maxNumber + 1);
        }
    }
}