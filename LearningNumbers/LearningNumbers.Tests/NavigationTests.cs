using System.Collections.Generic;
using LearningNumbers.Bootstrap;
using LearningNumbers.Models;
using LearningNumbers.Services;
using LearningNumbers.ViewModels;
using Moq;
using NUnit.Framework;
using Xamarin.Forms;

namespace LearningNumbers.Tests
{
    [TestFixture]
    public class NavigationTests
    {
        private NavigationService _testable;

        private Mock<IAppContainer> _appContainerMock;
        private Mock<IApplication> _applicationMock;
        private Mock<INavigation> _navigationMock;
        private Mock<Page> _questionPageMock;
        private Mock<IView> _questionViewMock;
        private Mock<IViewFactory> _viewFactoryMock;

        [SetUp]
        public void Setup()
        {
            SetupApplication();
            SetupQuestionView();

            _testable = new NavigationService(_appContainerMock.Object, _viewFactoryMock.Object);
        }

        private void SetupQuestionView()
        {
            _viewFactoryMock = new Mock<IViewFactory>();
            _questionPageMock = new Mock<Page>();
            _questionViewMock = _questionPageMock.As<IView>();

            _questionViewMock.Setup(p => p.GetViewModel())
                .Returns(new QuestionViewModel(new Mock<INavigationService>().Object, new CalculationGenerator(), new DigitService()));

            _viewFactoryMock.Setup(p => p.CreateQuestionView()).Returns(_questionViewMock.Object);
        }

        private void SetupApplication()
        {
            _applicationMock = new Mock<IApplication>();
            AppContainer.RegisterDependencies();
            _appContainerMock = new Mock<IAppContainer>();
            _navigationMock = new Mock<INavigation>();
            _applicationMock.Setup(p => p.GetNavigation()).Returns(_navigationMock.Object);
            _appContainerMock.Setup(p => p.GetApp()).Returns(_applicationMock.Object);
        }

        [Test]
        public void GoToQuestion_Always_NavigateToQuestionPage()
        {
            _testable.GoToQuestions(new QuestionViewModelConfiguration
                {CalculationConfiguration = new CalculationConfiguration(new List<Operator> {Operator.Division})});

            _navigationMock.Verify(p => p.PushAsync(_questionPageMock.Object, It.IsAny<bool>()), Times.Once);
        }

        [Test]
        public void GoBack_Always_PopAsyncIsInvoked()
        {
            _testable.GoBack();

            _navigationMock.Verify(p => p.PopAsync(), Times.Once);
        }
    }
}