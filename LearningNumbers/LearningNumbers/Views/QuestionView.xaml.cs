using LearningNumbers.Services;
using LearningNumbers.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LearningNumbers.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class QuestionView : ContentPage, IView
    {
        public QuestionView()
        {
            InitializeComponent();
        }

        public BaseViewModel GetViewModel()
        {
            return BindingContext as QuestionViewModel;
        }
    }
}