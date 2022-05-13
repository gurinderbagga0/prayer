
using Android.App;
using Android.Content.PM;
using Android.Views;
using Android.OS;
using PushNotification;
using Android.Content;
using Gcm.Client;
using ThePrayerZone;

namespace DrawerMenu.Droid
{

  
    [Activity(Label = "ThePrayerZone", Icon = "@drawable/logo", Theme = "@style/MyTheme", NoHistory = true , MainLauncher = false, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, WindowSoftInputMode = SoftInput.AdjustResize)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
    {
        //  public static Context AppContext;
        protected override void OnCreate(Bundle bundle)
        {
         
            base.OnCreate(bundle);
            global::Xamarin.Forms.Forms.Init(this, bundle);
            if (string.IsNullOrEmpty(clsCommon.GetString(clsCommon.DeviceId_String)))
            {
               
                GcmClient.Register(this, GcmBroadcastReceiver.SENDER_IDS);

            }
            LoadApplication(new App("",""));

        }
      

    }
}



