using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using DrawerMenu.Pages;
using DrawerMenu.Core;
using UIKit;
using DrawerMenu.Core.Prayers;
using System.ComponentModel;
using System.Windows.Input;
using System.Runtime.CompilerServices;
using Foundation;
using System.Text.RegularExpressions;

namespace DrawerMenu
{
    public partial class MyNotifications : ContentPage
    {
        public MyNotifications()
        {
            string UserUId = "";
            string Platform = Device.RuntimePlatform;
            if (!Platform.Contains("Android"))
            {
                NavigationPage.SetTitleIcon(this, "logoios.png");
            }
            InitializeComponent();
            if (Application.Current.Properties.ContainsKey("UserFirstName") && Application.Current.Properties.ContainsKey("UserLastName"))
            {
                UserUId = Convert.ToString(Application.Current.Properties["UserUID"]);
                SetReminderSwitch(UserUId);
                SetNotificationsSwitch(UserUId);
                SetPrayerUpdateNotificationSwitch(UserUId);
            }
        }
        public async void SetPrayerUpdateNotificationSwitch(string UserUId)
        {
            if (UserUId != string.Empty)
            {
                PostDeviceToken objDeviceTokenData = new PostDeviceToken();
                List<PostDeviceTokenModel> _list = new List<PostDeviceTokenModel>();
                _list = await objDeviceTokenData.getPrayerUpdateNotificationEnableDisable(UserUId);
                if (_list != null)
                {
                    if (_list.Count > 0)
                    {
                        Application.Current.Properties["PrayerUpdateNotificationUserId"] = UserUId;
                        Application.Current.Properties["PrayerUpdateNotificationToggled"] = "0";
                        // PushNotificationSwitch.IsVisible = true;
                        PrayerUpdateNotificationSwitch.IsToggled = true;
                    }
                    else
                    {
                        Application.Current.Properties["PrayerUpdateNotificationUserId"] = UserUId;
                        Application.Current.Properties["PrayerUpdateNotificationToggled"] = "0";
                        // PushNotificationSwitch.IsVisible = false;
                        PrayerUpdateNotificationSwitch.IsToggled = false;
                    }
                }
                else
                {
                    Application.Current.Properties["PrayerUpdateNotificationUserId"] = UserUId;
                    Application.Current.Properties["PrayerUpdateNotificationToggled"] = "0";
                    //  PushNotificationSwitch.IsVisible = false;
                    PrayerUpdateNotificationSwitch.IsToggled = false;
                }
            }
            Application.Current.Properties["PrayerUpdateNotificationUserId"] = UserUId;
            Application.Current.Properties["PrayerUpdateNotificationToggled"] = "1";

        }


        public async void PrayerUpdateNotificationSwitch_Toggled(object sender, ToggledEventArgs e)
        {

            string PrayerUpdateNotificationToggled = "";
            string PrayerUpdateNotificationUserId = "";
            if (Application.Current.Properties.ContainsKey("PrayerUpdateNotificationToggled") && (Application.Current.Properties.ContainsKey("PrayerUpdateNotificationUserId")))
            {
                PrayerUpdateNotificationToggled = Convert.ToString(Application.Current.Properties["PrayerUpdateNotificationToggled"]);
                PrayerUpdateNotificationUserId = Convert.ToString(Application.Current.Properties["PrayerUpdateNotificationUserId"]);
            }
            if (e.Value == true && PrayerUpdateNotificationToggled != "0")
            {
                PostNotificationData _PostNotificationUserData = new PostNotificationData();
                var _data = await _PostNotificationUserData.UpdatePrayerUpdateNotificationDetailsUsers(new PostNotificationDataModel { IsPrayerUpdateNotificationEnable = 1, UuID = PrayerUpdateNotificationUserId });
                // await Navigation.PushAsync(new PrayerRemainder());
            }
            else if (e.Value == false && PrayerUpdateNotificationToggled != "0")
            {
                var answer = await DisplayAlert("Question?", "Would you like to Set Prayer Update Notifications as OFF", "Yes", "No");
                if (answer)
                {
                    // if yes make all user alarm Inactive
                    PostNotificationData _PostNotificationUserData = new PostNotificationData();
                    var _data = await _PostNotificationUserData.UpdatePrayerUpdateNotificationDetailsUsers(new PostNotificationDataModel { IsPrayerUpdateNotificationEnable = 0, UuID = PrayerUpdateNotificationUserId });
                }
                else
                {
                    // if No make all user alarm Active
                    Application.Current.Properties["PrayerUpdateNotificationToggled"] = "0";
                    PrayerUpdateNotificationSwitch.IsToggled = true;
                }
            }
            Application.Current.Properties["PrayerUpdateNotificationToggled"] = "1";

           

        }



