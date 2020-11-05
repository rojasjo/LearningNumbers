using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using LearningNumbers.Services;
using LearningNumbers.Views;
using System.Runtime.CompilerServices;
using LearningNumbers.Bootstrap;

namespace LearningNumbers
{
    public partial class App : Application
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
    }
}
