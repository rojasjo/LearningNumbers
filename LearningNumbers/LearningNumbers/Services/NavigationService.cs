using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LearningNumbers.Services
{
    public class NavigationService : INavigationService
    {
        private readonly IAppContainer _appContainer;
        private readonly IViewFactory _viewFactory;

        public NavigationService(IAppContainer appContainer, IViewFactory viewFactory)
        {
            _appContainer = appContainer;
            _viewFactory = viewFactory;
        }

        public Task GoToQuestions(QuestionViewModelConfiguration viewModelConfiguration)
        {
            var view = _viewFactory.CreateQuestionView();
            var vm = view.GetViewModel();

            if (vm is null)
            {
                throw new InvalidOperationException();
            }

            vm.Configure(viewModelConfiguration);

            return _appContainer.GetApp().GetNavigation().PushAsync(view as Page, true);
        }

        public Task GoBack()
        {
            return _appContainer.GetApp().GetNavigation().PopAsync();
        }
    }
}