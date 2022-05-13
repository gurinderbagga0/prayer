using System.Text;
using Android.App;
using Android.Content;
using Android.Util;
using Gcm.Client;

//VERY VERY VERY IMPORTANT NOTE!!!!
// Your package name MUST NOT start with an uppercase letter.
// Android does not allow permissions to start with an upper case letter
// If it does you will get a very cryptic error in logcat and it will not be obvious why you are crying!
// So please, for the love of all that is kind on this earth, use a LOWERCASE first letter in your Package Name!!!!
using System;
using Newtonsoft.Json;
using DrawerMenu.Core;
using DrawerMenu.Droid;
using Android.Support.V4.App;
using ThePrayerZone;
using Android.Media;
using static Android.Media.Audiofx.EnvironmentalReverb;

namespace PushNotification
{
    //You must subclass this!
    [BroadcastReceiver(Permission = Gcm.Client.Constants.PERMISSION_GCM_INTENTS)]
    [IntentFilter(new string[] { Gcm.Client.Constants.INTENT_FROM_GCM_MESSAGE }, Categories = new string[] { "@PACKAGE_NAME@" })]
    [IntentFilter(new string[] { Gcm.Client.Constants.INTENT_FROM_GCM_REGISTRATION_CALLBACK }, Categories = new string[] { "@PACKAGE_NAME@" })]
    [IntentFilter(new string[] { Gcm.Client.Constants.INTENT_FROM_GCM_LIBRARY_RETRY }, Categories = new string[] { "@PACKAGE_NAME@" })]
    public class GcmBroadcastReceiver : GcmBroadcastReceiverBase<PushHandlerService>
    {
        //IMPORTANT: Change this to your own Sender ID!
        //The SENDER_ID is your Google API Console App Project ID.
        //  Be sure to get the right Project ID from your Google APIs Console.  It's not the named project ID that appears in the Overview,
        //  but instead the numeric project id in the url: eg: https://code.google.com/apis/console/?pli=1#project:785671162406:overview
        //  where 785671162406 is the project id, which is the SENDER_ID to use!
        public static string[] SENDER_IDS = new string[] { "210827963370" };

        public const string TAG = "PushSharp-GCM";
    }

    [Service] //Must use the service tag
    public class PushHandlerService : GcmServiceBase
    {
        public PushHandlerService() : base(GcmBroadcastReceiver.SENDER_IDS)
        {
        }

        const string TAG = "GCM-SAMPLE";
        public static string PREFS_NAME = "ThePrayter_PrefsFile";
        public static String DeviceId_String = "DeviceId";


        protected override void OnRegistered(Context context, string registrationId)
        {
            Log.Verbose(TAG, "GCM Registered: " + registrationId);
            if (registrationId != string.Empty)
            {

                AlertDialog.Builder builder = new AlertDialog.Builder(context);
                string SerialAndroid = Android.OS.Build.Serial;
                string DeviceType = "Android";
                string DeviceId = Convert.ToString(registrationId);
                PostDeviceToken _PostDeviceToken = new PostDeviceToken();
                var _data = _PostDeviceToken.PostDeviceDetails(new PostDeviceTokenModel { DeviceId = DeviceId, DeviceType = DeviceType, DeviceSerialNo = SerialAndroid });
            }
        }

        protected override void OnUnRegistered(Context context, string registrationId)
        {
            Log.Verbose(TAG, "GCM Unregistered: " + registrationId);
        }

        protected override void OnMessage(Context context, Intent intent)
        {
            try
            {
                Log.Info(TAG, "GCM Message Received!");

                var msg = new StringBuilder();

                if (intent != null && intent.Extras != null)
                {
                    foreach (var key in intent.Extras.KeySet())
                        msg.AppendLine(key + "=" + intent.Extras.Get(key).ToString());
                }
                //  string postData = "collapse_key=score_update&time_to_live=108&delay_while_idle=1&data.message=" + value + "&data.time=" + System.DateTime.Now.ToString() + "&registration_id=" + deviceId + "&data.RequestPostedUserId=" + RequestPostedUserId + "&data.PrayerRequestId=" + PrayerRequestId + "&data.userid=" + userid;
                string message = intent.Extras.GetString("message");
                string RequestPostedUserId = intent.Extras.GetString("RequestPostedUserId");
                string PrayerRequestId = intent.Extras.GetString("PrayerRequestId");
                string UserUId = intent.Extras.GetString("userid");
                string UserLoggedIn = "0";
                if (UserUId != string.Empty && UserUId != null)
                {
                    if ((RequestPostedUserId != null && RequestPostedUserId != string.Empty) && (PrayerRequestId != null && PrayerRequestId != string.Empty))
                    {
                        User _user = new User();
                        var _data = _user.GetUserLoggedIn(new UserModel { UserUID = UserUId });
                        UserLoggedIn = Convert.ToString(_data.Result.IsUserLoggedIn);
                        if (_data.Result.Status == true && UserLoggedIn == "0")
                        {
                            UserLoggedIn = "0";
                        }
                        else if (_data.Result.Status == true && UserLoggedIn == "1")
                        {
                            UserLoggedIn = "1";
                        }
                    }
                }
                string title = "ThePrayerZone";
                string body = message;

                SendNotification(title, body, RequestPostedUserId, PrayerRequestId, UserUId, UserLoggedIn);
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }

        }
        protected override bool OnRecoverableError(Context context, string errorId)
        {
            Log.Warn(TAG, "Recoverable Error: " + errorId);

            return base.OnRecoverableError(context, errorId);
        }