        public async void SetNotificationsSwitch(string UserUId)
        {
            if (UserUId != string.Empty)
            {
                PostDeviceToken objDeviceTokenData = new PostDeviceToken();
                List<PostDeviceTokenModel> _list = new List<PostDeviceTokenModel>();
                _list = await objDeviceTokenData.getNotificationEnableDisable(UserUId);
                if (_list != null)
                {
                    if (_list.Count > 0)
                    {
                        Application.Current.Properties["PushNutificationsUserId"] = UserUId;
                        Application.Current.Properties["PushNutificationToggled"] = "0";
                        // PushNotificationSwitch.IsVisible = true;
                        PushNotificationSwitch.IsToggled = true;
                    }
                    else
                    {
                        Application.Current.Properties["PushNutificationsUserId"] = UserUId;
                        Application.Current.Properties["PushNutificationToggled"] = "0";
                        // PushNotificationSwitch.IsVisible = false;
                        PushNotificationSwitch.IsToggled = false;
                    }
                }
                else
                {
                    Application.Current.Properties["PushNutificationsUserId"] = UserUId;
                    Application.Current.Properties["PushNutificationToggled"] = "0";
                    //  PushNotificationSwitch.IsVisible = false;
                    PushNotificationSwitch.IsToggled = false;
                }
            }
            Application.Current.Properties["PushNutificationsUserId"] = UserUId;
            Application.Current.Properties["PushNutificationToggled"] = "1";

        }

        public async void PushNotificationSwitch_Toggled(object sender, ToggledEventArgs e)
        {
            string PushNutificationToggled = "";
            string PushNutificationsUserId = "";
            if (Application.Current.Properties.ContainsKey("PushNutificationToggled") && (Application.Current.Properties.ContainsKey("PushNutificationsUserId")))
            {
                PushNutificationToggled = Convert.ToString(Application.Current.Properties["PushNutificationToggled"]);
                PushNutificationsUserId = Convert.ToString(Application.Current.Properties["PushNutificationsUserId"]);
            }
            if (e.Value == true && PushNutificationToggled != "0")
            {
                PostDeviceToken _PostNotificationUserData = new PostDeviceToken();
                var _data = await _PostNotificationUserData.UpdatePushNotificationDetailsUsers(new PostDeviceTokenModel { IsNotificationEnable = 1, UuID = PushNutificationsUserId });
                // await Navigation.PushAsync(new PrayerRemainder());
            }
            else if (e.Value == false && PushNutificationToggled != "0")
            {
                var answer = await DisplayAlert("Question?", "Would you like to Set Push Notifications as OFF", "Yes", "No");
                if (answer)
                {
                    // if yes make all user alarm Inactive
                    PostDeviceToken _PostNotificationUserData = new PostDeviceToken();
                    var _data = await _PostNotificationUserData.UpdatePushNotificationDetailsUsers(new PostDeviceTokenModel { IsNotificationEnable = 0, UuID = PushNutificationsUserId });
                    //PostDeviceToken _PostDeviceToken = new PostDeviceToken();
                    //var _data = _PostDeviceToken.UpdateDeviceDetails(new UpdateDeviceTokenModel { DeviceId = DeviceId });
                }
                else
                {
                    // if No make all user alarm Active
                    Application.Current.Properties["PushNutificationToggled"] = "0";
                    PushNotificationSwitch.IsToggled = true;
                }
            }
            Application.Current.Properties["PushNutificationToggled"] = "1";

        }

