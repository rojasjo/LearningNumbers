using System.Collections.Generic;
using LearningNumbers.Models;

namespace LearningNumbers.Extensions
{
    public static class OperatorExtension
    {
        public static void AddOperators(this ISet<Operator> operators, bool canDivide, bool canMultiply,
            bool canSubtract, bool canSum)
        {
            if (canDivide)
            {
                operators.Add(Operator.Division);
            }

            if (canMultiply)
            {
                operators.Add(Operator.Multiplication);
            }

            if (canSubtract)
            {
                operators.Add(Operator.Subtraction);
            }

            if (canSum)
            {
                operators.Add(Operator.Sum);
            }
        }
    }
}