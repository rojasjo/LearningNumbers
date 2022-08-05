using System.Collections.Generic;
using System.Windows.Input;
using LearningNumbers.Exceptions;
using LearningNumbers.Extensions;
using LearningNumbers.Models;
using LearningNumbers.Services;
using LearningNumbers.Tests.Helpers;
using LearningNumbers.Utilities;
using LearningNumbers.ViewModels;
using Moq;
using NUnit.Framework;

namespace LearningNumbers.Tests
{
    [TestFixture]
    public class QuestionViewModelTests
    {
        private QuestionViewModel _systemUnderTest;

        private Mock<ICalculationGenerator> _calculationGeneratorMock;
        private Mock<INavigationService> _navigationServiceMock;

        [SetUp]
        public void Setup()
        {
            _calculationGeneratorMock = new Mock<ICalculationGenerator>();
            _navigationServiceMock = new Mock<INavigationService>();

            _systemUnderTest = new QuestionViewModel(_navigationServiceMock.Object, _calculationGeneratorMock.Object,
                new DigitService());
        }

        [Test]
        public void Configure_InvalidConfigurationObject_ThrowsInvalidConfigurationException()
        {
            Assert.Throws<InvalidConfigurationException>(() => _systemUnderTest.Configure(new object()));
        }

        [Test]
        public void Configure_ValidConfiguration_NoThrowException()
        {
            Assert.DoesNotThrow(() => _systemUnderTest.Configure(new QuestionViewModelConfiguration()));
        }

        [Test]
        public void Configure_ValidConfiguration_NumberOfQuestionsIsCorrect()
        {
            _systemUnderTest.Configure(new QuestionViewModelConfiguration {QuestionsNumber = 10});

            Assert.AreEqual(10, _systemUnderTest.NumberOfQuestions);
        }

        [Test]
        public void Configure_ValidConfiguration_IsAppliedCorrectly([Values(true, false)] bool canSum,
            [Values(true, false)] bool canSubtract,
            [Values(true, false)] bool canMultiply,
            [Values(true, false)] bool canDivide)
        {
            var operators = new HashSet<Operator>();
            operators.AddOperators(canDivide, canMultiply, canSubtract, canSum);

            var calculationConfiguration = new CalculationConfiguration(operators)
            {
                MaximumNumber = 10
            };

            _systemUnderTest.Configure(new QuestionViewModelConfiguration
            {
                QuestionsNumber = 10, CalculationConfiguration = calculationConfiguration
            });

            _calculationGeneratorMock.Verify(p => p.Configure(calculationConfiguration), Times.Once);
        }

        [Test]
        public void Configure_Configured10QuestionsNotAnsweredToAnyQuestion_ShowEndIsFalse()
        {
            _calculationGeneratorMock.SetupSum(Operator.Sum, 10, 20);

            _systemUnderTest.Configure(new QuestionViewModelConfiguration {QuestionsNumber = 10});

            Assert.False(_systemUnderTest.ShowEnd);
        }

        [Test]
        public void Configure_Configured10QuestionsNotAnsweredToAnyQuestion_CurrentAttemptsAreThree()
        {
            _calculationGeneratorMock.SetupSum(Operator.Sum, 10, 20);

            _systemUnderTest.Configure(new QuestionViewModelConfiguration {QuestionsNumber = 10});

            Assert.AreEqual(3, _systemUnderTest.CurrentAttempts);
        }

        [Test]
        public void Configure_Configured10QuestionsNotAnsweredToAnyQuestion_WrongAnswerAreZero()
        {
            _calculationGeneratorMock.SetupSum(Operator.Sum, 10, 20);

            _systemUnderTest.Configure(new QuestionViewModelConfiguration {QuestionsNumber = 10});

            Assert.AreEqual(0, _systemUnderTest.WrongAnswers);
        }

        [Test]
        public void Configure_Configured10QuestionsNotAnsweredToAnyQuestion_CorrectAnswerAreZero()
        {
            _calculationGeneratorMock.SetupSum(Operator.Sum, 10, 20);

            _systemUnderTest.Configure(new QuestionViewModelConfiguration {QuestionsNumber = 10});

            Assert.AreEqual(0, _systemUnderTest.CorrectAnswers);
        }

