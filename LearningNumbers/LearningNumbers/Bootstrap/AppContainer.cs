using Autofac;
using LearningNumbers.Services;
using LearningNumbers.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace LearningNumbers.Bootstrap
{
    public class AppContainer
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

           

            _container = builder.Build();
        }

        /// <summary>
        /// This method will resolve dependency by typenames
        /// </summary>
        /// <param name="typename"></param>
        /// <returns></returns>
        public static object Resolve(Type typename)
        {
            return _container.Resolve(typename);
        }

        /// <summary>
        /// This method will resolve the dependency using generics
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T Resolve<T>()
        {
            return _container.Resolve<T>();
        }
    }
}
