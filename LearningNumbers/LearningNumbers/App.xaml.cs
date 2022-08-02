using LearningNumbers.Bootstrap;
using LearningNumbers.Services;
using LearningNumbers.Views;
using Xamarin.Forms;

namespace LearningNumbers
{
    public partial class App : Application, IApplication
    {
        public App()
        {
            InitializeComponent();
            AppContainer.RegisterDependencies();
            MainPage = new NavigationPage(new HomeView());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        public INavigation GetNavigation()
        {
            return MainPage.Navigation;
        }
    }
}