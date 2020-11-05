using LearningNumbers.Models;

namespace LearningNumbers.Services
{
    public interface ICalculationGenerator
    {
        void Configure(int maximunNumber, bool canSum, bool canSubstract, bool canMultiplicate, bool canDivide);
        Calculation Generate();
    }
}