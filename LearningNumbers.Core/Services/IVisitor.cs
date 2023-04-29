using LearningNumber.Core.Models;

namespace LearningNumber.Core.Services
{
    public interface IVisitor
    {
        void Visit(Division calculation);
        
        void Visit(Multiplication calculation);
        
        void Visit(Sum calculation);
        
        void Visit(Subtraction calculation);
    }
}