using System;
using Autofac;
using LearningNumbers.Services;
using LearningNumbers.ViewModels;
using Xamarin.Forms;

namespace LearningNumbers.Bootstrap
{
    public class AppContainer : IAppContainer
    {
        private static IContainer _container;

        public static void RegisterDependencies()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<HomeViewModel>();
            builder.RegisterType<QuestionViewModel>();

            builder.RegisterType<CalculationGenerator>()
                .As<ICalculationGenerator>();
            
            builder.RegisterType<NavigationService>()
                .As<INavigationService>();

            builder.RegisterType<AppContainer>().As<IAppContainer>();
            builder.RegisterType<ViewFactory>().As<IViewFactory>();
            
            _container = builder.Build();
        }

        public static object Resolve(Type typename)
        {
            return _container.Resolve(typename);
        }

        private static IApplication GetCurrentApplication()
        {
            return Application.Current as IApplication;
        }

        public IApplication GetApp()
        {
            return GetCurrentApplication();
        }
    }
}