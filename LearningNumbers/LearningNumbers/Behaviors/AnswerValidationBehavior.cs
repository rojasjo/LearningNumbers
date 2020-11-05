using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace LearningNumbers.Behaviors
{
    public class AnswerValidationBehavior : Behavior<Label>
    {
        public static readonly BindableProperty ShakeProperty =
       BindableProperty.Create(nameof(Shake), typeof(ICommand), typeof(View), null, defaultBindingMode: BindingMode.TwoWay);

        public ICommand Shake
        {
            get { return (ICommand)GetValue(ShakeProperty); }
            set { SetValue(ShakeProperty, value); }
        }
        public Label AssociatedObject { get; private set; }

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

        bool _isAnimating = false;

        void ShakeIt()
        {
            if (_isAnimating)
                return;

            _isAnimating = true;

            Device.BeginInvokeOnMainThread(async () =>
            {
                try
                {
                    var color = AssociatedObject.TextColor;
                    AssociatedObject.TextColor = (Color)ColorConverters.FromHex("#dc3545");
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
                    _isAnimating = false;
                }
            });
        }

        void OnBindingContextChanged(object sender, EventArgs e)
        {
            OnBindingContextChanged();
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            BindingContext = AssociatedObject.BindingContext;

            if (BindingContext != null)
            {
                Shake = new Command(() =>
                {
                    ShakeIt();
                });
            }
        }
    }
}
