using Xamarin.Forms;

namespace LearningNumbers.Services
{
    public interface IViewFactory
    {
        IView CreateQuestionView();
    }
}