using LearningNumbers.Exceptions;
using LearningNumbers.Models;
using LearningNumbers.Services;
using LearningNumbers.ViewModels;
using Moq;
using NUnit.Framework;

namespace LearningNumbers.Tests
{
    [TestFixture]
    public class QuestionViewModelTests
    {
        private QuestionViewModel _testable;
        
        private Mock<ICalculationGenerator> _calculationGeneratorMock;
        private Mock<INavigationService> _navigationServiceMock;

        [SetUp]
        public void Setup()
        {
            _calculationGeneratorMock = new Mock<ICalculationGenerator>();
            _navigationServiceMock = new Mock<INavigationService>();
            
            _testable = new QuestionViewModel(_navigationServiceMock.Object, _calculationGeneratorMock.Object);
        }
        
        [Test]
        public void Configure_InvalidConfigurationObject_ThrowsInvalidConfigurationException()
        {
            Assert.Throws<InvalidConfigurationException>(() =>_testable.Configure(new object()));
        }
        
        [Test]
        public void Configure_ValidConfiguration_NoThrowException()
        {
            Assert.DoesNotThrow(() =>_testable.Configure(new QuestionViewModelConfiguration()));
        }
        
        [Test]
        public void Configure_ValidConfiguration_NumberOfQuestionsIsCorrect()
        {
            _testable.Configure(new QuestionViewModelConfiguration() {QuestionsNumber = 10});

            Assert.AreEqual(10, _testable.NumberOfQuestions);
        }
        
        [Test]
        public void Configure_ValidConfiguration_IsAppliedCorrectly()
        {
            _calculationGeneratorMock.Setup(p => p.Generate())
                .Returns(new Calculation(Operator.Sum) {First = 10, Second = 20});
            
            _testable.Configure(new QuestionViewModelConfiguration() {QuestionsNumber = 10});

            Assert.AreEqual(Operator.Sum, _testable.CurrentCalculus.Operator);
        }
    }
}