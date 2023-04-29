using LearningNumber.Core.Models;
using LearningNumbers.Core.Services;

namespace LearningNumber.Core.Services
{
    public interface ICalculationGenerator
    {
        void Configure(CalculationConfiguration configuration);
        
        Calculation Generate();
    }
}