using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using NorthwindClient.Common;
using Plugin.Firebase.CloudMessaging;

namespace NorthwindClient;

[Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, LaunchMode = LaunchMode.SingleTop,
    ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode |
                           ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
public class MainActivity : MauiAppCompatActivity
{
    public static string CHANNEL_ID = "com.darrenle.northwind.general";
    public static int NOTIFICATION_ID = 101;

    protected override void OnCreate(Bundle savedInstanceState)
    {
        base.OnCreate(savedInstanceState);
        HandleIntent(Intent);
        CreateNotificationChannelIfNeeded();
    }

    protected override void OnNewIntent(Intent intent)
    {
        base.OnNewIntent(intent);
        HandleIntent(intent);
    }

    private static async void HandleIntent(Intent intent)
    {
        FirebaseCloudMessagingImplementation.OnNewIntent(intent);
        if (intent.Extras != null && intent.Extras.ContainsKey("customerId"))
        {
            var customerId = intent.Extras.GetString("customerId");
            Console.WriteLine($"Customer id = {customerId}");
            if (customerId != null)
                await Shell.Current.GoToAsync(Routes.CustomerDetailPage,
                    new Dictionary<string, object> { { "customerId", customerId } }
                );
        }
    }

    private void CreateNotificationChannelIfNeeded()
    {
        if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
        {
            CreateNotificationChannel();
        }
    }

    private void CreateNotificationChannel()
    {
        var notificationManager = (NotificationManager)GetSystemService(NotificationService);
        var channel = new NotificationChannel(CHANNEL_ID, "General", NotificationImportance.Default);
        notificationManager.CreateNotificationChannel(channel);
        FirebaseCloudMessagingImplementation.ChannelId = CHANNEL_ID;
    }
}