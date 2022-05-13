using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace DrawerMenu.Pages
{
    public partial class TestNotification : ContentPage
    {
        public TestNotification(string x, string y)
        {

            //Application.Current.Properties["RequestPostedUserId"] = x;
            //Application.Current.Properties["PrayerRequestId"] = y;
            //if (Application.Current.Properties.ContainsKey("AccessToken"))
            //{
            //    var AccessToken = Convert.ToString(Application.Current.Properties["AccessToken"]);
            //    if (AccessToken != null && AccessToken != "")
            //    {
            InitializeComponent();
            App.Current.MainPage = new MenuPageMemberRequest(x, y);
                  
                    // Navigation.PushModalAsync(new MenuPageMemberRequest(x, y));
            //    }
            //}
            //else
            //{
            //    Navigation.PushModalAsync(new MainPage("",""));
            //    InitializeComponent();
            //}
        }
    }
}
