using System;
using Xamarin.Forms;

namespace LearningNumbers.CustomElements
{
    public class ToggleButton : Button
    {
        public bool IsToggled
        {
            set => SetValue(IsToggledProperty, value);
            get => (bool) GetValue(IsToggledProperty);
        }

        public static readonly BindableProperty IsToggledProperty =
            BindableProperty.Create(nameof(IsToggled), typeof(bool), typeof(ToggleButton), false,
                propertyChanged: OnIsToggledChanged);

        public ToggleButton()
        {
            Clicked += UpdateIsToggled;
        }

        private void UpdateIsToggled(object sender, EventArgs e)
        {
            IsToggled ^= true;
        }

        protected override void OnParentSet()
        {
            base.OnParentSet();
            
            VisualStateManager.GoToState(this, "ToggledOff");
        }

        static void OnIsToggledChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var toggleButton = (ToggleButton) bindable;
            var isToggled = (bool) newValue;

            VisualStateManager.GoToState(toggleButton, isToggled ? "ToggledOn" : "ToggledOff");
        }
    }
}