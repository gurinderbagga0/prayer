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

namespace DrawerMenu
{
    public partial class PushNotificationsAdmin : ContentPage
    {
        private AddPrayerRequestPopupPage _AddPrayerRequestPopup;
        private ObservableCollection<AllUsersModel> Iitem;
        public PushNotificationsAdmin()
        {
            //  NavigationPage.SetHasNavigationBar(this, false);
            string Platform = Device.RuntimePlatform;
            if (!Platform.Contains("Android"))
            {
                NavigationPage.SetTitleIcon(this, "logoios.png");
            }
            if (Application.Current.Properties.ContainsKey("UserRole"))
            {
                string UserRole = Convert.ToString(Application.Current.Properties["UserRole"]);
                if (UserRole.Contains("admin"))
                {
                    InitializeComponent();
                    ShowDataRoleWise();
                    GetLiveCounter();
                    BindAllNotificationsSet();
                    // BindingContext = new AllUsersViewModel();
                    //   lstAllUsersAdmin.RefreshCommand.Execute(null);
                }
                else
                {

                }
            }

        }
        public async void BindAllNotificationsSet()
        {
            PostNotificationData objNotificationData = new PostNotificationData();
            List<PostNotificationDataModel> _list = new List<PostNotificationDataModel>();
            _list = await objNotificationData.getAllNotificationSetDataAdmin();
            if (_list != null)
            {
                NotificationTimeView.ItemsSource = _list;
                if (_list.Count > 0)
                {
                    lblNotification.Text = "Following are the time settings";
                    stkNotificationTime.IsVisible = true;
                }
                else
                {
                    stkNotificationTime.IsVisible = false;
                }
            }
            else
            {
                stkNotificationTime.IsVisible = false;
                lblNotification.Text = "";
                NotificationTimeView.ItemsSource = null;
            }
        }
        private async void BackButtonClicked(object sender, EventArgs e)
        {
            var loadingPage = new LoadingPopupPage();
            await Navigation.PushPopupAsync(loadingPage);
            Application.Current.Properties["Request"] = "MyRequestsUserid";
            await Navigation.PushModalAsync(new MenuPage());
            await Navigation.RemovePopupPageAsync(loadingPage);
        }
        public bool ValidateAllControls()
        {

            bool DayRequired = true;
            bool SubscribeRequired = true;
            if (DaysPicker.SelectedIndex == -1)
            {
                // GnderPicker.BackgroundColor = Color.Red;
                DaysPicker.BackgroundColor = (Color.FromHex("#F4C6C9"));
                DayRequired = false;
            }
            else
            {
                DaysPicker.BackgroundColor = (Color.FromHex("#eaeaea"));
                DayRequired = true;
            }
            if (SubscribePicker.SelectedIndex == -1)
            {
                // GnderPicker.BackgroundColor = Color.Red;
                SubscribePicker.BackgroundColor = (Color.FromHex("#F4C6C9"));
                SubscribeRequired = false;
            }
            else
            {
                SubscribePicker.BackgroundColor = (Color.FromHex("#eaeaea"));
                SubscribeRequired = true;
            }



            return DayRequired && SubscribeRequired;
        }
        private async void OnSaveNotificationClick(object sender, EventArgs e)
        {
            bool Valid = ValidateAllControls();
            if (Valid == true)
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
                if (SubscribePicker.SelectedIndex == -1)
                {
                    UserType = "";
                }
                else
                {
                    string UserTypeName = SubscribePicker.Items[SubscribePicker.SelectedIndex];
                    if (UserTypeName == "All")
                    {
                        UserType = "1"; // 1 for all users
                    }
                    if (UserTypeName == "Subscribe Users")
                    {
                        UserType = "2"; // 2 for Subscribe Notifications users
                    }
                    if (UserTypeName == "UnSubscribe Users")
                    {
                        UserType = "3"; // 3 for UnSubscribe Notifications users
                    }
                    if (UserTypeName == "Requesters")
                    {
                        UserType = "4"; // 4 for Requesters users
                    }
                    if (UserTypeName == "Champions")
                    {
                        UserType = "5"; // 5 for Champions users
                    }
                }
                time = Convert.ToString(TimePicker.Time);
                var loadingPage = new LoadingPopupPage();
                await Navigation.PushPopupAsync(loadingPage);
                PostNotificationData _PostNotificationData = new PostNotificationData();
                var _data = await _PostNotificationData.PostNotificationDetails(new PostNotificationDataModel { DayName = day, NotificationTime = time, UserType = UserType });
                await Navigation.RemovePopupPageAsync(loadingPage);
                if (_data.Status == true && _data.Message == "Passsed")
                {
                    BindAllNotificationsSet();
                    //if (day != string.Empty)
                    //{
                    //    PostNotificationData objNotificationData = new PostNotificationData();
                    //    List<PostNotificationDataModel> _list = new List<PostNotificationDataModel>();
                    //    _list = await objNotificationData.getAllNotificationDataAdmin(day);
                    //    if (_list != null)
                    //    {
                    //        NotificationTimeView.ItemsSource = _list;
                    //        if (_list.Count > 0)
                    //        {
                    //            lblNotification.Text = "Following are the time settings for : " + day;
                    //            stkNotificationTime.IsVisible = true;
                    //        }
                    //        else
                    //        {
                    //            stkNotificationTime.IsVisible = false;
                    //        }
                    //    }
                    //    else
                    //    {
                    //        stkNotificationTime.IsVisible = false;
                    //        lblNotification.Text = "";
                    //        NotificationTimeView.ItemsSource = null;
                    //    }
                    //}
                }
                else if (_data.Status == false && _data.Message.Contains("already exists"))
                {
                    string msgStatus = "-5";
                    await Navigation.PushPopupAsync(new SignUpRequesterFailedPopupPage(msgStatus));
                }

            }
        }
        private async void OnDeleteTimeClicked(object sender, EventArgs e)
        {
            //var item = (Xamarin.Forms.Button)sender;
            //string TimeId = Convert.ToString(item.CommandParameter.ToString());
            //var DeleteNotificationTimeAdminpopUp = new DeleteNotificationtimeAdminpopUp();
            //Application.Current.Properties["TimeId"] = TimeId;
            //await Navigation.PushPopupAsync(DeleteNotificationTimeAdminpopUp);

            var item = (Xamarin.Forms.Button)sender;
            string TimeId = Convert.ToString(item.CommandParameter.ToString());
            var answer = await DisplayAlert("Question?", "Would you like delete this time settings", "Yes", "No");
            if (answer)
            {
                PostNotificationData _DeleteNotificationTime = new PostNotificationData();
                var _data = await _DeleteNotificationTime.DeleteNotificationTime(new PostNotificationDataModel { TimeId = Convert.ToInt32(TimeId) });
                if (_data.Status == true)
                {
                    var loadingPage = new LoadingPopupPage();
                    await Navigation.PushPopupAsync(loadingPage);
                    BindAllNotificationsSet();
                    await Navigation.RemovePopupPageAsync(loadingPage);
                }

            }
            else
            {
                // if No make all user alarm Active

            }

        }

