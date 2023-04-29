using LearningNumber.Core.Models;
using LearningNumber.Core.Services;
using Moq;

namespace LearningNumbers.Tests.Helpers
{
    public static class CalculationGeneratorMock
    {
        public static Mock<ICalculationGenerator> SetupSum(this Mock<ICalculationGenerator> calculationGeneratorMock, int first, int second)
        {
            calculationGeneratorMock.Setup(p => p.Generate()).Returns(new Sum {First = first, Second = second});
            return calculationGeneratorMock;
        }
    }
}