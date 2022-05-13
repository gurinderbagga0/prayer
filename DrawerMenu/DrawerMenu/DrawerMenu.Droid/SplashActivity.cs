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

namespace DrawerMenu.Droid
{
    [Activity(Label = "ThePrayerZone", Icon = "@drawable/icon", Theme = "@style/splashscreen", MainLauncher = true, NoHistory = true, WindowSoftInputMode = SoftInput.AdjustResize)]
    public class SplashActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
           
            base.OnCreate(savedInstanceState);
            StartActivity(typeof(MainActivity));
            // Create your application here
        }
    }
}