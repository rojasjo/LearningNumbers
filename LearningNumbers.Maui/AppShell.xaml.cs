using LearningNumbers.Maui.Views;
using Microsoft.Maui.Controls;

namespace LearningNumbers.Maui;

public partial class AppShell : Shell
{
    public AppShell()
    {
        Routing.RegisterRoute("Home", typeof(HomeView));
        
        InitializeComponent();
    }
}