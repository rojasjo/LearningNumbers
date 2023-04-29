using LearningNumber.Core.Services;
using LearningNumber.Core.ViewModels;
using LearningNumbers.Maui.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.Controls.Hosting;
using Microsoft.Maui.Hosting;

namespace LearningNumbers.Maui;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder.UseMauiApp<App>()
            .ConfigureFonts(fonts => { fonts.AddFont("LuckiestGuy-Regular.ttf", "LuckiestGuy"); });
        
        builder.Services.AddTransient<HomeViewModel>();
        builder.Services.AddTransient<QuestionViewModel>();
        
        builder.Services.AddSingleton<ICalculationGenerator, CalculationGenerator>();
        builder.Services.AddSingleton<IDigitService, DigitService>();
        builder.Services.AddSingleton<INavigationService, NavigationService>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}