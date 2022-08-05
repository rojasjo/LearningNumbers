using System;
using System.Linq;
using LearningNumbers.Models;

namespace LearningNumbers.Services
{
    public class CalculationGenerator : ICalculationGenerator
    {
        private int _maxNumber;

        private Operator[] _operators;

        private Random _random;

        public void Configure(CalculationConfiguration calculationConfiguration)
        {
            _random = new Random();
            _operators = calculationConfiguration.Operators.ToArray();

            _maxNumber = calculationConfiguration.MaximumNumber <= 0 ? 10 : calculationConfiguration.MaximumNumber;
        }

        public Calculation Generate()
        {
            var randomIndex = _random.Next(_operators.Length);
            var randomOperator = _operators[randomIndex];

            var calculation = CalculationFactory.CreateCalculation(randomOperator);  
            
            calculation.Accept(new CalculationVisitor(_maxNumber));
            
            return calculation;
        }
    }
}