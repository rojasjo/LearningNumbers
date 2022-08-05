using System;
using System.Globalization;
using LearningNumbers.Converters;
using LearningNumbers.Models;
using NUnit.Framework;

namespace LearningNumbers.Tests
{
    [TestFixture]
    public class SymbolConverterTests
    {
        private SymbolConverter _systemUnderTest;

        [SetUp]
        public void Setup()
        {
            _systemUnderTest = new SymbolConverter();
        }

        [Test]
        public void ConvertBack_Always_ThrowsNotImplementedException(
            [Values("", " ", "+", "-", "/", "*", "÷", "×")]
            string symbol)
        {
            Assert.Throws<NotImplementedException>(() =>
                _systemUnderTest.ConvertBack(symbol, typeof(Operator), null, CultureInfo.InvariantCulture));
        }

        [Test]
        [TestCase(Operator.Division, "÷")]
        [TestCase(Operator.Multiplication, "×")]
        [TestCase(Operator.Subtraction, "-")]
        [TestCase(Operator.Sum, "+")]
        public void Convert_ValidOperator_ReturnExpectedSymbol(
            Operator arithmeticOperator, string expectedSymbol)
        {
            var actualSymbol = (string)
                _systemUnderTest.Convert(arithmeticOperator, typeof(string), null, CultureInfo.InvariantCulture);

            Assert.AreEqual(expectedSymbol, actualSymbol);
        }

        [Test]
        public void Convert_InvalidOperator_ReturnsEmptyString()
        {
            var actualSymbol = (string)
                _systemUnderTest.Convert(new object(), typeof(string), null, CultureInfo.InvariantCulture);

            Assert.AreEqual(string.Empty, actualSymbol);
        }
    }
}