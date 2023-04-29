using LearningNumber.Core.Services;

namespace LearningNumber.Core.Models
{
    public class Division : Calculation
    {
        public override int Second
        {
            get => base.Second;
            
            set
            {
                if (value == 0)
                {
                    throw new DivideByZeroException();
                }

                base.Second = value;
            }
        }

        public override Operator Symbol => Operator.Division;
        
        public override int Calculate()
        {
            return First / Second;
        }

        public override void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}