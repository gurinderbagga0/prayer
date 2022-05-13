using DrawerMenu.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Extensions;
using Xamarin.Forms;
using DrawerMenu.Pages;
using Xamarin.Forms;
using DrawerMenu.Core;

namespace DrawerMenu
{
    public partial class MenuPageNotifications : MasterDetailPage
    {
        private AddPrayerRequestPopupPage _AddPrayerRequestPopup;
        private DisclaimerPopupPage _DisclaimerPopup;
        private ContactUsPopupPage _ContactUsPopup;
        public MenuPageNotifications()
        {
            _AddPrayerRequestPopup = new AddPrayerRequestPopupPage();
            _DisclaimerPopup = new DisclaimerPopupPage();
            _ContactUsPopup = new ContactUsPopupPage();
            try
            {
                InitializeComponent();

            }
            catch (Exception ex)
            {

            }
            string Platform = Device.RuntimePlatform;
            if (!Platform.Contains("Android"))
            {
                stackUserInfo.Margin = new Thickness(10, 30);
                // stackUserInfo.Spacing = 40;

            }
            if (Application.Current.Properties.ContainsKey("UserFirstName") && Application.Current.Properties.ContainsKey("UserLastName"))
            {
                string UserFullName = "";
                var UserFirstName = Application.Current.Properties["UserFirstName"];
                var UserLastName = Application.Current.Properties["UserLastName"];
                if (UserFirstName != null)
                {
                    TxtUserFullName.Text = Convert.ToString(UserFirstName);
                }
                if (UserLastName != null)
                {
                    TxtUserFullName.Text = TxtUserFullName.Text + " " + Convert.ToString(UserLastName);
                }
                string UserRole = Convert.ToString(Application.Current.Properties["UserRole"]);
                //  member,admin,warrior

                if (UserRole.Contains("warrior") && !UserRole.Contains("admin") && !UserRole.Contains("member"))
                {
                    btnPostaPrayer.IsVisible = false;
                    btnMyRequests.IsVisible = false;
                    btnAllPrayerRequests.IsVisible = true;
                    btnWaitingforPrayer.IsVisible = true;
                    btnRequestsIvePrayedFor.IsVisible = true;
                }
                if (UserRole.Contains("member") && !UserRole.Contains("admin") && !UserRole.Contains("warrior"))
                {
                    btnPostaPrayer.IsVisible = true;
                    btnMyRequests.IsVisible = true;
                    btnAllPrayerRequests.IsVisible = true;
                    btnWaitingforPrayer.IsVisible = true;
                    btnRequestsIvePrayedFor.IsVisible = false;
                }
                if (UserRole.Contains("member") && UserRole.Contains("warrior") == true && !UserRole.Contains("admin"))
                {
                    btnPostaPrayer.IsVisible = true;
                    btnMyRequests.IsVisible = true;
                    btnAllPrayerRequests.IsVisible = true;
                    btnWaitingforPrayer.IsVisible = true;
                    btnRequestsIvePrayedFor.IsVisible = true;
                }

                if (UserRole.Contains("admin"))
                {
                    btnPostaPrayer.IsVisible = true;
                    btnMyRequests.IsVisible = true;
                    btnAllPrayerRequests.IsVisible = true;
                    btnWaitingforPrayer.IsVisible = true;
                    btnRequestsIvePrayedFor.IsVisible = false;
                    btnUsers.IsVisible = true;
                    btnPushNotifications.IsVisible = true;
                    btnBlockWords.IsVisible = true;
                }
                //  moderator,  member,admin,warrior
                if (UserRole.Contains("moderator"))
                {
                    btnPostaPrayer.IsVisible = true;
                    btnMyRequests.IsVisible = true;
                    btnAllPrayerRequests.IsVisible = true;
                    btnWaitingforPrayer.IsVisible = true;
                    btnRequestsIvePrayedFor.IsVisible = false;
                }
                if (UserRole.Contains("member") && UserRole.Contains("warrior") && UserRole.Contains("admin"))
                {
                    btnPostaPrayer.IsVisible = true;
                    btnMyRequests.IsVisible = true;
                    btnAllPrayerRequests.IsVisible = true;
                    btnWaitingforPrayer.IsVisible = true;
                    btnRequestsIvePrayedFor.IsVisible = true;
                    btnUsers.IsVisible = true;
                    btnPushNotifications.IsVisible = true;
                    btnBlockWords.IsVisible = true;
                }
                if (UserRole.Contains("member") && UserRole.Contains("warrior") && UserRole.Contains("admin") && UserRole.Contains("moderator"))
                {
                    btnPostaPrayer.IsVisible = true;
                    btnMyRequests.IsVisible = true;
                    btnAllPrayerRequests.IsVisible = true;
                    btnWaitingforPrayer.IsVisible = true;
                    btnRequestsIvePrayedFor.IsVisible = true;
                    btnUsers.IsVisible = true;
                    btnPushNotifications.IsVisible = true;
                    btnBlockWords.IsVisible = true;
                }
                if (!UserRole.Contains("member") && !UserRole.Contains("warrior") && !UserRole.Contains("admin") && !UserRole.Contains("moderator"))
                {
                    btnPostaPrayer.IsVisible = false;
                    btnMyRequests.IsVisible = false;
                    btnAllPrayerRequests.IsVisible = false;
                    btnWaitingforPrayer.IsVisible = false;
                    btnRequestsIvePrayedFor.IsVisible = false;
                }

            }
            if (Application.Current.Properties.ContainsKey("UserEmail"))
            {
                var UserEmail = Application.Current.Properties["UserEmail"];
                if (UserEmail != null)
                {
                    TxtUserEmail.Text = Convert.ToString(UserEmail);
                }
            }


        }
        private async void OnOpenContactUsPage(object sender, EventArgs e)
        {
            var loadingPage = new LoadingPopupPage();
            await Navigation.PushPopupAsync(loadingPage);
            await Navigation.PushPopupAsync(_ContactUsPopup);
            IsPresented = false;
            await Navigation.RemovePopupPageAsync(loadingPage);
        }
        private async void OnOpenHome(object sender, EventArgs e)
        {
            var loadingPage = new LoadingPopupPage();
            await Navigation.PushPopupAsync(loadingPage);
            Application.Current.Properties["Request"] = "MyRequestsUserid";
            Detail = new NavigationPage(new HomeNew());
            IsPresented = false;
            await Navigation.RemovePopupPageAsync(loadingPage);
        }
        private async void OnOpenAllPrayers(object sender, EventArgs e)
        {
            var loadingPage = new LoadingPopupPage();
            await Navigation.PushPopupAsync(loadingPage);
            Application.Current.Properties["Request"] = "MyRequestsUserid";
            Detail = new NavigationPage(new HomeNew());
            IsPresented = false;
            await Navigation.RemovePopupPageAsync(loadingPage);
        }
        private async void OnOpenWaitingPrayers(object sender, EventArgs e)
        {
            var loadingPage = new LoadingPopupPage();
            await Navigation.PushPopupAsync(loadingPage);
            Application.Current.Properties["Request"] = "WaitingForPrayers";
            Detail = new NavigationPage(new HomeNew());
            IsPresented = false;
            await Navigation.RemovePopupPageAsync(loadingPage);
        }
        private async void OnOpenMyRequestsPosted(object sender, EventArgs e)
        {
            var loadingPage = new LoadingPopupPage();
            await Navigation.PushPopupAsync(loadingPage);
            Application.Current.Properties["Request"] = "MyRequests";
            Detail = new NavigationPage(new HomeNew());
            IsPresented = false;
            await Navigation.RemovePopupPageAsync(loadingPage);
        }
        private async void OnOpenGetHelpPage(object sender, EventArgs e)
        {
            var loadingPage = new LoadingPopupPage();
            await Navigation.PushPopupAsync(loadingPage);
            Detail = new NavigationPage(new GetHelp());
            IsPresented = false;
            await Navigation.RemovePopupPageAsync(loadingPage);
        }
        private async void OnOpenRequestsIHavePrayedFor(object sender, EventArgs e)
        {
            var loadingPage = new LoadingPopupPage();
            await Navigation.PushPopupAsync(loadingPage);
            Application.Current.Properties["Request"] = "PrayedForRequests";
            Detail = new NavigationPage(new HomeNew());
            IsPresented = false;
            await Navigation.RemovePopupPageAsync(loadingPage);
        }
        private async void OnOpenPostaPrayer(object sender, EventArgs e)
        {
            var loadingPage = new LoadingPopupPage();
            await Navigation.PushPopupAsync(loadingPage);
            await Navigation.PushPopupAsync(_AddPrayerRequestPopup);
            IsPresented = false;
            await Navigation.RemovePopupPageAsync(loadingPage);
        }
        private async void OnOpenDisclaimerPage(object sender, EventArgs e)
        {
            //var loadingPage = new LoadingPopupPage();
            //await Navigation.PushPopupAsync(loadingPage);
            //Application.Current.Properties["MenuPage"] = "MainMenu";
            //await Navigation.PushModalAsync(new Disclaimer());
            //IsPresented = false;
            //await Navigation.RemovePopupPageAsync(loadingPage);

            var loadingPage = new LoadingPopupPage();
            await Navigation.PushPopupAsync(loadingPage);
            await Navigation.PushPopupAsync(_DisclaimerPopup);
            IsPresented = false;
            await Navigation.RemovePopupPageAsync(loadingPage);
        }
        private async void OnOpenSearchUsersPage(object sender, EventArgs e)
        {
            var loadingPage = new LoadingPopupPage();
            await Navigation.PushPopupAsync(loadingPage);
            Detail = new NavigationPage(new SearchUsersAdmin());
            IsPresented = false;
            await Navigation.RemovePopupPageAsync(loadingPage);
        }
        private async void OnOpenPushNotificationAdminPage(object sender, EventArgs e)
        {
            var loadingPage = new LoadingPopupPage();
            await Navigation.PushPopupAsync(loadingPage);
            Detail = new NavigationPage(new PushNotificationsAdmin());
            IsPresented = false;
            await Navigation.RemovePopupPageAsync(loadingPage);
        }
        private async void OnOpenBlockWordsAdminPage(object sender, EventArgs e)
        {
            var loadingPage = new LoadingPopupPage();
            await Navigation.PushPopupAsync(loadingPage);
            Detail = new NavigationPage(new BlockWords());
            IsPresented = false;
            await Navigation.RemovePopupPageAsync(loadingPage);
        }
        private async void OnOpenChatPage(object sender, EventArgs e)
        {
            var loadingPage = new LoadingPopupPage();
            await Navigation.PushPopupAsync(loadingPage);
            Detail = new NavigationPage(new LiveChat());
            IsPresented = false;
            await Navigation.RemovePopupPageAsync(loadingPage);
        }
        private async void OnOpenProfilePage(object sender, EventArgs e)
        {
            Application.Current.Properties["userUniqueIDSearchUser"] = null;
            var loadingPage = new LoadingPopupPage();
            await Navigation.PushPopupAsync(loadingPage);
            Detail = new NavigationPage(new MemberProfile());
            IsPresented = false;
            await Navigation.RemovePopupPageAsync(loadingPage);
        }

