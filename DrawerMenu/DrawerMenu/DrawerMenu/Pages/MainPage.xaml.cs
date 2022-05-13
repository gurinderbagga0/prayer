using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Extensions;
using Xamarin.Forms;
using DrawerMenu.Pages;
using PushNotification.Plugin;
using PushNotification;

namespace DrawerMenu
{
    public partial class MainPage : ContentPage
    {
        string NotificationClickedPage = "";
        private LoginPopupPage _loginPopup;
        public MainPage(string RequestedPostedUserId, string PrayerRequestId)
        {
            Application.Current.Properties["RequestPostedUserId"] = RequestedPostedUserId;
            Application.Current.Properties["PrayerRequestId"] = PrayerRequestId;
            NavigationPage.SetHasNavigationBar(this, false);
            _loginPopup = new LoginPopupPage();

           // CheckAndroidRequestPermisionEnable();

            if (Application.Current.Properties.ContainsKey("AccessToken"))
            {

               // string AccessToken1 = Convert.ToString(Application.Current.Properties["AccessToken"]);
                //var answer = DisplayAlert("Question?", AccessToken1, "Yes", "No");
                var AccessToken = Convert.ToString(Application.Current.Properties["AccessToken"]);
                if (AccessToken != null && AccessToken != "")
                {
                    if (Application.Current.Properties.ContainsKey("RequestPostedUserId") && Application.Current.Properties.ContainsKey("PrayerRequestId"))
                    {
                        RequestedPostedUserId = Convert.ToString(Application.Current.Properties["RequestPostedUserId"]);
                        PrayerRequestId = Convert.ToString(Application.Current.Properties["PrayerRequestId"]);
                        // do something with id
                        if ((RequestedPostedUserId != null && RequestedPostedUserId != string.Empty) && (PrayerRequestId != null && PrayerRequestId != string.Empty))
                        {
                             Navigation.PushModalAsync(new MenuPageMemberRequest(RequestedPostedUserId, PrayerRequestId));
                        }
                        else
                        {
                            Navigation.PushModalAsync(new MenuPage());
                        }
                    }
                    else
                    {
                        InitializeComponent();
                    }
                }
                else
                {
                    InitializeComponent();
                }
            }
            else
            {
                InitializeComponent();
            }
        }
        //public async void CheckAndroidRequestPermisionEnable()
        //{
        //    var answer = await DisplayAlert("Question?", "Would you like delete this reminder", "Yes", "No");
        //  //  var answer = await DisplayAlert("ThePrayerZone Would Like to Send You Notifications", "Notifications may include alerts, sounds, icon and badges. Would you like delete this reminder", "Allow", "Don't Allow");
        //    if (answer)
        //    {

        //    }
        //    else
        //    {

        //    }
        //}

        private async void OnOpenPupup(object sender, EventArgs e)
        {
            //var page = new LoginPopupPage();
            if (Application.Current.Properties.ContainsKey("RequestPostedUserId") && Application.Current.Properties.ContainsKey("PrayerRequestId"))
            {
                var RequestedPostedUserId = Application.Current.Properties["RequestPostedUserId"];
                var PrayerRequestId = Application.Current.Properties["PrayerRequestId"];
            }
            await Navigation.PushPopupAsync(_loginPopup);
        }

        private async void OnUserAnimationPupup(object sender, EventArgs e)
        {
            var page = new SignUpPopupPage();
            await Navigation.PushPopupAsync(page);
        }

        private async void OnOpenSystemOffsetPage(object sender, EventArgs e)
        {
            //  var page = new SystemOffsetPage();
            // await Navigation.PushPopupAsync(page);
        }

        private async void OnOpenListViewPage(object sender, EventArgs e)
        {
            ///var page = new ListViewPage();
           // await Navigation.PushPopupAsync(page);
        }
    }
}
