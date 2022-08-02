using LearningNumbers.ViewModels;

namespace LearningNumbers.Services
{
    public interface IView
    {
        BaseViewModel GetViewModel();
    }
}