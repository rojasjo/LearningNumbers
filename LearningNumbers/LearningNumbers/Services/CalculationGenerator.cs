using System;
using System.Collections.Generic;
using LearningNumbers.Models;

namespace LearningNumbers.Services
{
    public class CalculationGenerator : ICalculationGenerator
    {
        int maxNumber;
        Dictionary<Operator, bool> choosenOperators;
        Random random;

        public void Configure(int maximunNumber, bool canSum, bool canSubstract,
                          bool canMultiplicate, bool canDivide)
        {
            choosenOperators = new Dictionary<Operator, bool>();
            random = new Random();

            //we want at least one operator to generate the calculus
            if (!canSum && !canSubstract && !canMultiplicate && !canDivide)
            {
                choosenOperators.Add(Operator.Sum, true);
            }
            else
            {
                choosenOperators.Add(Operator.Sum, canSum);
            }

            choosenOperators.Add(Operator.Subtraction, canSubstract);
            choosenOperators.Add(Operator.Multiplication, canMultiplicate);
            choosenOperators.Add(Operator.Division, canDivide);

            if (maximunNumber <= 0)
                maxNumber = 10;
            else
                maxNumber = maximunNumber;
        }
        
        public Calculation Generate()
        {
            int randomOperator = random.Next(4);

            bool isvalidOperator = false;

            while (!isvalidOperator)
            {
                bool isOperatorEnabled = choosenOperators[(Operator)randomOperator];

                if (isOperatorEnabled)
                    isvalidOperator = true;
                else
                    randomOperator = random.Next(4);
            }

            Calculation calculation = new Calculation((Operator)randomOperator);

            int a = random.Next(maxNumber + 1);
            int b = random.Next(maxNumber + 1);

            
            if (calculation.Operator == Operator.Subtraction)
            {
                bool aIsValid = a > 0;

                while (!aIsValid)
                {
                    a = random.Next(maxNumber + 1);
                    aIsValid = a > 0;
                }

                bool bIsValid = a >= b;

                while (!bIsValid)
                {
                    b = random.Next(maxNumber + 1);
                    bIsValid = a >= b;
                }
            }

            if (calculation.Operator == Operator.Division)
            {
                bool bIsValid = b > 0;

                while (!bIsValid)
                {
                    b = random.Next(maxNumber + 1);
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