        protected override void OnError(Context context, string errorId)
        {
            Log.Error(TAG, "GCM Error: " + errorId);
        }
        void SendNotification(string title, string body, string RequestPostedUserId, string PrayerRequestId, string UserUId, string UserLoggedIn)
        {
            try
            {
                Context context = this;

                if ((RequestPostedUserId != null && RequestPostedUserId != string.Empty) && (PrayerRequestId != null && PrayerRequestId != string.Empty))
                {
                    Intent uiIntent = new Intent(this, typeof(NotificationActivity));
                    uiIntent.PutExtra("title", title);
                    uiIntent.PutExtra("body", body);
                    uiIntent.PutExtra("RequestPostedUserId", RequestPostedUserId);
                    uiIntent.PutExtra("PrayerRequestId", PrayerRequestId);
                    uiIntent.PutExtra("UserUId", UserUId);
                    uiIntent.PutExtra("UserLoggedIn", UserLoggedIn);
                    // uiIntent.SetFlags(ActivityFlags.ClearTop);
                    Random rcode = new Random();
                    int ResultCode = rcode.Next();
                    var pendingIntent = PendingIntent.GetActivity(context, ResultCode, uiIntent, PendingIntentFlags.OneShot | PendingIntentFlags.UpdateCurrent);
                    var notificationBuilder = new NotificationCompat.Builder(context)
                   .SetSmallIcon(ThePrayerZone.Resource.Drawable.icon)
                   .SetContentTitle(title)
                    .SetSound(RingtoneManager.GetDefaultUri(RingtoneType.Notification))
                  //  .SetVibrate(new long[] { 1000, 1000, 1000, 1000, 1000 })
                    .SetContentText(body)
                    .SetAutoCancel(true)
                    .SetContentIntent(pendingIntent);
                    if (Android.OS.Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.JellyBean)
                    {
                        // Using BigText notification style to support long message
                        var style = new NotificationCompat.BigTextStyle();
                        style.BigText(body);
                        notificationBuilder.SetStyle(style);
                    }

                    NotificationManager notificationManager = (NotificationManager)context.GetSystemService(Context.NotificationService);
                    try
                    {
                        Random rnd = new Random();
                        int rndnumber = rnd.Next();
                        Guid obj = Guid.NewGuid();
                        string tag = obj.ToString().Substring(0, 8);
                        notificationManager.Notify(tag, rndnumber, notificationBuilder.Build());
                    }
                    catch
                    {

                    }

                }
                else
                {
                    Intent uiIntent = new Intent(this, typeof(SplashActivity));
                    uiIntent.PutExtra("title", title);
                    uiIntent.PutExtra("body", body);
                    //  uiIntent.SetFlags(ActivityFlags.ClearTop);
                    Random rcode = new Random();
                    int ResultCode = rcode.Next();
                    var pendingIntent = PendingIntent.GetActivity(context, ResultCode, uiIntent, PendingIntentFlags.OneShot);
                    var notificationBuilder = new NotificationCompat.Builder(context)
                   .SetSmallIcon(ThePrayerZone.Resource.Drawable.icon)
                   .SetContentTitle(title)
                    .SetSound(RingtoneManager.GetDefaultUri(RingtoneType.Notification))
                  //  .SetVibrate(new long[] { 1000, 1000, 1000, 1000, 1000 })
                    .SetContentText(body)
                     .SetAutoCancel(true)
                    .SetContentIntent(pendingIntent);
                    if (Android.OS.Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.JellyBean)
                    {
                        // Using BigText notification style to support long message
                        var style = new NotificationCompat.BigTextStyle();
                        style.BigText(body);
                        notificationBuilder.SetStyle(style);
                    }

                    NotificationManager notificationManager = (NotificationManager)context.GetSystemService(Context.NotificationService);
                    try
                    {
                        Random rnd = new Random();
                        int rndnumber = rnd.Next();
                        Guid obj = Guid.NewGuid();
                        string tag = obj.ToString().Substring(0, 8);
                        notificationManager.Notify(tag, rndnumber, notificationBuilder.Build());
                    }
                    catch
                    {

                    }
                }
            }
            catch (Exception ex)
            {
                string msg = ex.StackTrace;
            }
        }

    }

}

