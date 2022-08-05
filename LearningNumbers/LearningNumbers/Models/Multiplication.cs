using LearningNumbers.Services;

namespace LearningNumbers.Models
{
    public class Multiplication : Calculation
    {
        public override Operator Symbol => Operator.Multiplication;
        
        public override int Calculate()
        {
            return First * Second;
        }

        public override void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}