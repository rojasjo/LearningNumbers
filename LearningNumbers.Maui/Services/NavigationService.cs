using LearningNumber.Core.Services;
using LearningNumber.Core.ViewModels;
using LearningNumbers.Maui.Views;

namespace LearningNumbers.Maui.Services
{
    public class NavigationService : INavigationService
    {
        public async Task GoToQuestions(QuestionConfiguration configuration)
        {
            var view = new QuestionView(App.MauiContext.Services.GetService<QuestionViewModel>())
            {
                Configuration = configuration
            };
            
            await Application.Current.MainPage.Navigation.PushAsync(view);
        }

        public Task GoBack()
        {
            return Application.Current.MainPage.Navigation.PopAsync();
        }
    }
}