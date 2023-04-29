using LearningNumber.Core.Services;

namespace LearningNumber.Core.Models
{
    public class Subtraction : Calculation
    {
        public override Operator Symbol => Operator.Subtraction;
        
        public override int Calculate()
        {
            return First - Second;
        }

        public override void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}