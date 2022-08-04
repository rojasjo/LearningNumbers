using System;
using System.Collections.Generic;
using LearningNumbers.Models;

namespace LearningNumbers.Services
{
    public class CalculationGenerator : ICalculationGenerator
    {
        private int _maxNumber;
        Dictionary<Operator, bool> _choosenOperators;
        private Random _random;

        public void Configure(CalculationConfiguration calculationConfiguration)
        {
            _choosenOperators = new Dictionary<Operator, bool>();
            _random = new Random();

            RegisterOperators(calculationConfiguration);

            _maxNumber = calculationConfiguration.MaximumNumber <= 0 ? 10 : calculationConfiguration.MaximumNumber;
        }

        private void RegisterOperators(CalculationConfiguration calculationConfiguration)
        {
            //we want at least one operator to generate the calculus
            _choosenOperators.Add(Operator.Sum,
                !calculationConfiguration.AnyOperatorEnabled() || calculationConfiguration.CanSum);

            _choosenOperators.Add(Operator.Subtraction, calculationConfiguration.CanSubtract);
            _choosenOperators.Add(Operator.Multiplication, calculationConfiguration.CanMultiply);
            _choosenOperators.Add(Operator.Division, calculationConfiguration.CanDivide);
        }

        public Calculation Generate()
        {
            var randomOperator = _random.Next(4);

            var isValidOperator = false;

            while (!isValidOperator)
            {
                bool isOperatorEnabled = _choosenOperators[(Operator) randomOperator];

                if (isOperatorEnabled)
                    isValidOperator = true;
                else
                    randomOperator = _random.Next(4);
            }

            Calculation calculation = new Calculation((Operator) randomOperator);

            int a = _random.Next(_maxNumber + 1);
            int b = _random.Next(_maxNumber + 1);


            if (calculation.Operator == Operator.Subtraction)
            {
                bool aIsValid = a > 0;

                while (!aIsValid)
                {
                    a = _random.Next(_maxNumber + 1);
                    aIsValid = a > 0;
                }

                bool bIsValid = a >= b;

                while (!bIsValid)
                {
                    b = _random.Next(_maxNumber + 1);
                    bIsValid = a >= b;
                }
            }

            if (calculation.Operator == Operator.Division)
            {
                bool bIsValid = b > 0;

                while (!bIsValid)
                {
                    b = _random.Next(_maxNumber + 1);
                    bIsValid = b > 0;
                }

                int risultato = a * b;
                a = risultato;
            }

            calculation.First = a;
            calculation.Second = b;
            return calculation;
        }
    }
}