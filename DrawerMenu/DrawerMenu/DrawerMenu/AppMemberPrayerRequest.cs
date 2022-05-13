using DrawerMenu.Pages;

using PushNotification;
using PushNotification.Plugin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UIKit;
using Xamarin.Forms;

namespace DrawerMenu
{
    public class AppMemberPrayerRequest : Application
    {
        public AppMemberPrayerRequest(string RequestPostedUserId, string PrayerRequestId)
        {

            // The root page of your application
            MainPage = new NavigationPage(new TestNotification(RequestPostedUserId,  PrayerRequestId))
            {
                BarBackgroundColor = Color.FromHex("#D0D0D0"),
                BarTextColor = Color.White

            };
          
           
        }
        protected override void OnStart()
        {
            // Handle when your app starts
            string Platform = Device.RuntimePlatform;
            if (Platform.Contains("Android"))
            {
             //  GcmClient.Register(this, GcmBroadcastReceiver.SENDER_IDS);
             //   CrossPushNotification.Current.Register();
            }
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
