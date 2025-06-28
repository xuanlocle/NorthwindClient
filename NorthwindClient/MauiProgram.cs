using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.LifecycleEvents;
using Mopups.Hosting;
using NorthwindClient.Infrastructure;
using NorthwindClient.Services;
using NorthwindClient.ViewModels;
using NorthwindClient.Views;
using ZXing.Net.Maui.Controls;
using Plugin.Firebase.CloudMessaging;
#if ANDROID
using Plugin.Firebase.Core.Platforms.Android;
#elif IOS
using Plugin.Firebase.Core.Platforms.iOS;
#endif

namespace NorthwindClient;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .RegisterFirebaseServices()
            .UseMauiCommunityToolkit()
            .UseBarcodeReader()
            .ConfigureMopups()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("MaterialIcons-Regular.ttf", "MaterialIcons");
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        builder.Services.AddSingleton(sp =>
            new HttpClient
            {
// #if ANDROID
                BaseAddress = new Uri("http://10.0.2.2:5100/api/")
// #elif IOS
                // BaseAddress = new Uri("http://localhost:5100/api/")
// #endif
            });

        builder.Services.AddSingleton<INavigationService, NavigationService>();
        builder.Services.AddSingleton<IRouteRegistrar, RouteRegistrar>();

        builder.Services.AddTransient<IApiService, ApiService>();
        builder.Services.AddTransient<CustomerViewModel>();
        builder.Services.AddTransient<CustomerDetailsViewModel>();
        builder.Services.AddTransient<NewOrderViewModel>();

        builder.Services.AddTransient<CustomerPage>();
        builder.Services.AddTransient<CustomerDetailPage>();
        builder.Services.AddTransient<NewOrderPage>();

        builder.Services.AddSingleton<AppShell>();


#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }

    private static MauiAppBuilder RegisterFirebaseServices(this MauiAppBuilder builder)
    {
        builder.ConfigureLifecycleEvents(events =>
        {
#if IOS
            events.AddiOS(iOS => iOS.WillFinishLaunching((_, __) =>
            {
                CrossFirebase.Initialize();
                FirebaseCloudMessagingImplementation.Initialize();
                return false;
            }));
#elif ANDROID
                events.AddAndroid(android => android.OnCreate((activity, _) =>
                CrossFirebase.Initialize(activity)));
#endif
        });

        return builder;
    }
}