using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using UserNotifications;
using Firebase.CloudMessaging;
using DrawerMenu.Core;
using System.Threading;
using DrawerMenu.Pages;
using System.Threading.Tasks;

namespace DrawerMenu.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate, IUNUserNotificationCenterDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //

        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {

            global::Xamarin.Forms.Forms.Init();
            KeyboardOverlap.Forms.Plugin.iOSUnified.KeyboardOverlapRenderer.Init();
            UINavigationBar.Appearance.TintColor = Color.Black.ToUIColor();
            LoadApplication(new App("", ""));
            if (UIDevice.CurrentDevice.CheckSystemVersion(10, 0))
            {
                // iOS 10
                var authOptions = UNAuthorizationOptions.Alert | UNAuthorizationOptions.Badge | UNAuthorizationOptions.Sound;
                UNUserNotificationCenter.Current.RequestAuthorization(authOptions, (granted, error) =>
                {
                    Console.WriteLine(granted);
                });

                // For iOS 10 display notification (sent via APNS)
                UNUserNotificationCenter.Current.Delegate = this;
            }
            else
            {
                var allNotificationTypes = UIUserNotificationType.Alert | UIUserNotificationType.Badge | UIUserNotificationType.Sound;
                var settings = UIUserNotificationSettings.GetSettingsForTypes(allNotificationTypes, null);
                UIApplication.SharedApplication.RegisterUserNotificationSettings(settings);
            }

            UIApplication.SharedApplication.RegisterForRemoteNotifications();

            // Firebase component initialize
            Firebase.Analytics.App.Configure();

            Firebase.InstanceID.InstanceId.Notifications.ObserveTokenRefresh((sender, e) =>
            {
                var newToken = Firebase.InstanceID.InstanceId.SharedInstance.Token;
                // if you want to send notification per user, use this token
                string SerialIos = UIKit.UIDevice.CurrentDevice.IdentifierForVendor.AsString();
                // Save the device token in database
                if (newToken != null && newToken != string.Empty)
                {
                    string DeviceType = "Iphone";
                    string DeviceId = Convert.ToString(newToken);
                    PostDeviceToken _PostDeviceToken = new PostDeviceToken();
                    var _data = _PostDeviceToken.PostDeviceDetails(new PostDeviceTokenModel { DeviceId = DeviceId, DeviceType = DeviceType, DeviceSerialNo = SerialIos });

                }
                System.Diagnostics.Debug.WriteLine(newToken);
                connectFCM();
            });
            
            return base.FinishedLaunching(app, options);
        }
        
        public override void OnActivated(UIApplication uiApplication)
        {
            var Token = Firebase.InstanceID.InstanceId.SharedInstance.Token;

            UIUserNotificationType types = UIApplication.SharedApplication.CurrentUserNotificationSettings.Types;
            if (types.HasFlag(UIUserNotificationType.Alert))
            {
                // active
            }
            else
            {
                if (Token != null && Token != string.Empty)
                {
                    string DeviceType = "Iphone";
                    string DeviceId = Convert.ToString(Token);
                    PostDeviceToken _PostDeviceToken = new PostDeviceToken();
                    var _data = _PostDeviceToken.UpdateDeviceDetails(new UpdateDeviceTokenModel { DeviceId = DeviceId });
                }
                // not active
            }
            connectFCM();
            base.OnActivated(uiApplication);
        }

        public override void RegisteredForRemoteNotifications(UIApplication application, NSData deviceToken)
        {
#if DEBUG
            Firebase.InstanceID.InstanceId.SharedInstance.SetApnsToken(deviceToken, Firebase.InstanceID.ApnsTokenType.Sandbox);
#endif
#if RELEASE
			Firebase.InstanceID.InstanceId.SharedInstance.SetApnsToken(deviceToken, Firebase.InstanceID.ApnsTokenType.Prod);
#endif
        }

        public override void DidEnterBackground(UIApplication uiApplication)
        {
            Messaging.SharedInstance.Disconnect();
            // Thread.CurrentThread.Abort();
        }

        // iOS 9 <=, fire when recieve notification foreground
        public override void DidReceiveRemoteNotification(UIApplication application, NSDictionary userInfo, Action<UIBackgroundFetchResult> completionHandler)
        {
            // await Task.Delay(200);
            Messaging.SharedInstance.AppDidReceiveMessage(userInfo);
            // Generate custom event
            NSString[] keys = { new NSString("Event_type") };
            NSObject[] values = { new NSString("Recieve_Notification") };
            var parameters = NSDictionary<NSString, NSObject>.FromObjectsAndKeys(keys, values, keys.Length);
            // Send custom event
            Firebase.Analytics.Analytics.LogEvent("CustomEvent", parameters);

            if (UIApplication.SharedApplication.ApplicationState.Equals(UIApplicationState.Active))
            {
                //App is in foreground. Act on it.
                System.Diagnostics.Debug.WriteLine(userInfo);
                var aps_d = userInfo["aps"] as NSDictionary;
                var alert_d = aps_d["alert"] as NSDictionary;
                var body = alert_d["body"] as NSString;
                var title = alert_d["title"] as NSString;
                var RequestPostedUserId = userInfo["RequestPostedUserId"] as NSString;
                var PrayerRequestId = userInfo["PrayerRequestId"] as NSString;
                var UserUId = userInfo["UserUId"] as NSString;
                string UserLoggedIn = "0";
                if (UserUId != string.Empty && UserUId != null)
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
                debugAlert(title, body, RequestPostedUserId, PrayerRequestId, UserUId, UserLoggedIn);
            }
            else
            {
                System.Diagnostics.Debug.WriteLine(userInfo);
                var aps_d = userInfo["aps"] as NSDictionary;
                var alert_d = aps_d["alert"] as NSDictionary;
                var body = alert_d["body"] as NSString;
                var title = alert_d["title"] as NSString;
                var RequestPostedUserId = userInfo["RequestPostedUserId"] as NSString;
                var PrayerRequestId = userInfo["PrayerRequestId"] as NSString;
                var UserUId = userInfo["UserUId"] as NSString;
                string UserLoggedIn = "0";
                if (UserUId != string.Empty && UserUId != null)
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
               
                if ((RequestPostedUserId != null && RequestPostedUserId != string.Empty) && (PrayerRequestId != null && PrayerRequestId != string.Empty))
                {
                    debugAlert(title, body, RequestPostedUserId, PrayerRequestId, UserUId, UserLoggedIn);
                }
            }
        }
        // iOS 10, fire when recieve notification foreground
        [Export("userNotificationCenter:willPresentNotification:withCompletionHandler:")]
        public async void WillPresentNotification(UNUserNotificationCenter center, UNNotification notification, Action<UNNotificationPresentationOptions> completionHandler)
        {
            var title = notification.Request.Content.Title;
            var body = notification.Request.Content.Body;
            var AllData = notification.Request.Content.UserInfo;
            var RequestPostedUserId = AllData["RequestPostedUserId"] as NSString;
            var PrayerRequestId = AllData["PrayerRequestId"] as NSString;
            var UserUId = AllData["UserUId"] as NSString;
            string UserLoggedIn = "0";
            if (UserUId != string.Empty && UserUId != null)
            {
                User _user = new User();
                var _data = await _user.GetUserLoggedIn(new UserModel { UserUID = UserUId });
                if (_data.Status == true && _data.IsUserLoggedIn == "0")
                {
                    UserLoggedIn = "0";
                }
                else if (_data.Status == true && _data.IsUserLoggedIn == "1")
                {
                    UserLoggedIn = "1";
                }
            }


            debugAlert(title, body, RequestPostedUserId, PrayerRequestId, UserUId, UserLoggedIn);

        }

        private void connectFCM()
        {
            Messaging.SharedInstance.Connect((error) =>
            {
                if (error == null)
                {
                    //TODO: Change Topic to what is required
                    Messaging.SharedInstance.Subscribe("/topics/all");
                }
                System.Diagnostics.Debug.WriteLine(error != null ? "error occured" : "connect success");
            });
        }

        private void debugAlert(string title, string message, string RequestPostedUserId, string PrayerRequestId, string UserUId, string UserLoggedIn)
        {

            var alert = new UIAlertView(title ?? "Title", message ?? "Message", null, "Cancel", "OK");
            alert.Show();

            alert.Clicked += (sender, args) =>
            {
                if ((RequestPostedUserId != null && RequestPostedUserId != string.Empty) && (PrayerRequestId != null && PrayerRequestId != string.Empty))
                {
                    // check if the user NOT pressed the cancel button
                    if (args.ButtonIndex != alert.CancelButtonIndex)
                    {
                        if ((RequestPostedUserId != null && RequestPostedUserId != string.Empty) && (PrayerRequestId != null && PrayerRequestId != string.Empty))
                        {
                            if (UserLoggedIn == "0")
                            {
                                App.Current.MainPage = new MainPage(RequestPostedUserId, PrayerRequestId);
                            }
                            else
                            {
                                App.Current.MainPage = new MenuPageMemberRequest(RequestPostedUserId, PrayerRequestId);
                            }

                        }

                    }
                }
            };
        }

    }
}