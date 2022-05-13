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
    public partial class Disclaimer : ContentPage
    {
        string MenuPage = "";
        public Disclaimer()
        {
            InitializeComponent();
            stackHeading.Margin = new Thickness(10, 30);
            //  NavigationPage.SetTitleIcon(this, "logoios.png");
        }
        void OnTapGestureRecognizerTapped(object sender, EventArgs args)
        {
            Device.OpenUri(new Uri("mailto:theprayerzone@thehopeline.com"));
        }
        async void BackItem_Clicked(object sender, EventArgs e)
        {
            //if (Application.Current.Properties.ContainsKey("MenuPage"))
            //{
            //    MenuPage = Convert.ToString(Application.Current.Properties["MenuPage"]);
            //}
            //if (MenuPage == "MainMenu")
            //{
                var loadingPage = new LoadingPopupPage();
                await Navigation.PushPopupAsync(loadingPage);
                Application.Current.Properties["Request"] = "MyRequestsUserid";
                await Navigation.PushModalAsync(new MenuPage());
                await Navigation.RemovePopupPageAsync(loadingPage);
                Application.Current.Properties["MenuPage"] = null;
        //    }
        //    if (MenuPage == "MenuPageMemberRequest")
        //    {
        //        var loadingPage = new LoadingPopupPage();
        //        await Navigation.PushPopupAsync(loadingPage);
        //        await Navigation.PushModalAsync(new MenuPageMemberRequest());
        //        await Navigation.RemovePopupPageAsync(loadingPage);
        //        Application.Current.Properties["MenuPage"] = null;
        //    }
        //    if (MenuPage == "MenuPageProfile")
        //    {
        //        var loadingPage = new LoadingPopupPage();
        //        await Navigation.PushPopupAsync(loadingPage);
        //        await Navigation.PushModalAsync(new MenuPageProfile());
        //        await Navigation.RemovePopupPageAsync(loadingPage);
        //        Application.Current.Properties["MenuPage"] = null;
        //    }
        }
    }
}
