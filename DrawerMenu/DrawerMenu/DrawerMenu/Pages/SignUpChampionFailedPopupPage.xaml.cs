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
    public partial class SignUpChampionFailedPopupPage : PopupPage
    {
        public SignUpChampionFailedPopupPage(string msgStatus)
        {
            InitializeComponent();
            if (msgStatus == "1")
            {
               // FullmessageStatus.IsVisible = true;
                messageStatus.Text = "A message with confirmation email has been sent you your email. Please confirm your email to login.";
            }
            if (msgStatus == "-2")
            {
                messageStatus.Text = "User with this email Already exists.";
            }
            if (msgStatus == "-1")
            {
                messageStatus.Text = "User with this UserName Already exists.";
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