        [Test]
        public void
            CheckAnswerCommand_Configured10QuestionsAnsweredCorrectlyToAllQuestions100Times_GeneratedOnly10Questions()
        {
            _calculationGeneratorMock.SetupSum(Operator.Sum, 10, 20);

            ConfigureQuestionsAndAnswer(30, 10, 100);

            _calculationGeneratorMock.Verify(p => p.Generate(), Times.Exactly(10));
        }

        [Test]
        public void CheckAnswerCommand_AnsweredCorrectlyToAllQuestions_ShowEndIsTrue()
        {
            _calculationGeneratorMock.SetupSum(Operator.Sum, 10, 20);

            ConfigureQuestionsAndAnswer(30);

            Assert.True(_systemUnderTest.ShowEnd);
        }

        [Test]
        public void CheckAnswerCommand_AnsweredCorrectlyToAllQuestions_CorrectAnswerAre10()
        {
            _calculationGeneratorMock.SetupSum(Operator.Sum, 10, 20);

            ConfigureQuestionsAndAnswer(30);

            Assert.AreEqual(10, _systemUnderTest.CorrectAnswers);
        }

        [Test]
        public void CheckAnswerCommand_AnsweredCorrectlyToAllQuestions_WrongAnswerAreZero()
        {
            _calculationGeneratorMock.SetupSum(Operator.Sum, 10, 20);

            ConfigureQuestionsAndAnswer(30);

            Assert.AreEqual(0, _systemUnderTest.WrongAnswers);
        }

        [Test]
        public void CheckAnswerCommand_AnsweredCorrectlyToAllQuestions_NumberOfQuestionsIsZero()
        {
            _calculationGeneratorMock.SetupSum(Operator.Sum, 10, 20);

            ConfigureQuestionsAndAnswer(30);

            Assert.AreEqual(0, _systemUnderTest.NumberOfQuestions);
        }

        [Test]
        [TestCase(1, 2)]
        [TestCase(2, 1)]
        public void CheckAnswerCommand_WrongAttempts_CurrentAttemptsIsCorrect(int answersNumber,
            int expectedCurrentAttempts)
        {
            _calculationGeneratorMock.SetupSum(Operator.Sum, 10, 20);

            ConfigureQuestionsAndAnswer(10, 10, answersNumber);

            Assert.AreEqual(expectedCurrentAttempts, _systemUnderTest.CurrentAttempts);
        }

        [Test]
        [TestCase(1, 2)]
        [TestCase(2, 1)]
        public void CheckAnswerCommand_ConfiguredTenQuestionsWrongAttempts_NumberOfQuestionIsTen(int answersNumber,
            int expectedCurrentAttempts)
        {
            _calculationGeneratorMock.SetupSum(Operator.Sum, 10, 20);

            ConfigureQuestionsAndAnswer(10, 10, answersNumber);

            Assert.AreEqual(10, _systemUnderTest.NumberOfQuestions);
        }

        [Test]
        public void CheckAnswerCommand_OneQuestionIsAnsweredWrong_CurrentAttemptsAreResetToThree()
        {
            _calculationGeneratorMock.SetupSum(Operator.Sum, 10, 20);

            ConfigureQuestionsAndAnswer(10, 10, 3);

            Assert.AreEqual(3, _systemUnderTest.CurrentAttempts);
        }

        [Test]
        public void CheckAnswerCommand_OneQuestionIsAnsweredWrong_WrongAnswersIsOne()
        {
            _calculationGeneratorMock.SetupSum(Operator.Sum, 10, 20);

            ConfigureQuestionsAndAnswer(10, 10, 3);

            Assert.AreEqual(1, _systemUnderTest.WrongAnswers);
        }

        [Test]
        public void CheckAnswerCommand_OneQuestionIsAnsweredWrong_NumberOfQuestionsIsDecreased()
        {
            _calculationGeneratorMock.SetupSum(Operator.Sum, 10, 20);

            ConfigureQuestionsAndAnswer(10, 10, 3);

            Assert.AreEqual(9, _systemUnderTest.NumberOfQuestions);
        }

