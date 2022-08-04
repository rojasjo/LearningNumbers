using System.Threading.Tasks;

namespace LearningNumbers.Services
{
    public interface INavigationService
    {
        Task GoToQuestions(QuestionViewModelConfiguration viewModelConfiguration);

        Task GoBack();
    }
}