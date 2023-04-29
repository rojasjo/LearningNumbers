using LearningNumber.Core.Services;

namespace LearningNumber.Core.Models
{
    public abstract class Calculation
    {
        public int First { get; set; }

        public virtual int Second { get; set; }

        public abstract Operator Symbol { get; }
        
        public abstract int Calculate();

        public abstract void Accept(IVisitor visitor);
    }
}