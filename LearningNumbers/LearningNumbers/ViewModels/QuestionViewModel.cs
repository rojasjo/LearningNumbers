using System.Threading.Tasks;
using System.Windows.Input;
using LearningNumbers.Exceptions;
using LearningNumbers.Models;
using LearningNumbers.Services;
using Xamarin.Forms;

namespace LearningNumbers.ViewModels
{
    public class QuestionViewModel : BaseViewModel
    {
        private int numberOfQuestions;

        public int NumberOfQuestions
        {
            get { return numberOfQuestions; }
            set { SetProperty(ref numberOfQuestions, value); }
        }

        private int maxAttempts = 3;

        private int currentAttempts;

        public int CurrentAttempts
        {
            get { return currentAttempts; }
            set { SetProperty(ref currentAttempts, value); }
        }


        private int? answer;

        public int? Answer

        {
            get { return answer; }
            set { SetProperty(ref answer, value); }
        }

        private int correctAnswers = 0;

        public int CorrectAnswers
        {
            get { return correctAnswers; }
            set { SetProperty(ref correctAnswers, value); }
        }


        private int wrongAnswers;

        public int WrongAnswers
        {
            get { return wrongAnswers; }
            set { SetProperty(ref wrongAnswers, value); }
        }

        private bool showEnd;

        public bool ShowEnd
        {
            get { return showEnd; }
            set { SetProperty(ref showEnd, value); }
        }

        private Calculation currentCalculus;

        public Calculation CurrentCalculus
        {
            get { return currentCalculus; }
            set { SetProperty(ref currentCalculus, value); }
        }

        public ICommand CheckCommand { get; }
        public ICommand BackCommand { get; }
        public ICommand WriteNumberCommand { get; }
        public ICommand RemoveLastCharInAnswer { get; }

        public ICommand ValidateCommand { get; set; }
        public ICommand AttemptsAnimationCommand { get; set; }
        private ICalculationGenerator calculationGenerator;

        public QuestionViewModel(INavigationService navigation, ICalculationGenerator calculationGenerator) :
            base(navigation)
        {
            NumberOfQuestions = numberOfQuestions;
            this.calculationGenerator = calculationGenerator;
            CheckCommand = new Command(ExecuteCheckCommand);
            BackCommand = new Command(async () => await  ExecuteBackCommand());
            WriteNumberCommand = new Command<string>(ExecuteWriteNumberCommand);
            RemoveLastCharInAnswer = new Command(ExecuteRemoveLastCharInAnswer);
            CurrentAttempts = maxAttempts;
        }

        private void ExecuteRemoveLastCharInAnswer()
        {
            if (Answer != null)
            {
                string answerString = Answer.ToString();
                int lastIndex = answerString.Length - 1;
                answerString = answerString.Remove(lastIndex);
                if (int.TryParse(answerString, out int newAnswer))
                {
                    Answer = newAnswer;
                }
                else
                {
                    Answer = null;
                }
            }
        }

        private void ExecuteWriteNumberCommand(string x)
        {
            string currentValue = Answer.ToString() + x;

            if (int.TryParse(currentValue, out int newAnswer))
            {
                Answer = newAnswer;
            }
        }

        public void Start(QuestionViewModelConfiguration configuration)
        {
            calculationGenerator.Configure(configuration.CalculationConfiguration);
            
            NumberOfQuestions = configuration.QuestionsNumber;
            CurrentCalculus = calculationGenerator.Generate();
        }

        private Task ExecuteBackCommand()
        {
            return Application.Current.MainPage.Navigation.PopModalAsync();
        }

        private bool CanCheck()
        {
            return NumberOfQuestions > 0;
        }

        private void ExecuteCheckCommand()
        {
            if (CanCheck())
            {
                if (Answer == CurrentCalculus.Calculate())
                {
                    CorrectAnswers++;
                    NextQuestion();
                }
                else
                {
                    CurrentAttempts--;
                    ValidateCommand.Execute(null);

                    if (CurrentCalculus.Attempts >= maxAttempts)
                    {
                        WrongAnswers++;
                        NextQuestion();
                    }
                    else
                    {
                        AttemptsAnimationCommand.Execute(null);
                    }
                }
            }
        }

        private void NextQuestion()
        {
            NumberOfQuestions--;
            Answer = null;

            if (CanCheck())
            {
                CurrentCalculus = calculationGenerator.Generate();
                CurrentAttempts = maxAttempts;
            }
            else
            {
                ShowEnd = !CanCheck();
            }
        }

        public override void Configure(object configuration)
        {
            if (!(configuration is QuestionViewModelConfiguration calculationConfiguration))
            {
                throw new InvalidConfigurationException();
            }

            Start(calculationConfiguration);
        }
    }
}