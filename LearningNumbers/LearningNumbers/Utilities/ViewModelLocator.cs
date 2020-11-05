using LearningNumbers.Bootstrap;
using System;
using System.Globalization;
using System.Reflection;
using Xamarin.Forms;

namespace LearningNumbers.Utilities
{
    public class ViewModelLocator
    {
        public static readonly BindableProperty AutoWireViewModelProperty =
            BindableProperty.CreateAttached("AutoWireViewModel", typeof(bool),
            typeof(ViewModelLocator), default(bool),
            propertyChanged: OnAutoWireViewModelChanged);


        public static bool GetAutoWireViewModel(BindableObject bindable)
        {
            return (bool)bindable.GetValue(AutoWireViewModelProperty);
        }

        public static void SetAutoWireViewModel(BindableObject bindable, bool value)
        {
            bindable.SetValue(AutoWireViewModelProperty, value);
        }

        private static void OnAutoWireViewModelChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (!(bindable is Element view))
                return;

            var viewType = view.GetType();
            //I will assume that each view has its own viewmodel in the ViewModels namespaces
            //and the have the same prefix
            //Example: LoginView ==> LoginViewModel
            var viewName = viewType.FullName.Replace(".Views.", ".ViewModels.");
            var viewAssemblyName = viewType.GetTypeInfo().Assembly.FullName;
            var viewModelName = string.Format(CultureInfo.InvariantCulture, "{0}Model, {1}", viewName, viewAssemblyName);

            var viewModelType = Type.GetType(viewModelName);

            if (viewModelType == null)
                return;

            var viewModel = AppContainer.Resolve(viewModelType);
            view.BindingContext = viewModel;
        }
    }
}