        //public async void OnSelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (DaysPicker.SelectedIndex == -1)
        //    {
        //        //  boxView.Color = Color.Default;
        //    }
        //    else
        //    {

        //        string Day = DaysPicker.Items[DaysPicker.SelectedIndex];
        //        if (Day != string.Empty)
        //        {
        //            PostNotificationData objNotificationData = new PostNotificationData();
        //            List<PostNotificationDataModel> _list = new List<PostNotificationDataModel>();
        //            _list = await objNotificationData.getAllNotificationDataAdmin(Day);
        //            if (_list != null)
        //            {
        //                NotificationTimeView.ItemsSource = _list;
        //                if (_list.Count > 0)
        //                {
        //                    lblNotification.Text = "Following are the time settings for : " + Day;
        //                    stkNotificationTime.IsVisible = true;
        //                }
        //                else
        //                {
        //                    stkNotificationTime.IsVisible = false;
        //                }
        //            }
        //            else
        //            {
        //                stkNotificationTime.IsVisible = false;
        //                lblNotification.Text = "";
        //                NotificationTimeView.ItemsSource = null;
        //            }
        //        }

        //    }
        //}

        public void ShowDataRoleWise()
        {
            if (Application.Current.Properties.ContainsKey("UserRole"))
            {
                string UserRole = Convert.ToString(Application.Current.Properties["UserRole"]);
                if (UserRole.Contains("warrior") && !UserRole.Contains("admin") && !UserRole.Contains("member"))
                {
                    lblAddPrayerReques.IsVisible = false;
                    lblWaitingText.WidthRequest = 310;

                }
                if (UserRole.Contains("member") && !UserRole.Contains("admin") && !UserRole.Contains("warrior"))
                {
                    lblAddPrayerReques.IsVisible = true;


                }
                if (UserRole.Contains("member") && UserRole.Contains("warrior") && UserRole.Contains("admin"))
                {
                    lblAddPrayerReques.IsVisible = true;

                }

                if (UserRole.Contains("admin"))
                {
                    lblAddPrayerReques.IsVisible = true;


                }
                if (UserRole.Contains("moderator"))
                {

                    lblAddPrayerReques.IsVisible = true;


                }
                if (UserRole.Contains("member") && UserRole.Contains("warrior") && UserRole.Contains("admin"))
                {
                    lblAddPrayerReques.IsVisible = true;

                }
                if (UserRole.Contains("member") && UserRole.Contains("warrior") && UserRole.Contains("admin") && UserRole.Contains("moderator"))
                {
                    lblAddPrayerReques.IsVisible = true;

                }
                if (!UserRole.Contains("member") && !UserRole.Contains("warrior") && !UserRole.Contains("admin") && !UserRole.Contains("moderator"))
                {
                    lblAddPrayerReques.IsVisible = false;
                    lblWaitingText.WidthRequest = 310;

                }
            }
        }
        private async void WaitingTextClicked(object sender, EventArgs e)
        {
            var loadingPage = new LoadingPopupPage();
            await Navigation.PushPopupAsync(loadingPage);
            Application.Current.Properties["Request"] = "WaitingForPrayers";
            await Navigation.PushModalAsync(new MenuPage());
            await Navigation.RemovePopupPageAsync(loadingPage);
        }

        private async void AddPrayerRequestClicked(object sender, EventArgs e)
        {
            await Navigation.PushPopupAsync(_AddPrayerRequestPopup);
        }
        public async void GetLiveCounter()
        {

            UserStats _user = new UserStats();
            var _data = await _user.GetStats();
            if (_data.Status == true)
            {
                lblWaitingText.Text = _data.prayers_needed + " Waiting";
            }

        }

        async void Handle_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
            {
                return;
            }
            else
            {
                return;
                //var _item = (AllUsersModel)e.SelectedItem;
                //string UserId = _item.uuid;
                //Application.Current.Properties["userUniqueIDSearchUser"] = UserId;
                //await Navigation.PushAsync(new Profile());
            }
            //((ListView)sender).SelectedItem = null;
        }
    }


}
