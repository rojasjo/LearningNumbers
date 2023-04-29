using LearningNumber.Core.ViewModels;
using LearningNumbers.Maui.Views;
namespace LearningNumbers.Maui;

public partial class App : Application
{
    public static IMauiContext MauiContext { get; private set; } = default!;
    
    public App()
    {
        InitializeComponent();
    }

    protected override void OnHandlerChanged()
    {
        base.OnHandlerChanged();
        
        MauiContext = Handler.MauiContext;
        MainPage = new NavigationPage(new HomeView(MauiContext.Services.GetService<HomeViewModel>()));
    }
}
