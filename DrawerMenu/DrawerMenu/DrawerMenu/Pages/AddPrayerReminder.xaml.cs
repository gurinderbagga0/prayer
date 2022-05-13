using DrawerMenu.Core;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace DrawerMenu.Pages
{
    public partial class AddPrayerReminder : ContentPage
    {
        public AddPrayerReminder()
        {
            InitializeComponent();
            stackHeading.Margin = new Thickness(10, 30);
            if (Application.Current.Properties.ContainsKey("EditTimeId"))
            {
                string EditTimeId = Convert.ToString(Application.Current.Properties["EditTimeId"]);
                if (EditTimeId != "" && EditTimeId != string.Empty)
                {
                    GetReminderDetails(EditTimeId);
                }
            }

            //  NavigationPage.SetTitleIcon(this, "logoios.png");
        }
        public async void GetReminderDetails(string EditTimeId)
        {
            Int32 TimeId = Convert.ToInt32(EditTimeId);
            PostNotificationData _PostNotificationData = new PostNotificationData();
            var _data = await _PostNotificationData.GetReminderDetailsTimeId(new PostNotificationDataModel { TimeId = TimeId });

            if (_data.Status == true)
            {
                btnSaveNewReminder.Text = "Update";
                string NotificationTime = Convert.ToString(_data.NotificationTime);
                string DayName = Convert.ToString(_data.DayName);
                if (DayName != null && DayName != String.Empty)
                {
                    DaysPicker.SelectedItem = DayName;
                }
                TimeSpan Pickertime = TimeSpan.Parse(NotificationTime);
                TimePicker.Time = Pickertime;
            }


        }
        async void BackItem_Clicked(object sender, EventArgs e)
        {
            Application.Current.Properties["EditTimeId"] = null;
            var loadingPage = new LoadingPopupPage();
            await Navigation.PushPopupAsync(loadingPage);
            await Navigation.PushModalAsync(new PrayerRemainder());
            await Navigation.RemovePopupPageAsync(loadingPage);
        }
        async void Save_Clicked(object sender, EventArgs e)
        {
            bool Valid = ValidateAllControls();
            if (Valid == true)
            {


                string UserUId = "";
                if (Application.Current.Properties.ContainsKey("UserFirstName") && Application.Current.Properties.ContainsKey("UserLastName"))
                {
                    UserUId = Convert.ToString(Application.Current.Properties["UserUID"]);
                }

                if (Application.Current.Properties.ContainsKey("EditTimeId"))
                {
                    string EditTimeId = Convert.ToString(Application.Current.Properties["EditTimeId"]);
                    if (EditTimeId != "" && EditTimeId != string.Empty)
                    {

                        UpdateReminder(EditTimeId, UserUId);
                    }
                    else
                    {
                        SaveReminder(UserUId);
                    }
                }
                else
                {
                    SaveReminder(UserUId);
                }

            }


        }
        async void UpdateReminder( string EditTimeId, string UserUId)
        {
            Int32 TimeId = Convert.ToInt32(EditTimeId);
            string day = "";
            string time = "";
            string UserType = "";
            if (DaysPicker.SelectedIndex == -1)
            {
                day = "";
            }
            else
            {
                string dayName = DaysPicker.Items[DaysPicker.SelectedIndex];
                day = dayName;
            }
            time = Convert.ToString(TimePicker.Time);
            var loadingPage = new LoadingPopupPage();
            await Navigation.PushPopupAsync(loadingPage);
            PostNotificationData _PostNotificationData = new PostNotificationData();
            var _data = await _PostNotificationData.UpdateReminderNotificationUserDetailsTimeId(new PostNotificationDataModel { DayName = day, NotificationTime = time, TimeId = TimeId, UuID = UserUId });
            await Navigation.RemovePopupPageAsync(loadingPage);
            if (_data.Status == true && _data.Message == "Passsed")
            {
                //  await Navigation.PopAsync();
                Application.Current.Properties["EditTimeId"] = null;
                await Navigation.PushModalAsync(new PrayerRemainder());
            }
            else if (_data.Status == false && _data.Message.Contains("already exists"))
            {
                string msgStatus = "-5";
                await Navigation.PushPopupAsync(new SignUpRequesterFailedPopupPage(msgStatus));
            }
        }
        async void SaveReminder(string UserUId)
        {
            string day = "";
            string time = "";
            string UserType = "";
            if (DaysPicker.SelectedIndex == -1)
            {
                day = "";
            }
            else
            {
                string dayName = DaysPicker.Items[DaysPicker.SelectedIndex];
                day = dayName;
            }
            time = Convert.ToString(TimePicker.Time);
            var loadingPage = new LoadingPopupPage();
            await Navigation.PushPopupAsync(loadingPage);
            PostNotificationData _PostNotificationData = new PostNotificationData();
            var _data = await _PostNotificationData.PostNotificationDetailsUsers(new PostNotificationDataModel { DayName = day, NotificationTime = time, UuID = UserUId });
            await Navigation.RemovePopupPageAsync(loadingPage);
            if (_data.Status == true && _data.Message == "Passsed")
            {
                //  await Navigation.PopAsync();
                await Navigation.PushModalAsync(new PrayerRemainder());
            }
            else if (_data.Status == false && _data.Message.Contains("already exists"))
            {
                string msgStatus = "-5";
                await Navigation.PushPopupAsync(new SignUpRequesterFailedPopupPage(msgStatus));
            }
        }
        public bool ValidateAllControls()
        {

            bool DayRequired = true;
            if (DaysPicker.SelectedIndex == -1)
            {
                DaysPicker.BackgroundColor = (Color.FromHex("#F4C6C9"));
                DayRequired = false;
            }
            else
            {
                DaysPicker.BackgroundColor = (Color.FromHex("#eaeaea"));
                DayRequired = true;
            }

            return DayRequired;
        }
    }
}
