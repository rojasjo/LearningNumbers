using System.Collections.Generic;
using LearningNumber.Core.Extensions;
using LearningNumber.Core.Models;
using LearningNumber.Core.Services;
using LearningNumbers.Core.Services;
using NUnit.Framework;

namespace LearningNumbers.Tests
{
    [TestFixture]
    public class CalculationGeneratorTests
    {
        private CalculationGenerator _systemUnderTest;

        [SetUp]
        public void Setup()
        {
            _systemUnderTest = new CalculationGenerator();
        }

        [Test]
        public void Configure_OnlyDivision_GeneratedCalculationHasCorrectOperator()
        {
            ConfigureSystemUnderTest(true, false, false, false);

            var calculation = _systemUnderTest.Generate();

            Assert.AreEqual(Operator.Division, calculation.Symbol);
        }

        [Test]
        public void Calculate_OnlyDivision_GeneratedCalculationHasCorrectResult()
        {
            ConfigureSystemUnderTest(true, false, false, false);

            var calculation = _systemUnderTest.Generate();

            var expected = calculation.First / calculation.Second;

            Assert.AreEqual(expected, calculation.Calculate());
        }

        [Test]
        public void Configure_OnlyDivision_GeneratedCalculationHasCorrectResult()
        {
            ConfigureSystemUnderTest(true, false, false, false);

            var calculation = _systemUnderTest.Generate();

            var expected = calculation.First / calculation.Second;

            Assert.AreEqual(expected, calculation.Calculate());
        }

        [Test]
        public void Configure_OnlyMultiplication_GeneratedCalculationHasCorrectOperator()
        {
            ConfigureSystemUnderTest(false, true, false, false);

            var calculation = _systemUnderTest.Generate();

            Assert.AreEqual(Operator.Multiplication, calculation.Symbol);
        }

        [Test]
        public void Configure_OnlyMultiplication_GeneratedCalculationHasCorrectResult()
        {
            ConfigureSystemUnderTest(false, true, false, false);

            var calculation = _systemUnderTest.Generate();

            var expected = calculation.First * calculation.Second;

            Assert.AreEqual(expected, calculation.Calculate());
        }

        [Test]
        public void Configure_OnlySubtraction_GeneratedCalculationHasCorrectOperator()
        {
            ConfigureSystemUnderTest(false, false, true, false);

            var calculation = _systemUnderTest.Generate();

            Assert.AreEqual(Operator.Subtraction, calculation.Symbol);
        }

        [Test]
        public void Configure_OnlySubtraction_GeneratedCalculationHasCorrectResult()
        {
            ConfigureSystemUnderTest(false, false, true, false);

            var calculation = _systemUnderTest.Generate();

            var expected = calculation.First - calculation.Second;

            Assert.AreEqual(expected, calculation.Calculate());
        }

        [Test]
        public void Configure_OnlySum_GeneratedCalculationHasCorrectOperator()
        {
            ConfigureSystemUnderTest(false, false, false, true);

            var calculation = _systemUnderTest.Generate();

            Assert.AreEqual(Operator.Sum, calculation.Symbol);
        }

        [Test]
        public void Configure_OnlySum_GeneratedCalculationHasCorrectResult()
        {
            ConfigureSystemUnderTest(false, false, false, true);

            var calculation = _systemUnderTest.Generate();

            var expected = calculation.First + calculation.Second;

            Assert.AreEqual(expected, calculation.Calculate());
        }

        private void ConfigureSystemUnderTest(bool division, bool multiplication, bool subtraction, bool sum)
        {
            var operators = new HashSet<Operator>();
            operators.AddOperators(division, multiplication, subtraction, sum);
            _systemUnderTest.Configure(new CalculationConfiguration(operators)
            {
                MaximumNumber = 10
            });
        }
    }
}