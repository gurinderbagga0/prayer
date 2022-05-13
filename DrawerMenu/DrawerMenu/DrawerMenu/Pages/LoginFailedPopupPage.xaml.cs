using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace DrawerMenu.Pages
{
    public partial class LoginFailedPopupPage : PopupPage
    {
        public LoginFailedPopupPage(string msgStatus)
        {
            InitializeComponent();
            if (msgStatus == "0")
            {
                messageStatus.Text = "Invalid log In or password";
            }
            if (msgStatus == "verifyEmail")
            {
                messageStatus.Text = "You have to confirm your account before continuing";
            }
            if (msgStatus == "PrayerTitleAlreadyExists")
            {
                messageStatus.Text = "Prayer title already exists";
            }
            
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();

            HidePopup();
        }

        private async void HidePopup()
        {
            await Task.Delay(4000);
            await PopupNavigation.RemovePageAsync(this);
        }
    }
}
