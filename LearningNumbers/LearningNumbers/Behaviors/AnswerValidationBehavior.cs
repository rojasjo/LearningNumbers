using System;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace LearningNumbers.Behaviors
{
    public class AnswerValidationBehavior : Behavior<Label>
    {
        private bool isAnimating;

        public Label AssociatedObject { get; private set; }

        public static readonly BindableProperty ShakeProperty =
            BindableProperty.Create(nameof(Shake), typeof(ICommand), typeof(View), null,
                defaultBindingMode: BindingMode.TwoWay);

        public ICommand Shake
        {
            get => (ICommand) GetValue(ShakeProperty);
            set => SetValue(ShakeProperty, value);
        }

        protected override void OnAttachedTo(Label bindable)
        {
            base.OnAttachedTo(bindable);
            AssociatedObject = bindable;
            bindable.BindingContextChanged += OnBindingContextChanged;

            base.OnAttachedTo(bindable);
        }

        protected override void OnDetachingFrom(Label bindable)
        {
            bindable.BindingContextChanged -= OnBindingContextChanged;
            AssociatedObject = null;

            base.OnDetachingFrom(bindable);
        }

        private void ShakeIt()
        {
            if (isAnimating)
                return;

            isAnimating = true;

            Device.BeginInvokeOnMainThread(async () =>
            {
                try
                {
                    var color = AssociatedObject.TextColor;
                    AssociatedObject.TextColor = ColorConverters.FromHex("#dc3545");
                    await AssociatedObject.TranslateTo(-15, 0, 50);
                    await AssociatedObject.TranslateTo(15, 0, 50);
                    await AssociatedObject.TranslateTo(-10, 0, 50);
                    await AssociatedObject.TranslateTo(10, 0, 50);
                    await AssociatedObject.TranslateTo(-5, 0, 50);
                    await AssociatedObject.TranslateTo(5, 0, 50);
                    AssociatedObject.TranslationX = 0;
                    AssociatedObject.TextColor = color;
                }
                finally
                {
                    isAnimating = false;
                }
            });
        }

        private void OnBindingContextChanged(object sender, EventArgs e)
        {
            OnBindingContextChanged();
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            BindingContext = AssociatedObject.BindingContext;

            if (BindingContext != null)
            {
                Shake = new Command(ShakeIt);
            }
        }
    }
}