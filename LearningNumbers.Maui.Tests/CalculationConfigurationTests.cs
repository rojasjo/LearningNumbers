using System.Collections.Generic;
using LearningNumber.Core.Models;
using LearningNumbers.Core.Services;
using NUnit.Framework;

namespace LearningNumbers.Tests
{
    [TestFixture]
    public class CalculationConfigurationTests
    {
        [Test]
        public void Equals_SameValues_ReturnsTrue()
        {
            var first = new CalculationConfiguration(new List<Operator>())
            {
                MaximumNumber = 10
            };

            var second = new CalculationConfiguration(new List<Operator>())
            {
                MaximumNumber = 10
            };

            Assert.AreEqual(first, second);
        }

        [Test]
        public void Equals_DifferentValues_ReturnsFalse()
        {
            var first = new CalculationConfiguration(new List<Operator>()
            {
                Operator.Division,
                Operator.Multiplication,
                Operator.Subtraction,
                Operator.Sum
            })
            {
                MaximumNumber = 10
            };

            var second = new CalculationConfiguration(new List<Operator>()
            {
                Operator.Division,
                Operator.Multiplication,
                Operator.Sum
            })
            {
                MaximumNumber = 10
            };

            Assert.AreNotEqual(first, second);
        }

        [Test]
        public void Equals_SecondHasMaximumNumberLessThan10_ReturnsTrue()
        {
            var first = new CalculationConfiguration(new List<Operator>()
            {
                Operator.Division,
                Operator.Multiplication,
                Operator.Subtraction,
                Operator.Sum
            })
            {
                MaximumNumber = 10
            };

            var second = new CalculationConfiguration(new List<Operator>()
            {
                Operator.Division,
                Operator.Multiplication,
                Operator.Subtraction,
                Operator.Sum
            })
            {
                MaximumNumber = 1
            };

            Assert.AreEqual(first, second);
        }

        [Test]
        public void MaximumNumber_LessThan10_IsSetTo10()
        {
            var calculationConfiguration = new CalculationConfiguration(new List<Operator>())
            {
                MaximumNumber = -10
            };

            Assert.AreEqual(10, calculationConfiguration.MaximumNumber);
        }

        [Test]
        public void MaximumNumber_GreaterThan10000_IsSetTo10000()
        {
            var calculationConfiguration = new CalculationConfiguration(new List<Operator>())
            {
                MaximumNumber = 10_001
            };

            Assert.AreEqual(10_000, calculationConfiguration.MaximumNumber);
        }

        [Test]
        public void MaximumNumber_Between10And10000_IsSetCorrectly([Range(10, 10000, 10)] int value)
        {
            var calculationConfiguration = new CalculationConfiguration(new List<Operator>())
            {
                MaximumNumber = value
            };

            Assert.AreEqual(value, calculationConfiguration.MaximumNumber);
        }
    }
}