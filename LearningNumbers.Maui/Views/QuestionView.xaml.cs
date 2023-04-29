using LearningNumber.Core.Services;
using LearningNumber.Core.ViewModels;
using Microsoft.Maui.Controls;

namespace LearningNumbers.Maui.Views
{
    public partial class QuestionView : ContentPage
    {
        private readonly QuestionViewModel _viewModel;

        public QuestionConfiguration Configuration { get; set; }
        
        public QuestionView(QuestionViewModel questionViewModel)
        {
            InitializeComponent();

            _viewModel = questionViewModel;
            BindingContext = _viewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            
            _viewModel.OnAppearing(Configuration);
        }

        public BaseViewModel GetViewModel()
        {
            return BindingContext as QuestionViewModel;
        }
    }
}