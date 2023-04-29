namespace LearningNumber.Core.Services
{
    public interface INavigationService
    {
        Task GoToQuestions(QuestionConfiguration configuration);

        Task GoBack();
    }
}