using LearningNumber.Core.Services;

namespace LearningNumber.Core.Models
{
    public class Sum : Calculation
    {
        public override Operator Symbol => Operator.Sum;
        
        public override int Calculate()
        {
            return First + Second;
        }

        public override void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}