using LearningNumbers.Models;

namespace LearningNumbers.Services
{
    public interface ICalculationGenerator
    {
        void Configure(CalculationConfiguration configuration);
        
        Calculation Generate();
    }
}