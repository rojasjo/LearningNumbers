﻿using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LearningNumbers.Services
{
    public class NavigationService : INavigationService
    {
        private readonly IAppContainer appContainer;
        private readonly IViewFactory _viewFactory;

        public NavigationService(IAppContainer appContainer, IViewFactory viewFactory)
        {
            this.appContainer = appContainer;
            _viewFactory = viewFactory;
        }

        public Task GoBack()
        {
            return appContainer.GetApp().GetNavigation().PopAsync();
        }

        public Task GoToQuestions(bool canSum, bool canSubstract, bool CanMultiplicate, bool CanDivide,
            int largestNumber, int numberOfQuestions)
        {
            var view = _viewFactory.CreateQuestionView();
            var vm = view.GetViewModel();

            if (vm is null)
            {
                throw new InvalidOperationException();
            }

            vm.Configure(new {canSum, canSubstract, CanMultiplicate, CanDivide, largestNumber, numberOfQuestions});

            return appContainer.GetApp().GetNavigation().PushAsync(view as Page, true);
        }
    }
}