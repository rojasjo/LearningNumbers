using LearningNumbers.Views;
using Xamarin.Forms;

namespace LearningNumbers.Services
{
    public class ViewFactory : IViewFactory
    {
        public IView CreateQuestionView()
        {
            return new QuestionView();
        }
    }
}