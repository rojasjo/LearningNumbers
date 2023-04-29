using LearningNumber.Core.Models;

namespace LearningNumber.Core.Services
{
    public static class CalculationFactory
    {
        public static Calculation CreateCalculation(Operator calculationOperator)
        {
            Calculation calculation = null;
            switch (calculationOperator)
            {
                 case Operator.Division:
                     calculation = new Division();
                     break;
                 case Operator.Sum:
                     calculation = new Sum();
                     break;
                 case Operator.Subtraction:
                     calculation = new Subtraction();
                     break;
                 case Operator.Multiplication:
                     calculation = new Multiplication();
                     break;
                 default:
                     throw new ArgumentOutOfRangeException(nameof(calculationOperator), calculationOperator, null);
            }

            return calculation;
        }
    }
}