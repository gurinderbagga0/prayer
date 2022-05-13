using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Content.PM;

namespace DrawerMenu.Droid
{
    [Activity(Label = "ThePrayerZone", Icon = "@drawable/logo", Theme = "@style/MyTheme", NoHistory = true, MainLauncher = false, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, WindowSoftInputMode = SoftInput.AdjustResize)]
    public class NotificationActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            String RequestPostedUserId = Intent.GetStringExtra("RequestPostedUserId");
            String PrayerRequestId = Intent.GetStringExtra("PrayerRequestId");
            LoadApplication(new App(RequestPostedUserId, PrayerRequestId));

            // Create your application here
        }
    }
}