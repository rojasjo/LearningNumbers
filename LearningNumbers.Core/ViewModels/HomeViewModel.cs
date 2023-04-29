using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using LearningNumber.Core.Extensions;
using LearningNumber.Core.Models;
using LearningNumber.Core.Services;
using LearningNumbers.Core.Services;

namespace LearningNumber.Core.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        private bool canSum = true;

        public bool CanSum
        {
            get { return canSum; }
            set { SetProperty(ref canSum, value); }
        }

        private bool canSubstract;

        public bool CanSubtract
        {
            get { return canSubstract; }
            set { SetProperty(ref canSubstract, value); }
        }


        private bool canMultipilcate;

        public bool CanMultipilcate
        {
            get { return canMultipilcate; }
            set { SetProperty(ref canMultipilcate, value); }
        }


        private bool canDivide;

        public bool CanDivide
        {
            get { return canDivide; }
            set
            {
                if (value)
                {
                    Minimun = 100;
                    MinimunMedium = 400;
                    Medium = 2_500;
                    Big = 10_000;
                }
                else
                {
                    Minimun = 10;
                    MinimunMedium = 20;
                    Medium = 50;
                    Big = 100;
                }


                SetProperty(ref canDivide, value);
            }
        }


        private int largestNumber = 10;

        private bool largestIs10 = true;

        public bool LargestIs10
        {
            get { return largestIs10; }

            set
            {
                if (value == true)
                {
                    largestNumber = 10;
                    LargestIs20 = false;
                    LargestIs50 = false;
                    LargestIs100 = false;
                }

                SetProperty(ref largestIs10, value);
            }
        }

        private bool largestIs20;

        public bool LargestIs20
        {
            get { return largestIs20; }
            set
            {
                if (value == true)
                {
                    largestNumber = 20;
                    LargestIs10 = false;
                    LargestIs50 = false;
                    LargestIs100 = false;
                }

                SetProperty(ref largestIs20, value);
            }
        }

        private bool largestIs50;

        public bool LargestIs50
        {
            get { return largestIs50; }
            set
            {
                if (value == true)
                {
                    largestNumber = 50;
                    LargestIs10 = false;
                    LargestIs20 = false;
                    LargestIs100 = false;
                }

                SetProperty(ref largestIs50, value);
            }
        }

        private bool largestIs100;

        public bool LargestIs100
        {
            get { return largestIs100; }
            set
            {
                if (value == true)
                {
                    largestNumber = 100;
                    LargestIs10 = false;
                    LargestIs20 = false;
                    LargestIs50 = false;
                }

                SetProperty(ref largestIs100, value);
            }
        }

        private int minimun = 10;

        public int Minimun
        {
            get { return minimun; }
            set { SetProperty(ref minimun, value); }
        }

        private int minimunMedium = 20;

        public int MinimunMedium
        {
            get { return minimunMedium; }
            set { SetProperty(ref minimunMedium, value); }
        }

        private int medium = 50;

        public int Medium
        {
            get { return medium; }
            set { SetProperty(ref medium, value); }
        }

        private int big = 100;

        public int Big
        {
            get { return big; }
            set { SetProperty(ref big, value); }
        }

        private int numberOfQuestions = 10;

        private bool are10Questions = true;

        public bool Are10Questions
        {
            get { return are10Questions; }
            set
            {
                if (value == true)
                {
                    numberOfQuestions = 10;
                    Are25Questions = false;
                    Are50Questions = false;
                    Are100Questions = false;
                }

                SetProperty(ref are10Questions, value);
            }
        }

        private bool are25Questions;

        public bool Are25Questions
        {
            get { return are25Questions; }
            set
            {
                if (value == true)
                {
                    numberOfQuestions = 25;
                    Are10Questions = false;
                    Are50Questions = false;
                    Are100Questions = false;
                }

                SetProperty(ref are25Questions, value);
            }
        }

        private bool are50Questions;

        public bool Are50Questions
        {
            get { return are50Questions; }
            set
            {
                if (value == true)
                {
                    numberOfQuestions = 50;
                    Are10Questions = false;
                    Are25Questions = false;
                    Are100Questions = false;
                }

                SetProperty(ref are50Questions, value);
            }
        }

        private bool are100Questions;

        public bool Are100Questions
        {
            get { return are100Questions; }
            set
            {
                if (value == true)
                {
                    numberOfQuestions = 100;
                    Are10Questions = false;
                    Are25Questions = false;
                    Are50Questions = false;
                }

                SetProperty(ref are100Questions, value);
            }
        }

        public ICommand PlayCommand { get; }

        public HomeViewModel(INavigationService navigation) : base(navigation)
        {
            PlayCommand = new RelayCommand(async () => await ExecutePlayCommand());
        }

        private Task ExecutePlayCommand()
        {
            var operators = new HashSet<Operator>();

            operators.AddOperators(CanDivide, CanMultipilcate, CanSubtract, CanSum);

            var configuration = new QuestionConfiguration()
            {
                QuestionsNumber = numberOfQuestions,
                CalculationConfiguration = new CalculationConfiguration(operators)
                {
                    MaximumNumber = largestNumber
                }
            };

            return NavigationService.GoToQuestions(configuration);
        }
    }
}