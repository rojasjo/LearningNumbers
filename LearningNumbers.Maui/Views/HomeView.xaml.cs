using LearningNumber.Core.ViewModels;

namespace LearningNumbers.Maui.Views
{
    public partial class HomeView : ContentPage
    {
        public HomeView(HomeViewModel homeViewModel)
        {
            InitializeComponent();
            
            BindingContext = homeViewModel;
        }
    }
}