        public async void SetReminderSwitch(string UserUId)
        {
            if (UserUId != string.Empty)
            {
                PostNotificationData objNotificationData = new PostNotificationData();
                List<PostNotificationDataModel> _list = new List<PostNotificationDataModel>();
                _list = await objNotificationData.getAllNotificationDataUserTime(UserUId);
                if (_list != null)
                {
                    if (_list.Count > 0)
                    {
                        Application.Current.Properties["PrayerReminderUserId"] = UserUId;
                        Application.Current.Properties["Toggled"] = "0";
                        SwitchRemainder.IsVisible = true;
                        SetSwitchRemainder.IsToggled = true;

                    }
                    else
                    {
                        SwitchRemainder.IsVisible = false;
                        Application.Current.Properties["PrayerReminderUserId"] = UserUId;
                        Application.Current.Properties["Toggled"] = "0";
                        SetSwitchRemainder.IsToggled = false;
                    }
                }
                else
                {
                    SwitchRemainder.IsVisible = false;
                    Application.Current.Properties["PrayerReminderUserId"] = UserUId;
                    Application.Current.Properties["Toggled"] = "0";
                    SetSwitchRemainder.IsToggled = false;
                }
            }
            Application.Current.Properties["PrayerReminderUserId"] = UserUId;
            Application.Current.Properties["Toggled"] = "1";
        }
        public async void SetSwitchswitcher_Toggled(object sender, ToggledEventArgs e)
        {
            string Toggled = "";
            string PrayerReminderUserId = "";
            if (Application.Current.Properties.ContainsKey("Toggled") && (Application.Current.Properties.ContainsKey("PrayerReminderUserId")))
            {
                Toggled = Convert.ToString(Application.Current.Properties["Toggled"]);
                PrayerReminderUserId = Convert.ToString(Application.Current.Properties["PrayerReminderUserId"]);
            }
            if (e.Value == true && Toggled != "0")
            {
                PostNotificationData _PostNotificationUserData = new PostNotificationData();
                var _data = await _PostNotificationUserData.UpdateReminderNotificationDetailsUsers(new PostNotificationDataModel { IsReminderEnable = 1, UuID = PrayerReminderUserId });
                var loadingPage = new LoadingPopupPage();
                await Navigation.PushPopupAsync(loadingPage);
                await Navigation.PushModalAsync(new PrayerRemainder());
                await Navigation.RemovePopupPageAsync(loadingPage);
            }
            else if (e.Value == false && Toggled != "0")
            {
                var answer = await DisplayAlert("Question?", "Would you like to Set Prayer Reminders as OFF", "Yes", "No");
                if (answer)
                {
                    SwitchRemainder.IsVisible = false;
                    // if yes make all user alarm Inactive
                    PostNotificationData _PostNotificationUserData = new PostNotificationData();
                    var _data = await _PostNotificationUserData.UpdateReminderNotificationDetailsUsers(new PostNotificationDataModel { IsReminderEnable = 0, UuID = PrayerReminderUserId });
                    //PostDeviceToken _PostDeviceToken = new PostDeviceToken();
                    //var _data = _PostDeviceToken.UpdateDeviceDetails(new UpdateDeviceTokenModel { DeviceId = DeviceId });
                }
                else
                {
                    // if No make all user alarm Active
                    Application.Current.Properties["Toggled"] = "0";
                    SetSwitchRemainder.IsToggled = true;
                }
            }
            Application.Current.Properties["Toggled"] = "1";

        }
        private async void SetPrayerRemindersClicked(object sender, EventArgs e)
        {
            // await Navigation.PushAsync(new PrayerRemainder());
            var loadingPage = new LoadingPopupPage();
            await Navigation.PushPopupAsync(loadingPage);
            await Navigation.PushModalAsync(new PrayerRemainder());
            await Navigation.RemovePopupPageAsync(loadingPage);
        }




    }
}
