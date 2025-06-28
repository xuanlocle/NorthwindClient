// using Android.App;
// using AndroidX.Core.App;
// using Firebase.Messaging;
//
// namespace NorthwindClient.Services;
//
// [Service(Exported = true)]
// [IntentFilter(new[] { "com.google.firebase.MESSAGING_EVENT" })]
// public class FirebaseService : FirebaseMessagingService
// {
//     public override void OnMessageReceived(RemoteMessage message)
//     {
//         base.OnMessageReceived(message);
//         // Log message data when the app is in the background
//         if (message.Data != null && message.Data.Count > 0)
//         {
//             foreach (var key in message.Data.Keys)
//             {
//                 // You can access the data like this
//                 var dataValue = message.Data[key];
//                 Console.WriteLine($"Key: {key}, Value: {dataValue}");
//             }
//
//             // You can call SendNotification to show a notification
//             SendNotification("New Message", "You have received a new message.", message.Data);
//         }
//     }
//
//     private void SendNotification(string title, string body, IDictionary<string, string> data)
//     {
//         // Notification logic here (showing notification to user)
//         var notificationBuilder = new NotificationCompat.Builder(this, MainActivity.CHANNEL_ID)
//             .SetContentTitle(title)
//             .SetSmallIcon(Resource.Mipmap.appicon)
//             .SetContentText(body)
//             .SetPriority(NotificationCompat.PriorityHigh);
//
//         var notificationManager = NotificationManagerCompat.From(this);
//         notificationManager.Notify(0, notificationBuilder.Build());
//     }
// }