        private async void OnMyNotificationsPage(object sender, EventArgs e)
        {
            var loadingPage = new LoadingPopupPage();
            await Navigation.PushPopupAsync(loadingPage);
            Detail = new NavigationPage(new MyNotifications());
            IsPresented = false;
            await Navigation.RemovePopupPageAsync(loadingPage);
        }

        private async void OnLogOutPage(object sender, EventArgs e)
        {
            var loadingPage = new LoadingPopupPage();
            await Navigation.PushPopupAsync(loadingPage);
            if (Application.Current.Properties.ContainsKey("UserUID"))
            {
                string UserUID = Convert.ToString(Application.Current.Properties["UserUID"]);

                PostDeviceToken _PostDeviceToken = new PostDeviceToken();
                var _dataUpdate = _PostDeviceToken.UpdateUserLoginDetailsUserId(new UpdateDeviceDetailsModel { UuID = UserUID });
            }
            Application.Current.Properties["userUniqueIDSearchUser"] = null;
            Application.Current.Properties["AccessToken"] = null;
            Application.Current.Properties["UserUID"] = null;
            Application.Current.Properties["UserRole"] = null;
            Application.Current.Properties["UserFirstName"] = null;
            Application.Current.Properties["UserLastName"] = null;
            Application.Current.Properties["UserEmail"] = null;
            await Navigation.RemovePopupPageAsync(loadingPage);
            await Navigation.PushModalAsync(new MainPage("", ""), true);
            IsPresented = false;
        }

    }
}
