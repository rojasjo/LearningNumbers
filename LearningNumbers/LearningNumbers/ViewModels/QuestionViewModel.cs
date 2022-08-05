using System.Windows.Input;
using LearningNumbers.Exceptions;
using LearningNumbers.Models;
using LearningNumbers.Services;
using Xamarin.Forms;

namespace LearningNumbers.ViewModels
{
    public class QuestionViewModel : BaseViewModel
    {
        private readonly ICalculationGenerator _calculationGenerator;
        private readonly IDigitService _digitService;
        
        private const int MaxAttempts = 3;

        private int _numberOfQuestions;

        public int NumberOfQuestions
        {
            get => _numberOfQuestions;
            set => SetProperty(ref _numberOfQuestions, value);
        }

        private int _currentAttempts;

        public int CurrentAttempts
        {
            get => _currentAttempts;
            set => SetProperty(ref _currentAttempts, value);
        }

        private int? _answer;

        public int? Answer

        {
            get => _answer;
            set => SetProperty(ref _answer, value);
        }

        private int _correctAnswers;

        public int CorrectAnswers
        {
            get => _correctAnswers;
            set => SetProperty(ref _correctAnswers, value);
        }

        private int _wrongAnswers;

        public int WrongAnswers
        {
            get => _wrongAnswers;
            set => SetProperty(ref _wrongAnswers, value);
        }

        private bool _showEnd;

        public bool ShowEnd
        {
            get => _showEnd;
            set => SetProperty(ref _showEnd, value);
        }

        private Calculation _currentCalculus;
        
        public Calculation CurrentCalculus
        {
            get => _currentCalculus;
            set => SetProperty(ref _currentCalculus, value);
        }

        public ICommand CheckAnswerCommand { get; }

        public ICommand WriteNumberCommand { get; }

        public ICommand RemoveLastCharInAnswer { get; }

        public ICommand ValidateCommand { get; set; }

        public ICommand AttemptsAnimationCommand { get; set; }

        public QuestionViewModel(INavigationService navigation, ICalculationGenerator calculationGenerator, IDigitService digitService) :
            base(navigation)
        {
            _calculationGenerator = calculationGenerator;
            _digitService = digitService;
            
            NumberOfQuestions = _numberOfQuestions;
            CurrentAttempts = MaxAttempts;

            CheckAnswerCommand = new Command(ExecuteCheckCommand);
            WriteNumberCommand = new Command<string>(ExecuteWriteNumberCommand);
            RemoveLastCharInAnswer = new Command(ExecuteRemoveLastCharInAnswer);
        }

        public override void Configure(object configuration)
        {
            if (!(configuration is QuestionViewModelConfiguration calculationConfiguration))
            {
                throw new InvalidConfigurationException();
            }

            Start(calculationConfiguration);
        }

        private void Start(QuestionViewModelConfiguration configuration)
        {
            _calculationGenerator.Configure(configuration.CalculationConfiguration);

            NumberOfQuestions = configuration.QuestionsNumber;
            CurrentCalculus = _calculationGenerator.Generate();
        }

        private void ExecuteRemoveLastCharInAnswer()
        {
            Answer = _digitService.RemoveLastDigit(Answer);
        }

        private void ExecuteWriteNumberCommand(string digit)
        {
            Answer = _digitService.AppendDigits(Answer, digit);
        }

        private void ExecuteCheckCommand()
        {
            if (!HasUnansweredQuestions())
            {
                return;
            }

            if (IsAnswerCorrect())
            {
                CorrectAnswers++;
                NextQuestion();
            }
            else
            {
                ManageWrongAnswer();
            }
        }

        private bool IsAnswerCorrect()
        {
            return Answer == CurrentCalculus.Calculate();
        }

        private void ManageWrongAnswer()
        {
            CurrentAttempts--;
            ValidateCommand?.Execute(null);

            if (AttemptsFinished())
            {
                WrongAnswers++;
                NextQuestion();
            }
            else
            {
                AttemptsAnimationCommand?.Execute(null);
            }
        }

        private bool AttemptsFinished()
        {
            return CurrentAttempts <= 0;
        }

        private void NextQuestion()
        {
            NumberOfQuestions--;
            Answer = null;

            if (HasUnansweredQuestions())
            {
                CurrentCalculus = _calculationGenerator.Generate();
                CurrentAttempts = MaxAttempts;
            }
            else
            {
                ShowEnd = true;
            }
        }

        private bool HasUnansweredQuestions()
        {
            return NumberOfQuestions > 0;
        }
    }
}