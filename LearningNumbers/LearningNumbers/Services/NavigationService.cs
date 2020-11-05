using LearningNumbers.ViewModels;
using LearningNumbers.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LearningNumbers.Services
{
    public class NavigationService : INavigationService
    {
     
        public Task GoBack()
        {
            return Application.Current.MainPage.Navigation.PopAsync();
        }

        public Task GoToQuestions(bool canSum, bool canSubstract, bool CanMultiplicate, bool CanDivide, int largestNumber, int numberOfQuestions)
        {
            var view = new QuestionView();
            QuestionViewModel vm = view.BindingContext as QuestionViewModel;

            if (vm is null)
                throw new InvalidOperationException();

            vm.Start(canSum, canSubstract, CanMultiplicate, CanDivide, largestNumber, numberOfQuestions);
            return Application.Current.MainPage.Navigation.PushAsync(view, true);
        }
    }
}