        [Test]
        public void CheckAnswerCommand_OneQuestionIsAnsweredWrong_TwoCalculationGenerated()
        {
            _calculationGeneratorMock.SetupSum(Operator.Sum, 10, 20);

            ConfigureQuestionsAndAnswer(10, 10, 3);

            _calculationGeneratorMock.Verify(p => p.Generate(), Times.Exactly(2));
        }

        [Test]
        public void CheckAnswerCommand_OneQuestionIsAnsweredWrong_ValidateCommandIsExecutedThreeTimes()
        {
            var commandMock = new Mock<ICommand>();
            _systemUnderTest.ValidateCommand = commandMock.Object;

            _calculationGeneratorMock.SetupSum(Operator.Sum, 10, 20);

            ConfigureQuestionsAndAnswer(10, 10, 3);

            commandMock.Verify(p => p.Execute(It.IsAny<object?>()), Times.Exactly(3));
        }

        [Test]
        public void CheckAnswerCommand_OneQuestionIsAnsweredWrong_AttemptsAnimationCommandIsExecutedTwice()
        {
            var commandMock = new Mock<ICommand>();
            _systemUnderTest.AttemptsAnimationCommand = commandMock.Object;

            _calculationGeneratorMock.SetupSum(Operator.Sum, 10, 20);

            ConfigureQuestionsAndAnswer(10, 10, 3);

            commandMock.Verify(p => p.Execute(It.IsAny<object?>()), Times.Exactly(2));
        }

        private void ConfigureQuestionsAndAnswer(int answer, int questions = 10, int answersNumber = 10)
        {
            _systemUnderTest.Configure(new QuestionViewModelConfiguration {QuestionsNumber = questions});

            for (var i = 0; i < answersNumber; i++)
            {
                AnswerToQuestion(answer);
            }
        }

        private void AnswerToQuestion(int answer)
        {
            _systemUnderTest.Answer = answer;
            _systemUnderTest.CheckAnswerCommand.Execute(null);
        }

        [Test]
        public void WriteNumberCommand_StringParsableToInt_AnswerIsSetCorrectly()
        {
            _systemUnderTest.WriteNumberCommand.Execute("10");

            Assert.AreEqual(10, _systemUnderTest.Answer);
        }

        [Test]
        public void WriteNumberCommand_AnswerIsOneStringParsableToInt_AnswerIsSetCorrectly()
        {
            _systemUnderTest.Answer = 1;

            _systemUnderTest.WriteNumberCommand.Execute("10");

            Assert.AreEqual(110, _systemUnderTest.Answer);
        }

        [Test]
        public void WriteNumberCommand_StringIsNotParsableToInt_AnswerIsNotModified()
        {
            _systemUnderTest.Answer = 1;

            _systemUnderTest.WriteNumberCommand.Execute("1d0");

            Assert.AreEqual(1, _systemUnderTest.Answer);
        }

        [Test]
        public void RemoveLastCharInAnswerCommand_AnswerHasOnlyOneDigit_AnswerIsSetNull()
        {
            _systemUnderTest.Answer = 1;

            _systemUnderTest.RemoveLastCharInAnswer.Execute(null);

            Assert.AreEqual(null, _systemUnderTest.Answer);
        }

        [Test]
        public void RemoveLastCharInAnswerCommand_AnswerHasThreeDigit_AnswerIsSetTwoDigit()
        {
            _systemUnderTest.Answer = 100;

            _systemUnderTest.RemoveLastCharInAnswer.Execute(null);

            Assert.AreEqual(10, _systemUnderTest.Answer);
        }

        [Test]
        public void RemoveLastCharInAnswerCommand_AnswerIsNull_AnswerIsStillNull()
        {
            _systemUnderTest.Answer = null;

            _systemUnderTest.RemoveLastCharInAnswer.Execute(null);

            Assert.AreEqual(null, _systemUnderTest.Answer);
        }
    }
}