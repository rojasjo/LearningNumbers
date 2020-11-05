using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LearningNumbers.Services
{
    public interface INavigationService
    {
        Task GoToQuestions(bool canSum, bool canSubstract, bool CanMultiplicate,
                           bool CanDivide, int largestNumber, int numberOfQuestiosns);
        Task GoBack();
    }
}
