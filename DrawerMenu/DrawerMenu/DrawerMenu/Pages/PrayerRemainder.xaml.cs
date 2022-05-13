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
    public partial class PrayerRemainder : ContentPage
    {
        public PrayerRemainder()
        {
            InitializeComponent();
            stackHeading.Margin = new Thickness(10, 30);
            //  NavigationPage.SetTitleIcon(this, "logoios.png");
            BindNotificationTime();
        }

        //void OnoptTextMessageTapped(object sender, EventArgs args)
        //{
        private async void OnEditTimeClicked(object sender, EventArgs e)
        {
            var item = (Xamarin.Forms.Button)sender;
            string TimeId = Convert.ToString(item.CommandParameter.ToString());
            //var answer = await DisplayAlert("Question?", "Would you like Edit this reminder", "Yes", "No");
            //if (answer)
            //{
                Application.Current.Properties["EditTimeId"] = TimeId;
                var loadingPage = new LoadingPopupPage();
                await Navigation.PushPopupAsync(loadingPage);
                await Navigation.PushModalAsync(new AddPrayerReminder());
                await Navigation.RemovePopupPageAsync(loadingPage);

            //}
            //else
            //{
            //    // if No make all user alarm Active

            //}
        }
        private async void OnDeleteTimeClicked(object sender, EventArgs e)
        {
            var item = (Xamarin.Forms.Button)sender;
            string TimeId = Convert.ToString(item.CommandParameter.ToString());
            var answer = await DisplayAlert("Question?", "Would you like delete this reminder", "Yes", "No");
            if (answer)
            {

                PostNotificationData _DeleteNotificationTime = new PostNotificationData();
                var _data = await _DeleteNotificationTime.DeleteNotificationTimeUser(new PostNotificationDataModel { TimeId = Convert.ToInt32(TimeId) });
                if (_data.Status == true)
                {
                    var loadingPage = new LoadingPopupPage();
                    await Navigation.PushPopupAsync(loadingPage);
                    BindNotificationTime();
                    await Navigation.RemovePopupPageAsync(loadingPage);
                }

            }
            else
            {
                // if No make all user alarm Active

            }
        }
        public async void BindNotificationTime()
        {
            var loadingPage = new LoadingPopupPage();
            await Navigation.PushPopupAsync(loadingPage);
            string UserUId = "";
            if (Application.Current.Properties.ContainsKey("UserFirstName") && Application.Current.Properties.ContainsKey("UserLastName"))
            {
                UserUId = Convert.ToString(Application.Current.Properties["UserUID"]);
            }
            if (UserUId != string.Empty)
            {
                PostNotificationData objNotificationData = new PostNotificationData();
                List<PostNotificationDataModel> _list = new List<PostNotificationDataModel>();
                _list = await objNotificationData.getAllNotificationDataUserTime(UserUId);
                if (_list != null)
                {
                    NotificationTimeView.ItemsSource = _list;
                    if (_list.Count > 0)
                    {
                        NotificationTimeView.IsVisible = true;
                        lblNotificationUser.Text = "Following are the Prayer Reminders:";
                        //SwitchRemainder.IsToggled = true;

                    }
                    else
                    {
                        NotificationTimeView.IsVisible = false;
                        lblNotificationUser.Text = "There is no Prayer Reminders Click Add New to set";
                        //  SwitchRemainder.IsToggled = false;
                    }
                }
                else
                {
                    NotificationTimeView.IsVisible = false;
                    NotificationTimeView.ItemsSource = null;
                    lblNotificationUser.Text = "There is no Prayer Reminders Click Add New to set";
                }
            }
            await Navigation.RemovePopupPageAsync(loadingPage);
        }

        async void AddItem_Clicked(object sender, EventArgs e)
        {
         
            await Navigation.PushModalAsync(new AddPrayerReminder());
          
            // await Navigation.PushAsync(new AddPrayerReminder());
           
        }
        async void BackItem_Clicked(object sender, EventArgs e)
        {
           
            Application.Current.Properties["userUniqueIDSearchUser"] = null;
            var loadingPage = new LoadingPopupPage();
            await Navigation.PushPopupAsync(loadingPage);
            await Navigation.PushModalAsync(new MenuPageNotifications());
            await Navigation.RemovePopupPageAsync(loadingPage);
        }
    }
}
