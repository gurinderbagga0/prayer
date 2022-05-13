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
    public partial class SignUpRequesterFailedPopupPage : PopupPage
    {
        public SignUpRequesterFailedPopupPage(string msgStatus)
        {
            InitializeComponent();
            if (msgStatus == "-2")
            {
                messageStatus.Text = "User with this email Already exists.";
            }
            if (msgStatus == "-1")
            {
                messageStatus.Text = "User with this UserName Already exists.";
            }
            if (msgStatus == "-5")
            {
                messageStatus.Text = "This Time already exists";
            }
            if (msgStatus == "ForgotPasswordSuccess")
            {

                messageStatus.Text = "You will receive an email with instructions about how to reset your password in a few minutes.";
            }
            if (msgStatus == "ForgotPasswordFailed")
            {
                messageStatus.Text = "Email address does not exists in database.";
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
