using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using PushNotification.Plugin;
using PushNotification.Plugin.Abstractions;
using System.Diagnostics;
using DrawerMenu.Core;

namespace PushNotification
{
   public class CrossPushNotificationListener: IPushNotificationListener
    {
        //Here you will receive all push notification messages
        //Messages arrives as a dictionary, the device type is also sent in order to check specific keys correctly depending on the platform.
        void IPushNotificationListener.OnMessage(JObject Value, DeviceType deviceType)
        {
            Debug.WriteLine("Message Arrived");
        }
        //Gets the registration token after push registration
        void IPushNotificationListener.OnRegistered(string Token, DeviceType deviceType)
        {
            // code to save Device Token in Database
            if (Token != string.Empty)
            {
                
                string DeviceType = "Android";
                string DeviceId = Convert.ToString(Token);
                PostDeviceToken _PostDeviceToken = new PostDeviceToken();
                var _data = _PostDeviceToken.PostDeviceDetails(new PostDeviceTokenModel { DeviceId = DeviceId, DeviceType = DeviceType });
            }
            Debug.WriteLine(string.Format("Push Notification - Device Registered - Token : {0}", Token));
           
        }
        //Fires when device is unregistered
        void IPushNotificationListener.OnUnregistered(DeviceType deviceType)
        {
            Debug.WriteLine("Push Notification - Device Unnregistered");

        }

        //Fires when error
        void IPushNotificationListener.OnError(string message, DeviceType deviceType)
        {
            Debug.WriteLine(string.Format("Push notification error - {0}", message));
        }

        //Enable/Disable Showing the notification
        bool IPushNotificationListener.ShouldShowNotification()
        {
            return true;
        }
    }

}
