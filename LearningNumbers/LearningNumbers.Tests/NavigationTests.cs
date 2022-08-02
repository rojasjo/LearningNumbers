using LearningNumbers.Bootstrap;
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
        private NavigationService testable;

        private Mock<IAppContainer> appContainerMock;
        private Mock<IApplication> applicationMock;
        private Mock<INavigation> navigationMock;
        private Mock<Page> questionPageMock;
        private Mock<IView> questionViewMock;
        private Mock<IViewFactory> viewFactoryMock;
        
        [SetUp]
        public void Setup()
        {
            SetupApplication();
            SetupQuestionView();

            testable = new NavigationService(appContainerMock.Object, viewFactoryMock.Object);
        }

        private void SetupQuestionView()
        {
            viewFactoryMock = new Mock<IViewFactory>();
            questionPageMock = new Mock<Page>();
            questionViewMock = questionPageMock.As<IView>();

            questionViewMock.Setup(p => p.GetViewModel())
                .Returns(new QuestionViewModel(new Mock<INavigationService>().Object, new CalculationGenerator()));

            viewFactoryMock.Setup(p => p.CreateQuestionView()).Returns(questionViewMock.Object);
        }

        private void SetupApplication()
        {
            applicationMock = new Mock<IApplication>();
            AppContainer.RegisterDependencies();
            appContainerMock = new Mock<IAppContainer>();
            navigationMock = new Mock<INavigation>();
            applicationMock.Setup(p => p.GetNavigation()).Returns(navigationMock.Object);
            appContainerMock.Setup(p => p.GetApp()).Returns(applicationMock.Object);
        }

        [Test]
        public void GoToQuestion_Always_NavigateToQuestionPage()
        {
            testable.GoToQuestions(true, false, false, false, 10, 10);

            navigationMock.Verify(p => p.PushAsync(questionPageMock.Object, It.IsAny<bool>()), Times.Once);
        }
    }
}