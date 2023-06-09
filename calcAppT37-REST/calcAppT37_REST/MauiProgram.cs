﻿using calcAppT37_REST.ViewModels;

namespace calcAppT37_REST;


public static class MauiProgram
{
    public static ExpressionHistoryViewModel _expressionHistoryViewModel = new ExpressionHistoryViewModel();
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });
        

        return builder.Build();
    }
}
