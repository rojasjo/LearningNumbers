using System.Threading.Tasks;

namespace LearningNumbers.Services
{
    public interface INavigationService
    {
        Task GoToQuestions(bool canSum, bool canSubstract, bool canMultiplicate,
            bool canDivide, int largestNumber, int numberOfQuestiosns);

        Task GoBack();
    }
}